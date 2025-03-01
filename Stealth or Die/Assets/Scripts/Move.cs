using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float turnSpeed;

    public bool isRevealed;

    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!isRevealed)
        {
            MoveChar();
        }
        else
        {
            animator.SetBool("isRevealed", isRevealed);
        }
    }

    private void MoveChar()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * moveSpeed;
        rb.velocity = movement;
        float currentSpeed = rb.velocity.magnitude;
        animator.SetFloat("speed", currentSpeed);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    public float GetCurrentSpeed()
    {
        return rb.velocity.magnitude;
    }
}
