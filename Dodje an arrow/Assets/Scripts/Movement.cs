using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float timeToIgnore;
    [SerializeField] private float stopJumping;
    [SerializeField] private GameObject gameOver;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private float horizontalMove;
    private Rigidbody2D rigidbody;
    private float speed;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
    }

    void Update()
    {
        horizontalMove = speed;
        animator.SetFloat("HorizontalMove", Mathf.Abs(horizontalMove));

        if (horizontalMove < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontalMove > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void MoveLeft()
    {
        speed = -horizontalSpeed;
    }

    public void MoveRight()
    {
        speed = horizontalSpeed;
    }

    public void Stop()
    {
        speed = 0f;
    }

    public void Jump()
    {
        if (!IsFlying() && transform.position.y < stopJumping)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }

    public void Down()
    {
        if (!IsFlying())
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
            Invoke("IgnoreLayerOff", timeToIgnore);
        }
    }

    private bool IsFlying()
    {
        if (rigidbody.velocity.y != 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void OnCollisionEnter2D(Collision2D missile)
    {
        if (missile.gameObject.tag == "Missile")
        {
            gameOver.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0f;
        }    
    }
}
