using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip AWP, AWPR,AK47, AK47R, AA12, AA12R,GLOCK,GLOCKR,NOVA,NOVAR,MP5,MP5R,M4A4,M4A4R,
        LANZAR, SALIENDOSMOKE, IMPACTO,SAYINGSMOKE, SAYINGSMOKE2,
        CATWEAPON, CATWEAPONR, CATWEAPONR2, DEATH;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        AWP = Resources.Load<AudioClip>("AWP");
        AWPR = Resources.Load<AudioClip>("AWPR");
        AK47 = Resources.Load<AudioClip>("AK47");
        AK47R = Resources.Load<AudioClip>("AK47R");
        AA12 = Resources.Load<AudioClip>("AA12");
        AA12R = Resources.Load<AudioClip>("AA12R");
        GLOCK = Resources.Load<AudioClip>("GLOCK");
        GLOCKR = Resources.Load<AudioClip>("GLOCKR");
        NOVA = Resources.Load<AudioClip>("NOVA");
        NOVAR = Resources.Load<AudioClip>("NOVAR");
        LANZAR = Resources.Load<AudioClip>("LANZAR");
        SALIENDOSMOKE = Resources.Load<AudioClip>("SALIENDOSMOKE");
        IMPACTO = Resources.Load<AudioClip>("IMPACTO");
        SAYINGSMOKE = Resources.Load<AudioClip>("SAYINGSMOKE");
        SAYINGSMOKE2 = Resources.Load<AudioClip>("SAYINGSMOKE2");
        CATWEAPON = Resources.Load<AudioClip>("CATWEAPON");
        CATWEAPONR = Resources.Load<AudioClip>("CATWEAPONR");
        CATWEAPONR2 = Resources.Load<AudioClip>("CATWEAPONR2");
        DEATH = Resources.Load<AudioClip>("DEATH");
        MP5 = Resources.Load<AudioClip>("MP5");
        MP5R = Resources.Load<AudioClip>("MP5R");
        M4A4 = Resources.Load<AudioClip>("M4A4");
        M4A4R = Resources.Load<AudioClip>("M4A4R");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string sound)
    {
        switch (sound)
        {  case "MP5":
                audioSrc.PlayOneShot(MP5);
                break;
            case "MP5R":
                audioSrc.PlayOneShot(MP5R);
                break;
            case "M4A4":
                audioSrc.PlayOneShot(M4A4);
                break;
            case "M4A4R":
                audioSrc.PlayOneShot(M4A4R);
                break;
            case "AWP":
                audioSrc.PlayOneShot(AWP);
                break;
            case "AWPR":
                audioSrc.PlayOneShot(AWPR);
                break;
            case "AK47":
                audioSrc.PlayOneShot(AK47);
                break;
            case "AK47R":
                audioSrc.PlayOneShot(AK47R);
                break;
            case "AA12":
                audioSrc.PlayOneShot(AA12);
                break;
            case "AA12R":
                audioSrc.PlayOneShot(AA12R);
                break;
            case "GLOCKR":
                audioSrc.PlayOneShot(GLOCKR);
                break;
            case "GLOCK":
                audioSrc.PlayOneShot(GLOCK);
                break;
            case "NOVA":
                audioSrc.PlayOneShot(NOVA);
                break;
            case "NOVAR":
                audioSrc.PlayOneShot(NOVAR);
                break;
            case "LANZAR":
                audioSrc.PlayOneShot(LANZAR);
                break;
            case "SAYINGSMOKE":
                audioSrc.PlayOneShot(SAYINGSMOKE);
                break;
            case "SAYINGSMOKE2":
                audioSrc.PlayOneShot(SAYINGSMOKE2);
                break;
            case "IMPACTO":
                audioSrc.PlayOneShot(IMPACTO);
                break;
            case "SALIENDOSMOKE":
                audioSrc.PlayOneShot(SALIENDOSMOKE);
                break;
            case "CATWEAPON":
                audioSrc.PlayOneShot(CATWEAPON);
                break;
            case "CATWEAPONR":
                audioSrc.PlayOneShot(CATWEAPONR);
                audioSrc.PlayOneShot(CATWEAPONR2);
                break;
            case "DEATH":
                audioSrc.PlayOneShot(DEATH);
                break;
        }
    }
}
