using UnityEngine;

public class MoveBarrel : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float dropSpeed;
    [SerializeField]
    private float dropDistance;

    private Move playerMove;
    private float initialY;
    private bool isDrop = false;

    void Start()
    {
        playerMove = player.GetComponent<Move>();
        initialY = transform.position.y;
    }

    void FixedUpdate()
    {
        isDrop = playerMove.GetCurrentSpeed() <= 0.01f;

        if (isDrop && !playerMove.isRevealed)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, initialY - dropDistance, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, dropSpeed * Time.deltaTime);
           
        }
        else
        {
            Vector3 targetPosition = new Vector3(transform.position.x, initialY, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, dropSpeed * Time.deltaTime);
        }
    }
}
