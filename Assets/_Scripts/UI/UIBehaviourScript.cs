using _Scripts.Buildings;
using _Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIBehaviourScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyCode;
        [SerializeField] private TextMeshProUGUI lineOne;
        [SerializeField] private TextMeshProUGUI lineTwo;
        [SerializeField] private TextMeshProUGUI lineThree;
        [SerializeField] private TextMeshProUGUI stats;
        [SerializeField] private GameObject player;
        private int _buildingMode;

        void Update()
        {
            var playerController = player.GetComponent<PlayerController>();
            stats.text = "Life Points: " + playerController.Stats.CurrentHealth;

            if (playerController.GetCurrentPlayerMode() == 1)
            {
                keyCode.text = "Exit Build Mode with \"Esc\"";
                lineOne.enabled = true;
                lineTwo.enabled = true;
                lineThree.enabled = true;
                
                if (player.GetComponent<BuildingController>().buildingSelected)
                {
                    _buildingMode = player.GetComponent<BuildingController>().GetCurrentBuildMode();
                    
                    lineOne.color = Color.white;
                    lineTwo.color = Color.white;
                    lineThree.color = Color.white;
                
                    if (_buildingMode == 0)
                    {
                        lineOne.color = Color.red;
                    }
                    else if (_buildingMode == 1)
                    {
                        lineTwo.color = Color.red;
                    }
                    else if (_buildingMode == 2)
                    {
                        lineThree.color = Color.red;
                    }
                }
                else
                {
                    lineOne.color = Color.white;
                    lineTwo.color = Color.white;
                    lineThree.color = Color.white;
                }
            }
            else
            {
                keyCode.text = "Enter Build Mode with \"B\"";
                lineOne.enabled = false;
                lineTwo.enabled = false;
                lineThree.enabled = false;
            }
        }
    }
}