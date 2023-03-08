using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchSystem : MonoBehaviour
{
    private CharacterController ch;
    private float startHeight; // initial height
 
    void Start()
    {
        ch = this.GetComponent<CharacterController>();
        startHeight = ch.height;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ch.height = Mathf.Lerp(ch.height, 0.6f, 85 * Time.deltaTime);
            //transform.position += new Vector3(0, -((2 - 1f) * 0.5f), 0);
        }
            
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ch.height = Mathf.SmoothStep(ch.height, startHeight, 130 * Time.deltaTime);
            //ch.height = Mathf.Lerp(ch.height, startHeight, 130 * Time.deltaTime);
            //transform.position += new Vector3(0, -((1f - 2) * 0.5f), 0);
        }  

        /*
         newH = 0.1f * startHeight;

          var lastHeight = ch.height;
          ch.height = Mathf.Lerp(ch.height, newH, 5 * Time.deltaTime);
          transform.position += new Vector3(0, (ch.height - lastHeight * 0.5f), 0);
         */

    }
}
