using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Management : MonoBehaviour
{
    public float hp = 10.0f;


    public virtual void TakeDamage(float damage= 1.0f, float randomness = 0.0f) {

        hp = hp - damage; 
        
        if (hp < 0) {
            Death();
        } 
    }

    public virtual void Death()
    {
        Debug.Log("U ded, put a function for more action with death");
        
        Destroy(gameObject);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
