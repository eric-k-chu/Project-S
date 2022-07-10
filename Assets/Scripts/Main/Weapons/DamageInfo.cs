using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo : MonoBehaviour
{
    [SerializeField] private WeaponData weapon;

    public float getDamage()
    {
        return weapon.damage;
    }
}
