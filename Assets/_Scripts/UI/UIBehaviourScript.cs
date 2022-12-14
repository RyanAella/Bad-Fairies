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
            if (!GameManager._gamePaused)
            {
                var playerController = player.GetComponent(typeof(PlayerController)) as PlayerController;
                var playerStats = player.GetComponent(typeof(Stats)) as Stats;

                // Show the current player health in the UI
                stats.text = "Life Points: " + playerStats.CurrentHealth;

                // If BuildMode is active
                if (playerController.GetPlayerMode() == PlayerController.PlayerMode.BuildMode)
                {
                    // Show the buidling info in the UI
                    keyCode.text = "Exit Build Mode with \"B\"";
                    lineOne.enabled = true;
                    lineTwo.enabled = true;
                    lineThree.enabled = true;

                    // If a building is selected
                    if (player.GetComponent<BuildingController>().buildingSelected)
                    {
                        _buildingMode = player.GetComponent<BuildingController>().GetCurrentBuildMode();

                        lineOne.color = Color.white;
                        lineTwo.color = Color.white;
                        lineThree.color = Color.white;

                        // Show which building is selected
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
}