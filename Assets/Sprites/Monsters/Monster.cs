using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    
    public float speed = 0.01f;
    public LayerMask layer_colision;

    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _playerAnimator;
    private int _currentDirection = -1; // LEFT 

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        move();
        sprite_renderer();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Detected collision");
        //Debug.Log(collision.gameObject.layer );
        //Debug.Log(layer_colision.value);
        //Debug.Log(LayerMask.LayerToName(layer_colision));
        // if (collision.gameObject.layer == layer_colision.value) 
        // {
            Debug.Log("Detected layer_colision");

            _currentDirection = -_currentDirection;
        //}

    }
    void sprite_renderer() 
    {
        if (_currentDirection < 0 ) {
            _spriteRenderer.flipX =  false;
        }
        else {
            _spriteRenderer.flipX =  true;
        }
    }

    void move(){    
        Vector3 movement = new Vector3(speed * _currentDirection, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }



    
    
}
