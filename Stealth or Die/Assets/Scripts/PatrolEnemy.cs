using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private float patrolSpeed = 2f;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float bulletSpeed;

    public bool stop = false;
    public bool lose = false;

    private int currentPatrolIndex = 0;
    private Transform targetPoint;
    private bool isTurning = false;
    private Animator animator;

    void Start()
    {
        stop = false;
        animator = GetComponent<Animator>();
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
            targetPoint = patrolPoints[0];
        }
    }

    void FixedUpdate()
    {
        if (patrolPoints.Length > 0 && !stop)
        {
            if (isTurning)
            {
                TurnTowardsTarget();
            }
            else
            {
                MoveTowardsTarget();
            }
        }
        else if (bullet.activeSelf)
        {
            bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, player.position, bulletSpeed * Time.deltaTime);
            if(Vector3.Distance(bullet.transform.position, player.position) < 0.1f)
            {
                lose = true;
            }
        }
    }

    void TurnTowardsTarget()
    {
        patrolSpeed = 0;
        animator.SetFloat("speed", patrolSpeed);
        if (targetPoint == null) return;

        Vector3 direction = targetPoint.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            isTurning = false;
        }
    }

    void MoveTowardsTarget()
    {
        patrolSpeed = 2f;
        animator.SetFloat("speed", patrolSpeed);
        if (targetPoint == null) return;

        Vector3 direction = targetPoint.position - transform.position;
        transform.position += direction.normalized * patrolSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            targetPoint = patrolPoints[currentPatrolIndex];
            isTurning = true;
        }
    }

    public void Revealed()
    {
        transform.LookAt(player);
        stop = true;
        animator.SetBool("isRevealed", stop);
        bullet.gameObject.SetActive(true);
    }
}
