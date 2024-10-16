using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] SpaceShip playerSpaceShip;

    // Start is called before the first frame update
    void Start()
    {
        //Time.fixedDeltaTime = 1/60;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerSpaceShip.isDead()) {
            SceneManager.LoadScene("MainMenu");
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            playerSpaceShip.LaunchWithShip();
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            playerSpaceShip.GetProjectileLauncher().Reload();
        }

        if(Input.GetKeyDown(KeyCode.J)) {
            SolarSystemManager.singleton.JumpAwayFromSystem();
        }

        playerSpaceShip.AimShip(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void FixedUpdate() {

        Vector3 movement = Vector3.zero;
        if(Input.GetKey(KeyCode.W)) {
            movement += new Vector3(0,1,0);
        }
        if(Input.GetKey(KeyCode.S)) {
            movement += new Vector3(0,-1,0);
        }
        if(Input.GetKey(KeyCode.A)) {
            movement += new Vector3(-1,0,0);
        }
        if(Input.GetKey(KeyCode.D)) {
            movement += new Vector3(1,0,0);
        }
        playerSpaceShip.Move(movement);
        //playerSpaceShip.MoveToward(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public SpaceShip GetPlayerShip() {
        return playerSpaceShip;
    }
}
