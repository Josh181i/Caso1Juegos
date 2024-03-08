using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    [SerializeField]
    AttackController[] weapons;

    AttackController _currentWeapon;

    int _selectedWeapon;

    private void Start()
    {
        _selectedWeapon = 0;
        SelectWeapon();
    }

    private void Update()
    {
        HandleScroolWheel();
        HandleAttack();
    }

    private void HandleScroolWheel()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0F)
        {
            _selectedWeapon = scrollWheel > 0.0F
                ? _selectedWeapon + 1
                : _selectedWeapon - 1;

            if (_selectedWeapon >= weapons.Length)
            {
                _selectedWeapon = 0;
            }
            else if (_selectedWeapon < 0)
            {
                _selectedWeapon = weapons.Length - 1;
            }

            SelectWeapon();
        }
    }

    private void HandleAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            _currentWeapon.Attack();
        }
    }

    private void SelectWeapon()
    {
        for (int index = 0; index < weapons.Length; index++)
        {
            AttackController controller = weapons[index];
            bool isActive = (_selectedWeapon == index);
            controller.gameObject.SetActive(isActive);
        }

        _currentWeapon = weapons[_selectedWeapon];
    }
}