using _Scripts.Buildings;
using _Scripts.Player;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Bots
{
    public class BotController : MonoBehaviour
    {
        private enum BotMode
        {
            Default, // none of the below, move to random position around spawn location and enter idle animation on random stops
            SearchEnemy, // enemy is in detection radius, walk slowly towards random position near enemy
            ChaseEnemy, // enemy in detection radius and in field of view, run towards enemy position
            AttackEnemy, // enemy in attack range, stop movement and hit enemy
            Die // bot life == 0, play death animation and destroy gameobject
        }

        // bot behaviour
        private BotMode _mode = BotMode.Default;

        private BotStats _stats;

        [SerializeField] protected internal float searchRadius = 10;
        [SerializeField] private float chaseRadius = 5;
        [SerializeField] public float attackRadius = 1;
        [SerializeField] private LayerMask mask;
        [SerializeField] private LayerMask player;

        // bot movement
        [SerializeField] private float stopMargin = 0.1f;
        protected internal NavMeshAgent _agent;
        private Vector3 _destination;

        // bot animation
        protected internal Animator _animator;

        protected internal int _isIdlingHash;
        protected internal int _isWalkingHash;
        private int _isRunningHash;
        private int _isAttackingHash;
        private int _isDyingHash;
        private int _isGettingHitHash;
        private int _isHittingHash;

        // misc
        private Transform _target;

        private static int _botCounter;

        public Material water; // invisible

        void Awake()
        {
            _botCounter++;
        }

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = true;

            _agent.stoppingDistance = attackRadius - stopMargin;

            _target = PlayerManager.instance.player.transform;

            _isIdlingHash = Animator.StringToHash("idle");
            _isWalkingHash = Animator.StringToHash("walking");
            _isRunningHash = Animator.StringToHash("running");
            _isAttackingHash = Animator.StringToHash("attacking");
            _isDyingHash = Animator.StringToHash("dying");
            _isGettingHitHash = Animator.StringToHash("getHit");
            _isHittingHash = Animator.StringToHash("isHitting");

            _stats = new BotStats(50, 50, 10, 0);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBotMode();

            _animator.SetBool(_isAttackingHash, false);

            _destination = transform.position;

            Debug.Log(_mode);

            switch (_mode)
            {
                case BotMode.Default:
                    Idling();
                    break;
                case BotMode.SearchEnemy:
                    Search();
                    break;
                case BotMode.ChaseEnemy:
                    Chase();
                    break;
                case BotMode.AttackEnemy:
                    Attack();
                    break;
                case BotMode.Die:
                    Die();
                    break;
            }
        }

        public static int GetBotCounter()
        {
            return _botCounter;
        }

        /**
         * Sets BotMode according to defined criteria.
         * Sets BotMode according to either distance to player or bot life.
         */
        private void UpdateBotMode()
        {
            var pos = transform.position;
            // _animator.SetBool(_isAttackingHash, false);

            _mode = BotMode.SearchEnemy;

            // Check if Bot is dead
            if (_stats.CurrentHealth <= 0)
            {
                _mode = BotMode.Die;
                return;
            }

            // Check if enemy in attack range
            if (Physics.CheckSphere(pos, attackRadius, mask))
            {
                _mode = BotMode.AttackEnemy;
                return;
            }

            // Check if enemy in chase range (and in field of view)
            Collider[] chase = Physics.OverlapSphere(pos, chaseRadius, mask);
            if (chase.Length > 0)
            {
                foreach (var col in chase)
                {
                    var targetDir = col.transform.position - pos;
                    var angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

                    if (angleToPlayer is >= -60 and <= 60) // 120° FOV 
                    {
                        Debug.Log("Player in sight!");
                        _mode = BotMode.ChaseEnemy;
                        return;
                    }
                    // if (col.CompareTag("Player"))
                    // {
                    //     var targetDir = col.transform.position - pos;
                    //     var angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

                    //     if (angleToPlayer is >= -60 and <= 60) // 120° FOV 
                    //     {
                    //         Debug.Log("Player in sight!");
                    //         _mode = BotMode.ChaseEnemy;
                    //         return;
                    //     }
                    // }
                }
            }

            // Check if enemy in search range
            if (Physics.CheckSphere(pos, searchRadius, mask) || !DestinationReached())
            {
                _mode = BotMode.SearchEnemy;
            }
        }

        /**
         * Sets the NavMeshAgents destination and the _destination field. 
         */
        private void Move(Vector3 destination)
        {
            _destination = destination;
            _agent.SetDestination(_destination);
        }

        /**
         * Determines if bot has reached its current destination.
         * Destination is equal to NavMeshAgent destination.
         */
        private bool DestinationReached()
        {
            // var distance = Vector3.Distance(transform.position, _destination);
            // Debug.Log("Distance is: " + distance + " , _stoppingDistance is: " + _stoppingDistance);
            // return distance <= _stoppingDistance;
            return _destination == transform.position;
        }

        /**
         * Behaviour in BotMode Default.
         *
         * Either stand still and idle, or choose random point within range and walk towards it.
         */
        private void Idling()
        {
            // Debug.Log("Bot is in default mode");
            _animator.SetTrigger(_isIdlingHash);
            // _animator.SetBool(isAttackingHash, false);
            // InvokeRepeating("Patrol", 10f, 30f);
            // gameObject.GetComponent<Patrol>();
        }

        private void Search()
        {
            _animator.SetTrigger(_isWalkingHash);

            var pos = transform.position;

            Collider[] search = Physics.OverlapSphere(pos, searchRadius, mask);
            if (search.Length > 0)
            {
                foreach (var col in search)
                {
                    Move(col.transform.position);

                    if (_destination != transform.position)
                    {
                        Move(_destination);
                    }
                    else if (!DestinationReached() && !Physics.CheckSphere(pos, searchRadius, mask))
                    {
                        _animator.SetTrigger(_isWalkingHash);
                        Move(_destination);
                    }
                    else if (!DestinationReached() && Physics.CheckSphere(pos, searchRadius, player))
                    {
                        _animator.SetTrigger(_isWalkingHash);
                        Move(_target.position);
                    }
                    else
                    {
                        _destination = transform.position;
                    }
                }
            }

            // // Debug.Log("Bot is in search mode");

            // if (Physics.CheckSphere(pos, searchRadius, mask))
            // {
            //     Move(_target.position);
            // }

            // if (_destination != transform.position /* && Physics.CheckSphere(pos, searchRadius, mask)*/)
            // {
            //     Move(_target.position);
            // }
            // else if (!DestinationReached() && !Physics.CheckSphere(pos, searchRadius, mask))
            // {
            //     _animator.SetTrigger(_isWalkingHash);
            //     Move(_target.position);
            // }
            // else
            // {
            //     _destination = transform.position;
            // }
        }

        private void Chase()
        {
            // Debug.Log("Bot is in chase mode");
            _animator.SetTrigger(_isRunningHash);
            // Move(_target.position);

            var pos = transform.position;
            Collider[] chase = Physics.OverlapSphere(pos, chaseRadius, mask);
            if (chase.Length > 0)
            {
                foreach (var col in chase)
                {
                    Move(col.transform.position);
                }
            }
        }

        private void Attack()
        {
            // Debug.Log("Bot is in attack mode");
            _animator.SetBool(_isAttackingHash, true);

            var pos = transform.position;

            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
            {
                // _animator.SetTrigger(_isHittingHash);
                _animator.SetTrigger(_isHittingHash);
            } 

            // FaceTarget();

            // Collider[] attack = Physics.OverlapSphere(pos, attackRadius, mask);
            // if (attack.Length > 0)
            // {
            //     foreach (var col in attack)
            //     {
            //         FaceTarget(col.gameObject);
            //         MakeDamage();
            //         // if (Attack1AnimationDonePlaying() == true && Attack2AnimationDonePlaying() == true && PunchAnimationDonePlaying() == true)
            //         // {
            //         //     if (col.CompareTag("Player"))
            //         //     {
            //         //         col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
            //         //     }
            //         //     else if (col.transform.CompareTag("Buildable"))
            //         //     {
            //         //         col.transform.gameObject.GetComponent<Buildable>().TakeDamage(_stats.Damage);
            //         //     }
            //         // }

            //     }
            // }



            // RaycastHit hit;
            // if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerMask))
            // {
            //     if (hit.transform.gameObject.CompareTag("Buildable"))
            //     {
            //         hit.transform.gameObject.GetComponent<Buildable>().TakeDamage(damage);
            //     }

            //     var enemy = hit.transform.GetComponent<BotController>();

            //     if (enemy != null)
            //     {
            //         enemy.TakeDamage(damage);
            //     }
            // }
        }

        private void Die()
        {
            // Debug.Log("Bot is in die mode");
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
            {
                _animator.SetTrigger(_isDyingHash);
            }
        }

        private void DeathAnimationDonePlaying()
        {
            Destroy(gameObject);
        }

        private void Attack1AnimationDonePlaying()
        {
            Collider[] attack = Physics.OverlapSphere(transform.position, attackRadius, mask);
            if (attack.Length > 0)
            {
                attack[0].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                Debug.Log("Hit");
                // var enemy = attack[0];
                // FaceTarget(enemy.gameObject);
                // enemy.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                // Debug.Log("Hit");

                // for (int i = 0; i < attack.Length; i++)
                // // foreach (var col in attack)
                // {
                //     // var enemy = attack[0];
                //     // FaceTarget(enemy.gameObject);
                //     // enemy.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //     // Debug.Log("Hit");
                //     attack[i].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //     Debug.Log("Hit");
                //     if (i > 0)
                //     {
                //         if (attack[i] != attack[i - 1])
                //         {
                //             attack[i].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //             Debug.Log("Hit");
                //         }

                //         // MakeDamage();
                //         // if (Attack1AnimationDonePlaying() == true && Attack2AnimationDonePlaying() == true && PunchAnimationDonePlaying() == true)
                //         // {
                //         //     if (col.CompareTag("Player"))
                //         //     {
                //         //         col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         //     }
                //         //     else if (col.transform.CompareTag("Buildable"))
                //         //     {
                //         //         col.transform.gameObject.GetComponent<Buildable>().TakeDamage(_stats.Damage);
                //         //     }
                //         // }
                //         // col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         // if (col.CompareTag("Player"))
                //         // {
                //         //     col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         // }
                //     }
                // }
            }
        }

        private void Attack2AnimationDonePlaying()
        {
            Collider[] attack = Physics.OverlapSphere(transform.position, attackRadius, mask);
            if (attack.Length > 0)
            {
                attack[0].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                Debug.Log("Hit");
                // var enemy = attack[0];
                // FaceTarget(enemy.gameObject);
                // enemy.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                // Debug.Log("Hit");

                // for (int i = 0; i < attack.Length; i++)
                // // foreach (var col in attack)
                // {
                //     // var enemy = attack[0];
                //     // FaceTarget(enemy.gameObject);
                //     // enemy.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //     // Debug.Log("Hit");
                //     attack[i].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //     Debug.Log("Hit");
                //     if (i > 0)
                //     {
                //         if (attack[i] != attack[i - 1])
                //         {
                //             attack[i].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //             Debug.Log("Hit");
                //         }

                //         // MakeDamage();
                //         // if (Attack1AnimationDonePlaying() == true && Attack2AnimationDonePlaying() == true && PunchAnimationDonePlaying() == true)
                //         // {
                //         //     if (col.CompareTag("Player"))
                //         //     {
                //         //         col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         //     }
                //         //     else if (col.transform.CompareTag("Buildable"))
                //         //     {
                //         //         col.transform.gameObject.GetComponent<Buildable>().TakeDamage(_stats.Damage);
                //         //     }
                //         // }
                //         // col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         // if (col.CompareTag("Player"))
                //         // {
                //         //     col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         // }
                //     }
                // }
            }
        }

        private void PunchAnimationDonePlaying()
        {
            Collider[] attack = Physics.OverlapSphere(transform.position, attackRadius, mask);
            if (attack.Length > 0)
            {
                attack[0].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                Debug.Log("Hit");
                // var enemy = attack[0];
                // FaceTarget(enemy.gameObject);
                // enemy.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                // Debug.Log("Hit");

                // for (int i = 0; i < attack.Length; i++)
                // // foreach (var col in attack)
                // {
                //     // var enemy = attack[0];
                //     // FaceTarget(enemy.gameObject);
                //     // enemy.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //     // Debug.Log("Hit");
                //     attack[i].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //     Debug.Log("Hit");
                //     if (i > 0)
                //     {
                //         if (attack[i] != attack[i - 1])
                //         {
                //             attack[i].transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //             Debug.Log("Hit");
                //         }

                //         // MakeDamage();
                //         // if (Attack1AnimationDonePlaying() == true && Attack2AnimationDonePlaying() == true && PunchAnimationDonePlaying() == true)
                //         // {
                //         //     if (col.CompareTag("Player"))
                //         //     {
                //         //         col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         //     }
                //         //     else if (col.transform.CompareTag("Buildable"))
                //         //     {
                //         //         col.transform.gameObject.GetComponent<Buildable>().TakeDamage(_stats.Damage);
                //         //     }
                //         // }
                //         // col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         // if (col.CompareTag("Player"))
                //         // {
                //         //     col.transform.GetComponent<PlayerController>().TakeDamage(_stats.Damage);
                //         // }
                //     }
                // }
            }
        }

        public void TakeDamage(int dmg)
        {
            if (_stats.CurrentHealth > 0)
            {
                _animator.SetTrigger(_isGettingHitHash);
                _stats.TakeDamage(dmg);
            }
        }

        // private void FaceTarget()
        // {
        //     Vector3 direction = (_target.position - transform.position).normalized;
        //     Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        // }

        private void FaceTarget(GameObject col)
        {
            Vector3 direction = (col.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        /**
         * Draws Gizmos in Editor.
         * Only for development.
         */
        private void OnDrawGizmos()
        {
            var pos = transform.position;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(pos, searchRadius);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(pos, chaseRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pos, attackRadius);
        }
    }
}