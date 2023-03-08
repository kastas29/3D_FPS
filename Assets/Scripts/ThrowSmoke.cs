using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSmoke : MonoBehaviour
{

    [SerializeField]
    GameObject throweableObject;

    [SerializeField]
    Transform playerCamera;

    [SerializeField]
    int forceNormal, forceLow;

    int forceActual;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            forceActual = forceNormal;
            SoundManager.PlaySound("SAYINGSMOKE");
            toThrowSmoke();
        }else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            forceActual = forceLow;
            SoundManager.PlaySound("SAYINGSMOKE2");
            toThrowSmoke();
        }

    }

    void toThrowSmoke()
    {
        
        Instantiate(throweableObject,transform.position,transform.rotation).GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * forceActual;

    }
}
