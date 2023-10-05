using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;
    
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Invoke("LoadNextScene", 1f); 
            //Destroy(gameObject);
        }
    }

    void LoadNextScene()
    {
        
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
