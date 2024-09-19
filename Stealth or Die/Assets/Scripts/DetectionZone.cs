using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField]
    private GameObject alarm;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private Move player;
    [SerializeField]
    private PatrolEnemy enemy;

    private bool isTriggered = false;

    private void OnTriggerStay(Collider other)
    {
        if (IsPlayer(other))
        {
            if (player.GetCurrentSpeed() != 0f && !isTriggered)
            {
                isTriggered = true;
                alarm.SetActive(true);
                player.isRevealed = true;
                enemy.Revealed();
            }
        }
    }

    private bool IsPlayer(Collider other)
    {
        return ((1 << other.gameObject.layer) & playerLayer) != 0;
    }
}
