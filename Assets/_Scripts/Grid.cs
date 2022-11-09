using System;
using UnityEngine;

namespace _Scripts
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private float gridSize = 1f;
        
        // x and z Coordinates
        private float _borderLeft, _borderRight;
        private float _borderTop, _borderBottom;

        private void Awake()
        {
            CalculateBorders();
        }

        public Vector3 getNearestPointOnGrid(Vector3 position)
        {
            position -= transform.position;

            var xCount = Mathf.RoundToInt(position.x / gridSize);
            var yCount = Mathf.RoundToInt(position.y / gridSize);
            var zCount = Mathf.RoundToInt(position.z / gridSize);

            var result = new Vector3((float)xCount * gridSize, (float)yCount * gridSize, (float)zCount * gridSize);

            result += transform.position;

            return result;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            for (float x = _borderLeft; x < _borderRight + gridSize; x += gridSize)
            {
                for (float z = _borderBottom; z < _borderTop + gridSize; z += gridSize)
                {
                    var point = getNearestPointOnGrid(new Vector3(x, 0f, z));
                    Gizmos.DrawSphere(point, 0.1f);
                }
            }
        }
    
        private void CalculateBorders()
        {
            var ground = GameObject.Find("Ground");

            // calculate left, right, top and bottom edge of the playfield
            var pos = ground.transform.position;
            var scale = ground.transform.localScale;

            _borderLeft = pos.x - (scale.x * 10 / 2);
            _borderRight = pos.x + (scale.x * 10 / 2);
            _borderBottom = pos.z - (scale.z * 10 / 2);
            _borderTop = pos.z + (scale.z * 10 / 2);
        }
    }
}
