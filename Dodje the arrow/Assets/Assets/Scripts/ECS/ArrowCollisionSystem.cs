using Leopotam.Ecs;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ArrowCollisionSystem : IEcsRunSystem
{
    private readonly EcsFilter<TransformRefComponent, ArrowComponent> _arrows = null;

    public void Run()
    {
        foreach (var i in _arrows)
        {
            ref var arrowTransform = ref _arrows.Get1(i);
            ref var arrow = ref _arrows.Get2(i);

            if (!arrow.IsActive) continue;

            if (!arrowTransform.Value.gameObject.activeInHierarchy)
            {
                arrow.IsActive = false;
                continue;
            }

            if (PlayerController.Instance == null) return;

            Vector2 playerPosition = PlayerController.Instance.transform.position;

            if (Vector2.Distance(arrowTransform.Value.position, playerPosition) < 1.2f && !GameManager.Instance.IsGameOver)
            {
                GameManager.Instance.IsGameOver = true;
                GameManager.Instance.GameOver();
                Debug.Log("qwe");
            }
        }
    }
}
