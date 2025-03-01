using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapePanel : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject tractor;
    [SerializeField] private GameSceneController gameSceneController;

    private int direction = 3;
    private bool ready = false;

    private void Start()
    {
        StartCoroutine(MoveTractor());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if (eventData.delta.x > 0)
            {
                if (direction != 4 && direction != 3)
                {
                    ready = false;
                    StartCoroutine(WaitingRoom(3));
                }
            }
            else
            {
                if (direction != 3 && direction != 4)
                {
                    ready = false;
                    StartCoroutine(WaitingRoom(4));
                }
            }
        }
        else
        {
            if (eventData.delta.y > 0)
            {
                if (direction != 2 && direction != 1)
                {
                    ready = false;
                    StartCoroutine(WaitingRoom(1));
                }
            }
            else
            {
                if (direction != 1 && direction != 2)
                {
                    ready = false;
                    StartCoroutine(WaitingRoom(2));
                }               
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    { }

    void Update()
    {
        if (gameSceneController.game)
        {
            if (tractor.transform.position.x >= 0.89f && tractor.transform.position.z <= -0.891f && direction == 3)
            {
                direction = 1;
            }
            else if (tractor.transform.position.x >= 0.901f && direction == 3)
            {
                direction = 2;
            }

            if (tractor.transform.position.x <= -0.59f && tractor.transform.position.z >= 0.59f && direction == 4)
            {
                direction = 2;
            }
            else if (tractor.transform.position.x <= -0.601f && direction == 4)
            {
                direction = 1;
            }

            if (tractor.transform.position.z >= 0.59f && tractor.transform.position.x >= 0.89f && direction == 1)
            {
                direction = 4;
            }
            else if (tractor.transform.position.z >= 0.601f && direction == 1)
            {
                direction = 3;
            }

            if (tractor.transform.position.z <= -0.89f && tractor.transform.position.x <= -0.59f && direction == 2)
            {
                direction = 3;
            }
            else if (tractor.transform.position.z <= -0.901f && direction == 2)
            {
                direction = 4;
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    IEnumerator MoveTractor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            switch (direction)
            {
                case 1:
                    tractor.transform.rotation = Quaternion.Lerp(tractor.transform.rotation, Quaternion.Euler(0, 0, 0), 1f);
                    tractor.transform.position += Vector3.forward * speed;
                    break;
                case 2:
                    tractor.transform.rotation = Quaternion.Lerp(tractor.transform.rotation, Quaternion.Euler(0, 180, 0), 1f);
                    tractor.transform.position -= Vector3.forward * speed;
                    break;
                case 3:
                    tractor.transform.rotation = Quaternion.Lerp(tractor.transform.rotation, Quaternion.Euler(0, 90, 0), 1f);
                    tractor.transform.position += Vector3.right * speed;
                    break;
                default:
                    tractor.transform.rotation = Quaternion.Lerp(tractor.transform.rotation, Quaternion.Euler(0, 270, 0), 1f);
                    tractor.transform.position -= Vector3.right * speed;
                    break;
            }
        }
    }

    IEnumerator WaitingRoom(int temp)
    {
        while (!ready)
        {
            yield return new WaitForSeconds(0.01f);
            if (temp == 1 || temp == 2)
            {
                if (tractor.transform.position.x >= 0)
                {
                    if (tractor.transform.position.x % 0.3 >= 0.29f || tractor.transform.position.x % 0.3f <= 0.009f)
                    {
                        direction = temp;
                        ready = true;
                    }
                }
                else
                {
                    if (tractor.transform.position.x % 0.3 <= -0.29f || tractor.transform.position.x % 0.3f >= -0.009f)
                    {
                        direction = temp;
                        ready = true;
                    }
                }

            }
            else if (temp == 3 || temp == 4)
            {
                if (tractor.transform.position.z >= 0)
                {
                    if (tractor.transform.position.z % 0.3f >= 0.29f || tractor.transform.position.z % 0.3f <= 0.009f)
                    {
                        direction = temp;
                        ready = true;
                    }
                }
                else
                {
                    if (tractor.transform.position.z % 0.3f <= -0.29f || tractor.transform.position.z % 0.3f >= -0.009f)
                    {
                        direction = temp;
                        ready = true;
                    }
                }
            }
        }
    }
}
