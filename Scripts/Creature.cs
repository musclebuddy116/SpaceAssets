using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//responsible for basic movement, health and damage, basic information storing
public class Creature : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] int currentHealthPoints = 3;
    [SerializeField] int maxHealthPoints = 3;
    [Header("Movement")]
    [SerializeField] float speed = 10.0f;
    [Header("Naming")]
    [SerializeField] string creatureName = "DaShip";
    [Header("Vanity")]
    [SerializeField] Color baseColor = Color.white;
    [Header("Tracked Information")]
    [SerializeField] bool isDead = false;

    SpriteRenderer sr;


    void Awake() //goes before Start
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = baseColor;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Creature Start! " + creatureName);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Creature Update! " + creatureName);
    }

    public void Move(Vector3 movement) {
        transform.localPosition += movement * speed * Time.deltaTime;
    }
}
