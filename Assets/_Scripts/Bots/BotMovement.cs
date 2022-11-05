using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Bots
{
    public class BotMovement
    {
        public bool destinationReached = false;
        
        private NavMeshAgent _agent;
        private GameObject _bot;
        private float _stopMargin = 0.1f;

        // Start is called before the first frame update
        public BotMovement(GameObject bot)
        {
            _bot = bot;
            
            _agent = _bot.GetComponent<NavMeshAgent>();
            _agent.updateRotation = true;

            _agent.stoppingDistance = _bot.GetComponent<BotController>().attackRadius - _stopMargin;
        }

        public void Move(Vector3 destination)
        {
            // var botPos2D = new Vector2(_bot.transform.position.x, _bot.transform.position.z);
            // var destination2D = new Vector2(destination.x, destination.z);
        
            _agent.SetDestination(destination);
            if (destination == _bot.transform.position)
            {
                DestinationReached();
            }
        }

        public bool DestinationReached()
        {
            destinationReached = true;
            return destinationReached;
        }
    }
}
