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

        private Stats m_stats;

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
        private static System.Random random;  // used for randomly choosing attack animations

        protected internal Animator _animator;

        protected internal int _isIdlingHash;
        protected internal int _isWalkingHash;
        private int _isRunningHash;
        private int _isAttackingHash;
        private int _attackIndexHash;
        private int _isDyingHash;
        private int _isGettingHitHash;

        [SerializeField] int attackIndexLow;
        [SerializeField] int attackIndexHigh;

        // misc
        private Transform _target;

        private static int _botCounter;

        void Awake()
        {
            _botCounter++;
            random = new System.Random();
        }

        void Start()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = true;

            _agent.stoppingDistance = attackRadius - stopMargin;

            _target = PlayerController.Instance.transform;

            _isIdlingHash = Animator.StringToHash("idle");
            _isWalkingHash = Animator.StringToHash("walking");
            _isRunningHash = Animator.StringToHash("running");
            _isAttackingHash = Animator.StringToHash("attacking");
            _attackIndexHash = Animator.StringToHash("attackIndex");
            _isDyingHash = Animator.StringToHash("dying");
            _isGettingHitHash = Animator.StringToHash("getHit");

            m_stats = GetComponent(typeof(Stats)) as Stats;
        }

        void Update()
        {
            if (!GameManager._gamePaused)
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

            _mode = BotMode.Default;

            // Check if Bot is dead
            if (m_stats.CurrentHealth <= 0)
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
                        _mode = BotMode.ChaseEnemy;
                        return;
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
            return _destination == transform.position;
        }

        /**
         * Behaviour in BotMode Default.
         *
         * Either stand still and idle, or choose random point within range and walk towards it.
         */
        private void Idling()
        {
            _animator.SetTrigger(_isIdlingHash);
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
                    // If destination not yet reached and nothing in searchRadius
                    else if (!DestinationReached() && !Physics.CheckSphere(pos, searchRadius, mask))
                    {
                        _animator.SetTrigger(_isWalkingHash);
                        Move(_destination);
                    }
                    // If destination not yet reached and something in searchRadius
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
        }

        private void Chase()
        {
            _animator.SetTrigger(_isRunningHash);

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
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
            {
                // prepare attack animation to be used by randomly choosing a attackIndex
                var attackIndex = random.Next(attackIndexLow, attackIndexHigh);

                // set attackIndex and enable attack animation
                _animator.SetInteger(_attackIndexHash, attackIndex);
                _animator.SetBool(_isAttackingHash, true);

                /* 
                     may keep facing target while playing the animation
                */
                // FaceTarget();
            }
        }

        private void Die()
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
            {
                _animator.SetTrigger(_isDyingHash);
            }
        }

        private void DeathAnimationDonePlaying()
        {
            BotFactory.RemoveBotFromList(gameObject);

            Destroy(gameObject);
        }

        private void ApplyDamageToTarget()
        {
            /*
                do not just check for attack range but also for field of view
            */

            Collider[] attack = Physics.OverlapSphere(transform.position, attackRadius, mask);
            if (attack.Length > 0)
            {
                Stats targetStats = attack[0].transform.GetComponent(typeof(Stats)) as Stats;
                if (targetStats != null) targetStats.TakeDamage(m_stats.Damage);
            }

            // disable continous attack animation if nothing in attack range
            _animator.SetBool(_isAttackingHash, false);
        }

        public void TakeDamage(int dmg)
        {
            if (m_stats.CurrentHealth > 0)
            {
                _animator.SetTrigger(_isGettingHitHash);
                m_stats.TakeDamage(dmg);
            }
        }

        // private void FaceTarget()
        // {
        //     Vector3 direction = (_target.position - transform.position).normalized;
        //     Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        // }

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