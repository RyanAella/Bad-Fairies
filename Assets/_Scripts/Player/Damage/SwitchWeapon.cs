using _Scripts;
using _Scripts.Player;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private int currentWeapon = 0;
    private bool weaponsEnabled = true;

    void Start()
    {
        if (player.GetComponent<PlayerController>().GetPlayerMode() == PlayerController.PlayerMode.Default)
        {
            SelectWeapon(currentWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager._gamePaused)
        {
            // If player mode is Default
            if (player.GetComponent<PlayerController>().GetPlayerMode() == PlayerController.PlayerMode.Default)
            {
                // If weapons are not enabled, enable currentWeapon
                if (!weaponsEnabled)
                {
                    SelectWeapon(currentWeapon);
                    weaponsEnabled = true;
                }

                int previousWeapon = currentWeapon;

                // Switch weapons with the mouse wheel (upwards)
                if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                {
                    if (currentWeapon >= transform.childCount - 1)
                    {
                        currentWeapon = 0;
                    }
                    else
                    {
                        currentWeapon++;
                    }

                }
                // Switch weapons with the mouse wheel (downwards)
                if (Input.GetAxis("Mouse ScrollWheel") < 0f)
                {
                    if (currentWeapon <= 0)
                    {
                        currentWeapon = transform.childCount - 1;
                    }
                    else
                    {
                        currentWeapon--;
                    }
                }

                // Choose weapon by pressing 1 or 2
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    currentWeapon = 0;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
                {
                    currentWeapon = 1;
                }

                if (previousWeapon != currentWeapon)
                {
                    SelectWeapon(previousWeapon);
                }
            }
            // If player mode is BuildMode
            else
            {
                var weapon = transform.GetChild(currentWeapon).gameObject;
                weapon.SetActive(false);

                weaponsEnabled = false;
            }
        }
    }

    /**
     * Set active currentWeapon, set previousWeapon inactive 
     */
    public void SelectWeapon(int previous)
    {
        var weapon = transform.GetChild(previous).gameObject;
        weapon.SetActive(false);
        weapon = transform.GetChild(currentWeapon).gameObject;
        weapon.SetActive(true);
    }
}
