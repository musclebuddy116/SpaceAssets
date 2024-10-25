using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CargoWindow : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI cargoText;
    [SerializeField] PlayerInputHandler playerInputHandler;

    // Update is called once per frame
    void Update()
    {
        cargoText.text = "CARGO     " + playerInputHandler.GetPlayerShip().GetCargo().ToString();
    }
}
