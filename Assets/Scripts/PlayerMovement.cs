using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] private float horizontalSpeed = 0;
    [SerializeField] private float jumpForce = 5;

    private const float RightXValue = 3;
    private const float LeftXValue = -3;
    private bool isFlyingRight = true;
    private bool isDead = false;
    
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
        GameHandler.Instance.OnPlayerDie += Die;
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        
        var target = new Vector2(HorizontalXValue(),rb.position.y);
        rb.position = Vector2.MoveTowards(rb.position, target, horizontalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (isDead) return;
        
        if (col.collider.CompareTag("Spike"))
        {
            GameHandler.Instance.InvokeOnPlayerDie();
        }
        else
        {
            ChangeBirdDirection();
            GameHandler.Instance.InvokeOnPlayerHitWall(isFlyingRight);
        }
    }

    private float HorizontalXValue()
    {
        return isFlyingRight ? RightXValue : LeftXValue;
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
        isDead = true;
        playerInputSystem.Disable();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddTorque(2f,ForceMode2D.Impulse);
    }

    private void OnDestroy()
    {
        playerInputSystem.Dispose();
    }
}
