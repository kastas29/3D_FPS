using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BombManager : MonoBehaviour
{

    float plantingTime;
    public GameObject bomb;
    public GameObject plantingUI;
    public Slider plantingSlider;
    public TextMeshProUGUI secondsText;

    bool isBombPlanted;

    private void Awake()
    {
        isBombPlanted = false;
        plantingTime = 3;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isBombPlanted)
        {
            PlantBomb();
        }
    }

    public void PlantBomb()
    {
        // Crouch function
        
        plantingUI.SetActive(true);
        StartCoroutine("PlantUI");
    }

    IEnumerator PlantUI()
    {
        plantingSlider.value = 0;
        while (plantingTime > 0)
        {
            yield return new WaitForSeconds(0.1f);
            plantingSlider.value += 0.1f;
            plantingTime -= 0.1f;
            secondsText.text = plantingTime.ToString("0.0").Replace(".",":");
        }
        GameObject playerPos = GameObject.Find("PlayerFPCamera");
        GameObject bombPlanted = Instantiate(bomb,new Vector3(playerPos.transform.position.x, playerPos.transform.position.y - 1.4f, playerPos.transform.position.z), Quaternion.identity);
        bombPlanted.transform.Rotate(new Vector3(270,0,0));
        plantingSlider.value = 0;
        plantingUI.SetActive(false);
        isBombPlanted = true;
    }

}
