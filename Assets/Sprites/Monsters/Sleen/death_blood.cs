using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death_blood : MonoBehaviour
{
    private Rigidbody2D rb;
    public float lifeSpawn = 3.0f;
    public float randomRotation = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
     
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-randomRotation ,randomRotation), Random.Range(-randomRotation ,randomRotation)));
        Destroy(gameObject, lifeSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
