using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWeapon : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private bool setToggle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleWeaponItem();
        }
    }
    private void ToggleWeaponItem()
    {
        setToggle = !setToggle;

        if (setToggle)
        {
            weapon.SetActive(true);
        }
        else
        {
            weapon.SetActive(false);
        }
    }
}
