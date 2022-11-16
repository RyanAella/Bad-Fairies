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

        [SerializeField] private float searchRadius = 10;
        [SerializeField] private float chaseRadius = 5;
        [SerializeField] public float attackRadius = 1;
        [SerializeField] private LayerMask mask;

        // bot movement
        [SerializeField] private float stopMargin = 0.1f;
        private NavMeshAgent _agent;
        private Vector3 _destination;

        // bot animation
        private Animator _animator;

        private int _isIdlingHash;
        private int _isWalkingHash;
        private int _isRunningHash;
        private int _isAttackingHash;
        private int _isDyingHash;
        private int _isGettingHitHash;

        // misc
        private Transform _target;

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
            _isGettingHitHash = Animator.StringToHash("gettingHit");

            _stats = new BotStats(50, 50, 5, 0);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBotMode();

            _animator.SetBool(_isAttackingHash, false);

            _destination = transform.position;

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

        /**
         * Sets BotMode according to defined criteria.
         * Sets BotMode according to either distance to player or bot life.
         */
        private void UpdateBotMode()
        {
            var pos = transform.position;
            // _animator.SetBool(_isAttackingHash, false);

            _mode = BotMode.Default;

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
            Collider[] colliders = Physics.OverlapSphere(pos, chaseRadius, mask);
            if (colliders.Length > 0)
            {
                foreach (var col in colliders)
                {
                    if (col.CompareTag("Player"))
                    {
                        var targetDir = col.transform.position - pos;
                        var angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

                        if (angleToPlayer is >= -60 and <= 60) // 120° FOV 
                        {
                            Debug.Log("Player in sight!");
                            _mode = BotMode.ChaseEnemy;
                            return;
                        }
                    }
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
        }

        private void Search()
        {
            _animator.SetTrigger(_isWalkingHash);

            var pos = transform.position;

            // Debug.Log("Bot is in search mode");

            if (Physics.CheckSphere(pos, searchRadius, mask))
            {
                Move(_target.position);
            }

            if (_destination != transform.position /* && Physics.CheckSphere(pos, searchRadius, mask)*/)
            {
                Move(_target.position);
            }
            else if (!DestinationReached() && !Physics.CheckSphere(pos, searchRadius, mask))
            {
                _animator.SetTrigger(_isWalkingHash);
                Move(_target.position);
            }
            else
            {
                _destination = transform.position;
            }
        }

        private void Chase()
        {
            // Debug.Log("Bot is in chase mode");
            _animator.SetTrigger(_isRunningHash);
            Move(_target.position);
        }

        private void Attack()
        {
            // Debug.Log("Bot is in attack mode");
            _animator.SetBool(_isAttackingHash, true);
            FaceTarget();
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

        public void TakeDamage(int dmg)
        {
            _stats.TakeDamage(dmg);
        }

        private void FaceTarget()
        {
            Vector3 direction = (_target.position - transform.position).normalized;
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