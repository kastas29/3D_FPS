using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class KnifeManager : MonoBehaviour
{

    public bool canAttack;
    Animator m_animator;
    RaycastHit WhatIsStabbed;


    [Header("Time")]
    public float fastAttackCooldown = 0.75f;
    public float hardAttackCooldown = 1.25f;


    [Header("Raycast")]
    [SerializeField]
    private Transform PlayerLookingTo;

    [Header("Effects")]
    public ParticleSystem HitSparkle;
    public VisualEffect bloodEffect;

    void Awake()
    {
        m_animator = GetComponentInChildren<Animator>();
        canAttack = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            playFastAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && canAttack)
        {
            playHardAttack();
        }
    }

    void playFastAttack()
    {
        // Throw raycast to detect if the attack has hit anyone
        if (Physics.Raycast(PlayerLookingTo.position, PlayerLookingTo.forward, out WhatIsStabbed, 1.9f))
        {
            if (WhatIsStabbed.transform.tag == "Enemy")
            {
                WhatIsStabbed.transform.GetComponent<EnemyScript>().ReceiveDamage(50);
                Instantiate(bloodEffect, WhatIsStabbed.point, Quaternion.LookRotation(WhatIsStabbed.normal));
            }
            else
                Instantiate(HitSparkle, WhatIsStabbed.point, Quaternion.LookRotation(WhatIsStabbed.normal));
        }
        m_animator.Play("Knife1");
        StartCoroutine(CooldownFastAttack());
    }

    void playHardAttack()
    {
        if (Physics.Raycast(PlayerLookingTo.position, PlayerLookingTo.forward, out WhatIsStabbed, 1.9f))
        {
            if (WhatIsStabbed.transform.tag == "Enemy")
            {
                WhatIsStabbed.transform.GetComponent<EnemyScript>().ReceiveDamage(100);
                Instantiate(bloodEffect, WhatIsStabbed.point, Quaternion.LookRotation(WhatIsStabbed.normal));
            }
            else
                Instantiate(HitSparkle, WhatIsStabbed.point, Quaternion.LookRotation(WhatIsStabbed.normal));
        }
        m_animator.Play("Knife2");
        StartCoroutine(CooldownHardAttack());

    }

    IEnumerator CooldownFastAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(fastAttackCooldown);
        canAttack = true;
    }

    IEnumerator CooldownHardAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(hardAttackCooldown);
        canAttack = true;
    }

}
