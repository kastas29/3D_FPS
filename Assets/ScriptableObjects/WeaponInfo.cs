using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/WeaponInfo")]
public class WeaponInfo : ScriptableObject
{
    

    [Header("WeaponInfo")]
    public int damage;
    public float timeBetweenBullets; // time between each bullet shooted
    public float timeBetweenShootAction; // time waited between each burst
    public int bulletsFiredByShot; // amount of bullets fired each shot
    public bool IsWeaponAuto;
    public string weaponSound;
    public bool pelletWeapon; // shotgun 
    public int pellets; // shotgun pellets per shot
    

    [Header("Ammo")]
    public int magazineSize;
    public int extraAmmo;
    public float reloadTime;

    [Header("MovementSpeed")]
    public float movementSpeedMultipler; // 0.2-1 * player velocity depending on weapon weight 

    [Header("Spread")]
    public float horizontalSpread;
    public float verticalSpread;
    public float movementSpreadMultipler;

    [Header("Recoil")]
    public float horizontalRecoil;
    public float verticalRecoill;
    public float inclinationRecoil;


}
