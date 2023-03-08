using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SmokeBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject smoke;

    bool firstHit;

    private void Start()
    {
        firstHit = false;
        SoundManager.PlaySound("LANZAR");
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!firstHit)
        {
            firstHit = true;
            StartCoroutine(ActivateSmoke());
        }
        SoundManager.PlaySound("IMPACTO");
        
    }

    IEnumerator ActivateSmoke()
    {
        yield return new WaitForSeconds(2.4f);
        GameObject spawnedSmoke = Instantiate(smoke, this.transform.position, Quaternion.identity);
        SoundManager.PlaySound("SALIENDOSMOKE");
        this.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(14f);
        Destroy(spawnedSmoke.gameObject);
        Destroy(this.gameObject);

    }
}

