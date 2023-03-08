using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RECOIL : MonoBehaviour
{
    Vector3 actual,adonde;


    [Header("Weapon firing variables updated automatically")]
    //CURRENT SPEED MUTIPLIER (BASED ON CURRENT WEAPON WEIGHT)
    public float WeaponMovementSpeedMultiplier;

    //CURRENT SPREAD MULTIPLIER FINAL (BASED ON CURRENT MOVEMENT VELOCITY)
    public float WeaponMovementSpreadMultiplier;

    //CURRENT WEAPON MULTIPLIER VAR (BASED ON WHAT CURRENT WEAPON MAX SPREAD IS)
    public float CurrentWeaponMaxSpreadMutilplier;
    
    
    [Header("Recoil Values - loaded from Weapons")]
    public float inclinacion;
    public float horizontal;
    public float vertical;

    [Header("Recoil Time to Recover Values")]
    [SerializeField]
    float lerp, slerp = 5;



    private void Update()
    {
        updatePosition();
    }
    private void updatePosition()
    {
        adonde = Vector3.Lerp(adonde, Vector3.zero, Time.fixedDeltaTime * lerp);//r
        actual = Vector3.Slerp(actual, adonde, Time.fixedDeltaTime * slerp);//s
        transform.localRotation = Quaternion.Euler(actual);
    }
    public void addRecoil()
    {
        float horizontalRandomRecoil = Random.Range(-horizontal, horizontal);
        float inclinationRandomRecoil = Random.Range(-inclinacion, inclinacion);
        adonde += new Vector3(-vertical, horizontalRandomRecoil, inclinationRandomRecoil);    
    }

    
    
}
