using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoiur : MonoBehaviour
{
    public float speed = 2f;
    public GameObject prefabDisparo;
    public float disparoSpeed = 2f;
    public float shootingInterval = 6f;
    public float timeDisparoDestroy = 2f;
    private float _shootingTimer;
    public Transform weapon1;
    public Transform weapon2;
    private float frecuencia = 1f;
    private float ampltitud = 2f;
    private float velCiclo = 1f;
    private Vector3 pos;
    private Vector3 axis;
    private PlayerHealth playerHealth;
    private bool isPausa = false;
    public int damage = 1;
    public int health ;
    

    void Start()
{
    health = 10;
    pos = transform.position;
    axis = transform.right;
    _shootingTimer = Random.Range(0f, shootingInterval);
    GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);

    playerHealth = FindObjectOfType<PlayerHealth>();
    if (playerHealth == null)
    {
        Debug.LogError("No se encontró el componente PlayerHealth en el jugador.");
    }

    
}


    void Update()
    {
        if (!isPausa)
        {
            StartFire();
        }
        ZigZagMov();
    }

    void ZigZagMov()
    {
        pos += Vector3.down * Time.deltaTime * velCiclo;
        transform.position = pos + axis * Mathf.Sin(Time.time * frecuencia) * ampltitud;
    }

    public void StartFire()
    {
        _shootingTimer -= Time.deltaTime;
        if (!isPausa && _shootingTimer <= 0f)
        {
            _shootingTimer = shootingInterval;
            GameObject disparoInstance = Instantiate(prefabDisparo);
            disparoInstance.transform.SetParent(transform.parent);
            disparoInstance.transform.position = weapon1.position;
            disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
            Destroy(disparoInstance, timeDisparoDestroy);

            GameObject disparoInstance2 = Instantiate(prefabDisparo);
            disparoInstance2.transform.SetParent(transform.parent);
            disparoInstance2.transform.position = weapon2.position;
            disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
            Destroy(disparoInstance2, timeDisparoDestroy);
        }
    }

    IEnumerator Pausa()
{
    isPausa = true;
    yield return new WaitForSeconds(1f);
    isPausa = false;
}

    void OnTriggerEnter2D(Collider2D otherCollider)
{
    if (otherCollider.CompareTag("Player"))
    {
        Destroy(gameObject);
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1);
        }
    }
    else if (otherCollider.CompareTag("disparoPlayer"))
    {
        StartCoroutine(Pausa());
        Destroy(otherCollider.gameObject);

        if (health != 0)
        {
            TakeDamage(damage);
        }
    }
    else if (otherCollider.CompareTag("Killer"))
    {
        Destroy(gameObject);
    }
}

public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        { 
            Destroy(gameObject);
        }
    }


}


