using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    
    public int ammo;
    public bool isFiring;
    public bool reload = false;
    public float reloadTime = 2.5f;
    public TextMeshProUGUI ammoDisplay;
    
    

    void Start() 
    {
        
    }

    void Update()
    {
        ammoDisplay.text = ammo.ToString();
        if (Input.GetMouseButtonDown(0) && !isFiring && ammo > 0)
        {
            isFiring = true;
            ammo--;
            isFiring = false;
            if (ammo == 0 && !reload)
            {
                StartCoroutine(Reload());
               
            }
        }
        
    }

    IEnumerator Reload()
    {
        
            reload = true;
            yield return new WaitForSeconds(reloadTime);
            ammo = 12;
            reload = false;
            

    }
}
