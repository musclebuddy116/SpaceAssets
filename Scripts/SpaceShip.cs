using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceShip : MonoBehaviour
{

    Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] float speed = 5;
    [SerializeField] float speedLimit = 10;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 10;

    [Header("Tools")]
    [SerializeField] ProjectileLauncher projectileLauncher;

    bool dead = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AimShip(Transform targetTransform) {
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position);
        AimShip(targetTransform.position);
    }
    public void AimShip(Vector3 aimPos) {

        Quaternion goalRotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
        Quaternion currentRotation = transform.rotation;
        
        transform.rotation = Quaternion.Lerp(currentRotation, goalRotation, Time.deltaTime * rotationSpeed);
    }

    void FixedUpdate()
    {
        if(rb.velocity.magnitude > speedLimit) {
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }

    public void Move(Vector3 movement)
    {
        //transform.localPosition += movement * 10 * Time.deltaTime;
        //rb.velocity = movement * speed;
        //rb.MovePosition(transform.position + (movement * speed) * Time.fixedDeltaTime );
        rb.AddForce(movement * speed);

    }

    public void Recoil(Vector3 amount) {
        rb.AddForce(amount, ForceMode2D.Impulse);
    }

    public void LaunchWithShip(){
        if(projectileLauncher.GetAmmo() < 0) {
            return;
            
        }
        
        Recoil(-transform.up*projectileLauncher.Launch());
    }

    public ProjectileLauncher GetProjectileLauncher() {
        return projectileLauncher;
    }

    public void Damage() {
        dead = true;
    }

    public bool isDead() {
        return dead;
    }
}
