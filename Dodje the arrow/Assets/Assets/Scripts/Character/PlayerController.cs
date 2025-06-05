using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float collisionDisableTime = 0.3f;
    [SerializeField] private LayerMask platformLayer;

    [SerializeField] private float xMin = -5f;
    [SerializeField] private float xMax = 5f;

    private Character inputActions;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D playerCollider;
    private Vector2 moveInput;

    private float moveX;
    private float moveY;
    public int currentPlatform = 2;
    private bool isGrounded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        inputActions = new Character();
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputActions.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Movement.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Movement.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Movement.Move.canceled -= ctx => moveInput = Vector2.zero;
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        moveX = moveInput.x * speed;
        moveY = rb.linearVelocity.y;


        isGrounded = IsGrounded();

        float clampedX = Mathf.Clamp(transform.position.x + moveX * Time.fixedDeltaTime, xMin, xMax);

        if (moveInput.y > 0 && isGrounded)
        {
            Jump();
        }
        else if (moveInput.y < 0 && currentPlatform > 1)
        {
            Down();
        }

        rb.linearVelocity = new Vector2(moveX, moveY);

        transform.position = new Vector2(clampedX, transform.position.y);

        if (moveX < 0)
        {
            sprite.flipX = true;
        }
        else if (moveX > 0)
        {
            sprite.flipX = false;
        }

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("speed", Mathf.Abs(moveX));

        animator.SetFloat("jumpSpeed", moveY);

        animator.SetBool("IsGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            float platformY = collision.transform.position.y;

            if (platformY <= -1.75f)
            {
                currentPlatform = 1;
            }
            else if (platformY <= 1.0f)
            {
                currentPlatform = 2;
            }
            else if (platformY > 1.0f)
            {
                currentPlatform = 3;
            }

            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private bool IsGrounded()
    {
        if (moveY != 0) return false;
        float extraHeight = 0.1f;
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeight, platformLayer);
        return hit.collider != null;
    }

    public void Jump()
    {
        if (currentPlatform < 3)
        {
            moveY = jumpPower;
            isGrounded = false;
            StartCoroutine(DisableCollisionTemporarily());
        }
    }

    public void Down()
    {
        if (isGrounded && IsPlatformBelow())
        {
            StartCoroutine(DisableCollisionTemporarily());
        }
    }

    private bool IsPlatformBelow()
    {
        float checkDistance = 1f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, platformLayer);
        return hit.collider != null;
    }

    private IEnumerator DisableCollisionTemporarily()
    {
        isGrounded = false;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), true);
        yield return new WaitForSeconds(collisionDisableTime);
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
    }

    public void ResetSpeed()
    {
        moveX = 0f;
        moveY = 0f;
        rb.linearVelocity = Vector2.zero;
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
    }
}