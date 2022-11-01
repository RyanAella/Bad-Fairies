using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Bots
{
    public class BotMovement
    {
        private GameObject _bot;
        
        private NavMeshAgent _agent;

        // private Animator _animator;

        // // dirty
        // public Transform pos1;
        // public Transform pos2;

        // public bool isWalking = false;
        // public bool isRunning = false;

        // private Vector3 _destination;

        // Start is called before the first frame update
        public BotMovement(GameObject bot)
        {
            _bot = bot;
            
            _agent = _bot.GetComponent<NavMeshAgent>();
            _agent.updateRotation = true;


            // _destination = pos1.position;
            // _agent.SetDestination(_destination);
            // _animator = GetComponent<Animator>();
        }

        private void Move(Vector3 destination)
        {
            // var botPos2D = new Vector2(_bot.transform.position.x, _bot.transform.position.z);
            // var destination2D = new Vector2(destination.x, destination.z);

            _agent.SetDestination(destination);
        }
    }
}
