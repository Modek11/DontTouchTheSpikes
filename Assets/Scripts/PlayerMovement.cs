using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] private float horizontalSpeed = 0;
    [SerializeField] private float jumpForce = 5;
    
    private readonly float rightXValue = 3;
    private readonly float leftXValue = -3;
    private bool isFlyingRight = true;
    
    private Rigidbody2D rb;
    private PlayerInputSystem playerInputSystem;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Gameplay.Enable();
        
    }

    private void Start()
    {
        playerInputSystem.Gameplay.Jump.started += JumpOnstarted;
    }

    private void FixedUpdate()
    {
        var target = new Vector2(HorizontalXValue(),rb.position.y);
        rb.position = Vector2.MoveTowards(rb.position, target, horizontalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Spike"))
        {
            Die();
        }
        else
        {
            ChangeBirdDirection();
            GameHandler.Instance.InvokeOnPlayerHitWall(isFlyingRight);
        }
    }

    private float HorizontalXValue()
    {
        return isFlyingRight ? rightXValue : leftXValue;
    }
    
    private void ChangeBirdDirection()
    {
        isFlyingRight = !isFlyingRight;
    }
    
    private void JumpOnstarted(InputAction.CallbackContext obj)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
    }
    
    private void Die()
    {
        Debug.Log("dead");
    }
    
}
