using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceShip : MonoBehaviour
{

    Rigidbody2D rb;
    Health health;

    public enum Team{player, pirate}

    [Header("Social")]
    [SerializeField] Team team;

    [Header("Movement")]
    [SerializeField] float speed = 5;
    [SerializeField] float speedLimit = 10;

    [Header("Rotation")]
    [SerializeField] float rotationSpeed = 10;

    [Header("Tools")]
    [SerializeField] ProjectileLauncher projectileLauncher;

    //Cargo
    [Header("Cargo")]
    [SerializeField] int currentCargo = 0;
    [SerializeField] int maxCargo = 100;

    [Header("Mining")]
    [SerializeField] GameObject minerPrefab;
    
    //Trackers
    Vector3 movement = Vector3.zero;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        projectileLauncher.Equip(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        SolarSystemManager.singleton.RegisterSpaceShip(this);
    }


    public void AimShip(Transform targetTransform) {
        //transform.rotation = Quaternion.LookRotation(Vector3.forward, targetTransform.position - transform.position);
        AimShip(targetTransform.position);
    }
    public void AimShip(Vector3 aimPos) {
        if(health.IsDead()) {
            return;
        }

        Quaternion goalRotation = Quaternion.LookRotation(Vector3.forward, aimPos - transform.position);
        Quaternion currentRotation = transform.rotation;
        
        transform.rotation = Quaternion.Lerp(currentRotation, goalRotation, Time.deltaTime * rotationSpeed);
    }

    void FixedUpdate()
    {
        if(health.IsDead()) {
            Die();
            return;
        }
        
        rb.AddForce(movement * speed);
        if(rb.velocity.magnitude > speedLimit) {
            rb.velocity = rb.velocity.normalized * speedLimit;
        }
    }

    public void Move(Vector3 newMovement)
    {
        //transform.localPosition += movement * 10 * Time.deltaTime;
        //rb.velocity = movement * speed;
        //rb.MovePosition(transform.position + (movement * speed) * Time.fixedDeltaTime );
        
        //Is now in FixedUpdate
        //rb.AddForce(movement * speed);

        movement = newMovement;

    }

    public void Stop() {
        movement = Vector3.zero;
    }

    void Die() {
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void MoveToward(Vector3 goalPos) {
        goalPos.z = 0;
        Vector3 direction = goalPos - transform.position;
        Move(direction.normalized);
    }

    public void Recoil(Vector3 amount) {
        rb.AddForce(amount, ForceMode2D.Impulse);
    }

    public void LaunchWithShip(){
        if(health.IsDead()) {
            return;
        }
        Recoil(-transform.up*projectileLauncher.Launch());
    }

    public ProjectileLauncher GetProjectileLauncher() {
        return projectileLauncher;
    }

    public bool isDead() {
        return health.IsDead();
    }

    public void AddToCargo(int amount){
        if(currentCargo <= maxCargo - amount){
            currentCargo += amount;
        }

        if(currentCargo > maxCargo){
            currentCargo = maxCargo;
        }
    }

    public bool CargoFull(){
        return currentCargo >= maxCargo;
    }

    public int EmptyCargo(){
        int tempCargo = currentCargo;
        currentCargo = 0;
        return tempCargo;
    }

    public void TransferCargo(SpaceShip deliveryBoi){
        AddToCargo(deliveryBoi.EmptyCargo());
    }
    
    public void DeployMiner() {
        if(currentCargo < 1) {
            return;
        }
        currentCargo -= 1;
        GameObject newMiner = Instantiate(minerPrefab, transform.position, Quaternion.identity);
        newMiner.GetComponent<MinerAI>().SetMotherShip(this);
    }

    public Health GetHealth() {
        return health;
    }

    public void SetProjectileLauncher(ProjectileLauncher p1) {
        projectileLauncher = p1;
    }

    public void SetTeam(Team newTeam) {
        team = newTeam;
    }

    public Team GetTeam() {
        return team;
    }

    public int GetCargo() {
        return currentCargo;
    }

    public bool TakeCargo(int amount) {
        if(currentCargo < amount) {
            return false;
        }
        currentCargo -= amount;
        return true;
    }
}
