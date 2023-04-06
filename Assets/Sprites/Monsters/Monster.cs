using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Health_Management
{
    
    public float speed = 0.01f;
    
    public LayerMask layer_colision;
    public LayerMask layer_bullet;

    public float HitImpactX = -10.0f;
    public float HitImpactY = 10.0f;

    public GameObject DeathEffect;

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
            Debug.Log("Detected layer_colision");
            _currentDirection = -_currentDirection;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var layer_collision = collision.gameObject.layer;

        if (layer_collision == Mathf.Log(layer_bullet.value, 2))
        {
            Debug.Log("yeay");
            var isSideLeft = true;
            if (collision.transform.position.x - transform.position.x < 0)
            {
                isSideLeft  = false;
            }
            Hit(isSideLeft);
        }

    }

    void Hit(bool isSideLeft)
    {
        Vector2 hitImpact;
        if (isSideLeft) {
            hitImpact = new Vector2( HitImpactX, HitImpactY);
        } 
        else 
        {
            hitImpact = new Vector2( -HitImpactX, HitImpactY);
        }
        _playerRigidbody.AddForce(hitImpact);

        TakeDamage();
    }

    public override void Death()
    {
        Debug.Log("Spawn");
        Instantiate(DeathEffect, transform.position, transform.rotation);
        Instantiate(DeathEffect, transform.position, transform.rotation);
        Instantiate(DeathEffect, transform.position, transform.rotation);
 
        Destroy(gameObject);
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
