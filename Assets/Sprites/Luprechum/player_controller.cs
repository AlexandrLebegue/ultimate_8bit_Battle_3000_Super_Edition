using System.Collections;
using System.Collections.Generic;
using UnityEngine;



static class Constants
{
public static readonly Vector2 OFFSET_COLLIDE_CROUCH = new Vector2(-0.34f, -0.24f);
public static readonly Vector2 SIZE_COLLIDE_CROUCH = new Vector2(3.83f, 4.07f);
public static readonly Vector2 OFFSET_COLLIDE_IDLE = new Vector2(0.1631325f, -0.15f);
public static readonly Vector2 SIZE_COLLIDE_IDLE = new Vector2(3.763591f, 8.12f);
public static readonly int MIN_HEIGHT = 6;

}


public class player_controller : MonoBehaviour
{

    public float speedForce = 2f;
    public float jumpforce = 2f;
    Vector2 player_movement;
    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _spriteRenderer;

    private Vector3 m_velocity = Vector3.zero;

    [SerializeField] private bool CheckOnGround;
    [Range(0, 1)] [SerializeField] private float m_MouvementSmoothing = .05f;
    [SerializeField] private float max_velocity;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        CheckOnGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
    }

    void move() 
    {
        side_move();
        crouch();
    }

    void side_move()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        Debug.Log("input");
        Vector3 targetVelocity = new Vector2(horizontalInput*speedForce, verticalInput*speedForce);
        _playerRigidbody.velocity = Vector3.SmoothDamp(_playerRigidbody.velocity, targetVelocity, ref m_velocity, m_MouvementSmoothing, max_velocity);
                

    }

    void crouch()
    {

    }

    void jump()
    {

    }



}
