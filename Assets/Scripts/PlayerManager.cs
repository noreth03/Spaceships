using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2f;
    public float speedY = 2f;
    [Header("Limites")]
    public float limiteX = 4f;

    [Header("Disparo")] 
    public GameObject prefabDisparo;
    public float disparoSpeed =2f;
    public float timeDisparoDestroy = 2f;
    
    public Transform weapon1;
    public Transform weapon2;
    private bool isFire = false;
    private Rigidbody2D rb;
    public PlayerHealth playerHealth;
    public int damage = 1;
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        StartFire();
    }

    public void MovePlayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, speedY);
        if (transform.position.x > limiteX)
        {
            transform.position = new Vector2(limiteX, transform.position.y);
        }
        else if(transform.position.x < -limiteX)
        {
            transform.position = new Vector2(-limiteX, transform.position.y);
        }
        
    }

    public void StartFire()
    {
        if (Input.GetAxis("Jump") == 1f)
        {
            if (!isFire)
            {
                isFire = true;
                GameObject disparoInstance = Instantiate(prefabDisparo);
                disparoInstance.transform.SetParent(transform.parent);
                
                disparoInstance.transform.position = weapon1.position;
                disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance,timeDisparoDestroy);
                
                GameObject disparoInstance2 = Instantiate(prefabDisparo);
                disparoInstance2.transform.SetParent(transform.parent);
                disparoInstance2.transform.position = weapon2.position;
                //Move
                disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance2,timeDisparoDestroy);
            }
            else
            {
                isFire = false;
            }
        }
    }
  
        // void OnCollisionEnter(Collider2D otherCollider)
        // {
            
        //     if (otherCollider.tag == "disparoEnemigo" ) {
        //         gameObject.SetActive (false);
        //         Destroy (otherCollider.gameObject);
        //     } 
        //     if (otherCollider.tag == "disparoEnemigo" || otherCollider.tag == "Enemigo"){
        //         playerHealth.TakeDamage(damage);
        //     }
        // }

        void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.CompareTag("disparoEnemigo"))
            {
                playerHealth.TakeDamage(damage);
                Destroy(otherCollider.gameObject);
            }
    
        }

}

