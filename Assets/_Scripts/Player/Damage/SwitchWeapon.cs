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
        if (player.GetComponent<PlayerController>().GetPlayerMode() == PlayerController.PlayerMode.Default)
        {
            if (!weaponsEnabled)
            {
                SelectWeapon(currentWeapon);
                weaponsEnabled = true;
            }

            int previousWeapon = currentWeapon;

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

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeapon = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            {
                currentWeapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            {
                currentWeapon = 2;
            }

            if (previousWeapon != currentWeapon)
            {
                SelectWeapon(previousWeapon);
            }
        }
        else
        {
            var weapon = transform.GetChild(currentWeapon).gameObject;
            weapon.SetActive(false);

            weaponsEnabled = false;
            //int i = 0;
            //foreach (Transform weapon in transform)
            //{
            //    if (i == currentWeapon)
            //    {
            //        weapon.gameObject.SetActive(false);
            //    }
            //    i++;
            //}
        }
    }

    /**
     * Set active current weapon, set previous weapon inactive 
     */
    public void SelectWeapon(int previous)
    {
        var weapon = transform.GetChild(previous).gameObject;
        weapon.SetActive(false);
        weapon = transform.GetChild(currentWeapon).gameObject;
        weapon.SetActive(true);

        //int i = 0;
        //foreach (Transform weapon in transform)
        //{
        //    if (i == currentWeapon)
        //    {
        //        weapon.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        weapon.gameObject.SetActive(false);
        //    }
        //    i++;
        //}
    }
}
