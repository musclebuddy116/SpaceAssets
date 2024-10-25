using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class AmmoWindow : MonoBehaviour
{

    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] TextMeshProUGUI ammoText;
    
    [Header("Reload Bar")]
    [SerializeField] Transform barTransform;
    [SerializeField] List<GameObject> hideableBarObjects;

    void SetReloadProgress(float progress) { //0.0 - 1.0
        barTransform.localScale = new Vector3 (progress, barTransform.localScale.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        int ammoAmount = playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetAmmo();
        int maxAmmo = playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetMaxAmmo();
        float fraction = ((float)ammoAmount/(float)maxAmmo);
        ammoText.text = "AMMO      " + ammoAmount.ToString();
        ammoText.color = Color.Lerp(Color.yellow, Color.white, fraction);
        if(ammoAmount == 0) {
            ammoText.color = Color.red;
        }

        
        float reloadProgress = playerInputHandler.GetPlayerShip().GetProjectileLauncher().GetReloadPercentage();
        SetReloadProgress(reloadProgress);
        if(reloadProgress <= 0) {
            foreach(GameObject g in hideableBarObjects) {
                g.SetActive(false);
            }
        } else {
            foreach(GameObject g in hideableBarObjects) {
                g.SetActive(true);
            }
        }
        // if(ammoAmount > 0){
        //     ammoText.color = Color.white;
        // } else {
        //     ammoText.color = Color.red;
        // }
    }
}
