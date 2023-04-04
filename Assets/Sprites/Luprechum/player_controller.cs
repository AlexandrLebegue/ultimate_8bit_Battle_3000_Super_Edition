using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;


static class Constants
{
public static readonly Vector2 OFFSET_COLLIDE_CROUCH = new Vector2(-0.34f, -0.24f);
public static readonly Vector2 SIZE_COLLIDE_CROUCH = new Vector2(3.83f, 4.07f);
public static readonly Vector2 OFFSET_COLLIDE_IDLE = new Vector2(0.1631325f, -0.15f);
public static readonly Vector2 SIZE_COLLIDE_IDLE = new Vector2(3.763591f, 8.12f);
public static readonly int MIN_HEIGHT = 6;
public static readonly float SHOT_POINT_POS_X = 0.05f;

}


public class player_controller : MonoBehaviour
{

    public float speedForce = 2f;
    public float jumpforce = 2f;
    public Vector3 boxSize;
    public Vector2 player_movement;
    public float max_velocity;
    public float maxDistance;
    public LayerMask layerMask_jump;
    public LayerMask layerMask_stick;


    private Rigidbody2D _playerRigidbody;
    private SpriteRenderer _spriteRenderer;
    public bool Face_Right = true;
    private Animator _playerAnimator;
    private bool _hasJumped = false;
    private Vector3 m_velocity = Vector3.zero;
    
    public Shaker camShaker;
    public ShakePreset ShakePreset;


    // Shoot interaction
    public Transform shootingPoint;
    public Vector2 distanceShoot;
    public GameObject bullet; 
    public float shootRate = 0.25f;
    private float _lastTimeShoot = 0.0f ;
    

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

        movement();
        GroundCheck();
    }

    void movement() 
    {
        _playerAnimator.SetBool("Running", side_move()); 
        _playerAnimator.SetBool("Crouching", crouch());
        _playerAnimator.SetBool("Shooting", shoot());
        jump();
    }

    bool shoot()
    {
        var shootInput = Input.GetAxisRaw("Shoot");
        if (shootInput >0 )
        {
            camShaker.Shake(ShakePreset);
            if (Time.time > _lastTimeShoot + shootRate) 
            {
                
                var bullet_spawned = Instantiate(bullet, shootingPoint.position, transform.rotation).GetComponent<bullet_controller>();
                bullet_spawned.owner = this;
                _lastTimeShoot = Time.time;

            }            
        }
        return shootInput > 0;
    }

    bool side_move()
    {
       var horizontalInput = Input.GetAxisRaw("Horizontal");
       Vector3 movement = new Vector3(speedForce * horizontalInput, 0);
       movement *= Time.deltaTime;
       transform.Translate(movement);
       
       if (horizontalInput < 0) {
            _spriteRenderer.flipX =  true;
            Face_Right = true;
            var old_pos = shootingPoint.localPosition;
            shootingPoint.localPosition  = new Vector3(-Constants.SHOT_POINT_POS_X ,old_pos.y ,old_pos.z);

       }
       else if (horizontalInput > 0)
       {
            _spriteRenderer.flipX = false;
            Face_Right = false;
            var old_pos = shootingPoint.localPosition;
            shootingPoint.localPosition  = new Vector3(Constants.SHOT_POINT_POS_X ,old_pos.y ,old_pos.z);
       }
       
       return horizontalInput != 0 ;
    }

    bool crouch()
    {
        return Input.GetAxisRaw("Vertical") < 0;  
    }

    bool jump()
    {
        var verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput > 0 && (GroundCheck()) ) 
        {
            _playerRigidbody.AddForce(Vector3.up * jumpforce,ForceMode2D.Impulse); 
            _hasJumped = true;
            Invoke("resetIsJumping", 1.5f); // Quoiqu'il arrive on reset le jump
            return true;
        }

        return false;
    }

    private void resetIsJumping(){
        _hasJumped=false;
    }

    void OnDrawGizmos()
    {
        // cube for jump detection
        Gizmos.color=Color.red;
        Gizmos.DrawCube(transform.position-transform.up*maxDistance,boxSize);
        
    }

    private bool GroundCheck()
    {
        // Vérification si le personnage se trouve sur un bloc de type WALL pour sauter  
        if(Physics2D.BoxCast(transform.position,boxSize,0,-transform.up,maxDistance,layerMask_jump))
        {
            // Si le personnage a déjà sauté alors on retourne false car le saut commence à avoir lieu 
            if ( _hasJumped) {
                return false;
            } else {
                
                return true;
            }
        }
        else
        {
            if ( _hasJumped) {   
                

                _hasJumped = false;
            }
            return false;
        }
    }

}
