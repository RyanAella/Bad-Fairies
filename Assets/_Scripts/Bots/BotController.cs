using System;
using UnityEngine;

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

        private BotMode _mode = BotMode.Default;

        private BotMovement _movement;
        private Animator _animator;

        [SerializeField] private float searchRadius = 10;
        [SerializeField] private float chaseRadius = 5;
        [SerializeField] private float attackRadius = 1;
        [SerializeField] private LayerMask _mask;

        public int isIdlingHash;
        public int isWalkingHash;
        public int isRunningHash;
        public int isAttackingHash;

        // Start is called before the first frame update
        void Start()
        {
            _movement = new BotMovement(gameObject);
            _animator = GetComponent<Animator>();

            isIdlingHash = Animator.StringToHash("idle");
            isWalkingHash = Animator.StringToHash("walking");
            isRunningHash = Animator.StringToHash("running");
            isAttackingHash = Animator.StringToHash("attacking");
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBotMode();

            _animator.SetBool(isAttackingHash, false);
            
            switch (_mode)
            {
                case BotMode.Default:
                    Debug.Log("Bot is in default mode");
                    _animator.SetTrigger(isIdlingHash);
                    // _animator.SetBool(isAttackingHash, false);
                    break;
                case BotMode.SearchEnemy:
                    Debug.Log("Bot is in search mode");
                    _animator.SetTrigger(isWalkingHash);
                    // _animator.SetBool(isAttackingHash, false);
                    // move to a random position which is near the enemy
                    break;
                case BotMode.ChaseEnemy:
                    Debug.Log("Bot is in chase mode");
                    _animator.SetTrigger(isRunningHash);
                    // _animator.SetBool(isAttackingHash, false);
                    // move directly to enemy position
                    break;
                case BotMode.AttackEnemy:
                    Debug.Log("Bot is in attack mode");
                    _animator.SetBool(isAttackingHash, true);
                    // stop movement and attack enemy
                    break;
                case BotMode.Die:
                    Debug.Log("Bot is in die mode");
                    break;
            }
        }

        private void UpdateBotMode()
        {
            _mode = BotMode.Default;

            var pos = transform.position;

            // Check if enemy in search range
            if (Physics.CheckSphere(pos, searchRadius, _mask))
            {
                _mode = BotMode.SearchEnemy;
            }

            // Check if enemy in chase range (and in field of view)
            Collider[] colliders = Physics.OverlapSphere(pos, chaseRadius, _mask);
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
                        }
                    }
                }
            }

            // Check if enemy in attack range
            if (Physics.CheckSphere(pos, attackRadius, _mask))
            {
                _mode = BotMode.AttackEnemy;
            }

            // Check if Bot is dead
            //...
        }

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