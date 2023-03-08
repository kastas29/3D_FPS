using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.VFX;

public class ShootingSystem : MonoBehaviour
{

    public bool isBot = false;

    [Header("RECOIL")]
    RECOIL recoilScript;

    [Header("HUD References")]
    public TMPro.TextMeshProUGUI MagazineNumber;
    public TMPro.TextMeshProUGUI ExtraNumber;

    [Header("Particles")]
    public ParticleSystem MuzzleFlash;
    public ParticleSystem HitSparkle;
    public VisualEffect bloodEffect;

    [Header("Weapons")]
    [SerializeField]
    private WeaponInfo weapon;

    [Header("Raycast")]
    //Where PlayerCameraIsLookingAt
    [SerializeField]
    private Transform PlayerLookingTo;

    //What bullet hits
    RaycastHit WhatIsShooted;

    //CurrentSpreadMultiplier, changes on move
    float CurrentSpreadMultiplier=0;

    //Animator
    Animator m_animator;

    //PrimaryWeaponAmmo
    int MagazineAmmoLeft;
    int ExtraAmmoLeft;
    int bulletsToShotThisBurst;

    // ShootingStates
    bool m_Shooting = false;
    bool m_Reloading = false;
    bool m_CanShoot =true;

    //Getters-Setters
    public WeaponInfo Weapon { get => weapon; set => weapon = value; }
    public bool Shooting { get => m_Shooting; set => m_Shooting = value; }

    public bool Reloading { get => m_Reloading; set => m_Reloading = value; }

    
    [SerializeField] CinemachineVirtualCamera ScopeCam;
    //Updates ammo values on UI
    public void UpdateAmmoUI()
    {
        MagazineNumber.text = "" + MagazineAmmoLeft;
        ExtraNumber.text = "" + ExtraAmmoLeft;
    }

    //Set the info from the weapon to holder
    public void SetWeaponVariablesToHolder()
    {
        //Set current weapon SpeedMultiplier(Weight) on RecoilScript
        recoilScript.WeaponMovementSpeedMultiplier=weapon.movementSpeedMultipler;
        //Set current weapon MaxSpreadMultiplier(max Fire error on move) on RecoilScript
        recoilScript.CurrentWeaponMaxSpreadMutilplier=weapon.movementSpreadMultipler;

        //Set current weapon RECOIL(Camera Movement) on RecoilScript
        recoilScript.horizontal=weapon.horizontalRecoil;
        recoilScript.vertical=weapon.verticalRecoill;
        recoilScript.inclinacion = weapon.inclinationRecoil;
    }

    private void Awake()
    {
        //Gets Animator to Play Animations
        m_animator = GetComponentInChildren<Animator>();

        //Gets the Recoil
        recoilScript = GetComponentInParent<RECOIL>();

        //loads on script ammo info of the weapon used
        MagazineAmmoLeft = weapon.magazineSize;
        ExtraAmmoLeft = weapon.extraAmmo;

        if (!isBot)
            UpdateAmmoUI();
        
        SetWeaponVariablesToHolder();
    }
    private void Update()
    {
        //use UpdateStates(used by player)
        if (!isBot)
            UpdateStates();
    }

    private void UpdateStates()
    {
        //gets the current spreadMultiplier given by recoilScript
        CurrentSpreadMultiplier=recoilScript.WeaponMovementSpreadMultiplier;

        // UPDATE SHOOTING BOOLEAN
        // AUTO - if weapon is auto it'll just check if mouse button is pressed
        // SEMI - if weapon is not auto it'll check if player tapped it
        if (weapon.IsWeaponAuto)
            m_Shooting = Input.GetKey(KeyCode.Mouse0);
        else
            m_Shooting = Input.GetKeyDown(KeyCode.Mouse0);


        // call ToReload() function if magazine isn't full, player isn't reloading and has at least 1 extra bullet remaining
        if (Input.GetKeyDown(KeyCode.R) && MagazineAmmoLeft < weapon.magazineSize && !m_Reloading && ExtraAmmoLeft>0)
            ToReload();

        // call ToShoot() function if player isn't reloading, magazine>1bullet, canShoot and is trying to shoot
        if (!m_Reloading && MagazineAmmoLeft > 0 && m_CanShoot && m_Shooting)
        {
            bulletsToShotThisBurst = weapon.bulletsFiredByShot;
            ToShoot();
        }else if (Shooting && MagazineAmmoLeft == 0 && !m_Reloading && ExtraAmmoLeft > 0)
                ToReload();

        if (Input.GetKey(KeyCode.Mouse1) && !Reloading && !Shooting && Weapon.weaponSound == "AWP")
        {
            ScopeCam.Priority = 3;
       }else if(Weapon.weaponSound=="AWP")
            ScopeCam.Priority = 1;


    }

    //Custom UpdateStates for BOT, because bot can't press buttons and has to reload when has 0 bullets on the magazine
    public void UpdateStatesBot()
    {
        //gets the current spreadMultiplier given by recoilScript
        CurrentSpreadMultiplier = recoilScript.WeaponMovementSpreadMultiplier;

        // call ToReload() function if magazine is empty, isn't already reloading and has at least 1 extra bullet remaining
        if (MagazineAmmoLeft == 0 && !m_Reloading && ExtraAmmoLeft > 0)
            ToReload();

        // call ToShoot() function if BOT isn't reloading, magazine>1bullet, canShoot and is trying to shoot
        if (!m_Reloading && MagazineAmmoLeft > 0 && m_CanShoot && Shooting)
        {
            bulletsToShotThisBurst = Weapon.bulletsFiredByShot;
            ToShoot();
        }
        Shooting = false;
    }


    private void ToReload()
    {
        m_Reloading = true;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        SoundManager.PlaySound(weapon.weaponSound+"R");
        if (weapon.name != "NOVA")
        {
            if (MagazineAmmoLeft == 0)
                m_animator.Play("RELOAD0");
            else
                m_animator.Play("RELOAD");


            yield return new WaitForSeconds(weapon.reloadTime);
            // if you need more or the exact quantity of ammo left, just reload all the remaining left and set it to 0
            if ((weapon.magazineSize - MagazineAmmoLeft) >= ExtraAmmoLeft)
            {
                MagazineAmmoLeft += ExtraAmmoLeft;
                ExtraAmmoLeft = 0;
            }
            else // si tienes mas de la que necesitas para llenar el cargador, lo haces y restas la recargada de la extra
            {
                ExtraAmmoLeft -= (weapon.magazineSize - MagazineAmmoLeft);
                MagazineAmmoLeft = weapon.magazineSize;
            }
        }
        else//SI ES LA ESCOPETA CON CARTUCHOS INDEPENDIENTES
        {
            m_animator.Play("RELOAD");
            yield return new WaitForSeconds(weapon.reloadTime);
            ExtraAmmoLeft--;
            MagazineAmmoLeft++;
        }

        if (!isBot)
            UpdateAmmoUI();
        
        m_Reloading = false;
    }

    private void CanShootAgain()
    {
        m_CanShoot = true;
    }
    private void ToShoot()
    {
        m_CanShoot = false;
        MuzzleFlash.Play();
        m_animator.Play("FIRE");
        SoundManager.PlaySound(weapon.weaponSound);

        
        if (weapon.pelletWeapon)
            for(int i=0;i< weapon.pellets; i++)
            {
                float spreadH = Random.Range(-weapon.horizontalSpread, weapon.horizontalSpread);
                float spreadV = Random.Range(-weapon.verticalSpread, weapon.verticalSpread);
                if (Physics.Raycast(PlayerLookingTo.position, PlayerLookingTo.forward + new Vector3(spreadH, spreadV, spreadH) *CurrentSpreadMultiplier, out WhatIsShooted, 50)) { 
                    Instantiate(HitSparkle, WhatIsShooted.point, Quaternion.LookRotation(WhatIsShooted.normal));
                   
                    // Apply the weapon damage to the enemy
                    if (WhatIsShooted.transform.tag == "Enemy")
                    {
                        WhatIsShooted.transform.gameObject.GetComponent<EnemyScript>().ReceiveDamage(weapon.damage);
                        Instantiate(bloodEffect, WhatIsShooted.point, Quaternion.LookRotation(WhatIsShooted.normal));
                    }
                        
                    else if (WhatIsShooted.transform.tag == "Ball")
                    {
                        WhatIsShooted.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-WhatIsShooted.normal * 1000, WhatIsShooted.point);
                    }
                }
            }
        else
        {
            float  spreadH = Random.Range(-weapon.horizontalSpread, weapon.horizontalSpread);
            float  spreadV = Random.Range(-weapon.verticalSpread, weapon.verticalSpread);
            if (Physics.Raycast(PlayerLookingTo.position, PlayerLookingTo.forward + new Vector3(spreadH, spreadV, spreadH) *CurrentSpreadMultiplier, out WhatIsShooted, 50)) { 
                Instantiate(HitSparkle, WhatIsShooted.point, Quaternion.LookRotation(WhatIsShooted.normal));

                // Apply the weapon damage to the enemy
                if (WhatIsShooted.transform.tag == "Enemy")
                {
                    WhatIsShooted.transform.gameObject.GetComponent<EnemyScript>().ReceiveDamage(weapon.damage);
                    Instantiate(bloodEffect, WhatIsShooted.point, Quaternion.LookRotation(WhatIsShooted.normal));
                }

                else if (WhatIsShooted.transform.tag == "Ball")
                {
                    WhatIsShooted.transform.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-WhatIsShooted.normal * 1000, WhatIsShooted.point);
                }
            }
        }

        this.GetComponentInParent<RECOIL>().addRecoil();
        MagazineAmmoLeft--;
        bulletsToShotThisBurst--;
        
        if (!isBot)
            UpdateAmmoUI();

        if (bulletsToShotThisBurst > 0 && MagazineAmmoLeft > 0)
            Invoke("ToShoot", weapon.timeBetweenBullets); 
        else
            Invoke("CanShootAgain", weapon.timeBetweenShootAction);
    }


}
