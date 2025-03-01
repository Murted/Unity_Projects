using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; 
    public float cameraSpeed = 5f;
    public Vector3 offset = new Vector3(0, 10f, -10f);

    private Vector3 targetPosition;

    void Start()
    {
        transform.position = player.position + offset;
    }

    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        targetPosition = player.position + offset;

        if (moveVertical != 0)
        {
            float moveDirection = moveVertical > 0 ? 1 : -1;
            targetPosition += transform.forward * cameraSpeed * moveDirection * Time.deltaTime;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraSpeed);
    }
}
