using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    public player_controller owner;
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!owner.Face_Right)
        {
            rb.velocity = transform.right * speed;
        }
        else 
        {
            rb.velocity = transform.right  * speed * -1;

        }


        Destroy(gameObject, 0.25f);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Destroy_obj()
    {

        Destroy(this);
    }
}
