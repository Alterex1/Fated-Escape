using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoManager : MonoBehaviour
{
    public Text[] ammo = new Text[3];

    public GameObject[] weaponIndicator = new GameObject[3];

    private void Start()
    {
        
    }

    public void setammo(string i)
    {
        for (int y = 0; y < ammo.Length; y++) 
        {
            ammo[y].text = i;
        }
    }

    public void setWeaponToDisplay(int e)
    {
        for (int i = 0; i < weaponIndicator.Length; i++)
        {
            if (i == e)
            {
                weaponIndicator[i].SetActive(true);
            }
            else
                weaponIndicator[i].SetActive(false);
        }
    }

}
