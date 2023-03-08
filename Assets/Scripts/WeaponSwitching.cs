using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    int numberOfWeapons;
    int currentweapon;

    // Start is called before the first frame update
    void Start()
    {
        numberOfWeapons=transform.childCount;
        currentweapon=0;
        SelectWeapon(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            //arriba
            if(numberOfWeapons>currentweapon+1)
                SelectWeapon(currentweapon+1);

        }else if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            //abajo
            if(numberOfWeapons>=currentweapon-1 && currentweapon>0)
                SelectWeapon(currentweapon-1);

        }else if(Input.GetKeyDown(KeyCode.Alpha1)){
            if(numberOfWeapons>=3)
                SelectWeapon(2);
        }
            
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            if(numberOfWeapons>=2)
                SelectWeapon(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            if(numberOfWeapons>=1)
                SelectWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (numberOfWeapons >= 4)
                SelectWeapon(3);
        }


    }


    void SelectWeapon (int index) {
        if(currentweapon!=index){
            for (var i=0;i<transform.childCount;i++)    {
                // Activate the selected weapon
                if (i == index){

                    //To Fix Reload on Changing and Knife cooldown on Changing
                    if (transform.GetChild(currentweapon).tag == "Weapon")
                    {
                        transform.GetChild(currentweapon).gameObject.GetComponent<ShootingSystem>().StopAllCoroutines();
                        transform.GetChild(currentweapon).gameObject.GetComponent<ShootingSystem>().Reloading = false;
                    }
                    else if (transform.GetChild(currentweapon).tag == "Knife")
                    {
                        transform.GetChild(currentweapon).gameObject.GetComponent<KnifeManager>().StopAllCoroutines();
                        transform.GetChild(currentweapon).gameObject.GetComponent<KnifeManager>().canAttack = true;
                    }
                        

                    transform.GetChild(i).gameObject.SetActive(true);

                    if (transform.GetChild(i).tag== "Weapon")
                    {
                        transform.GetChild(i).gameObject.GetComponent<ShootingSystem>().SetWeaponVariablesToHolder();
                        transform.GetChild(i).gameObject.GetComponent<ShootingSystem>().UpdateAmmoUI();
                    }
                    else//cuchillos y throweables
                    {
                        this.GetComponent<RECOIL>().WeaponMovementSpeedMultiplier = 1.4f;
                    }
                    
                }
                    
                // Deactivate all other weapons
                else
                    transform.GetChild(i).gameObject.SetActive(false);
            }
            //Debug.Log("ARMA"+index);
            currentweapon=index;
        }
        
   
    }
}
