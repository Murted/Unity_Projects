using Leopotam.Ecs;
using UnityEngine;

public class ArrowMoveSystem : IEcsRunSystem
{
    private readonly EcsFilter<TransformRefComponent, ArrowComponent> _arrows = null;

    public void Run()
    {
        foreach (var i in _arrows)
        {
            ref var arrowTransform = ref _arrows.Get1(i);
            ref var arrow = ref _arrows.Get2(i);

            if (!arrow.IsActive) continue;

            float direction = arrow.Direction == ArrowDirection.Right ? 1f : -1f;
            arrowTransform.Value.position += Vector3.right * direction * arrow.Speed * Time.deltaTime;

            if (Mathf.Abs(arrowTransform.Value.position.x) > 16f)
            {
                arrow.IsActive = false;
                arrowTransform.Value.gameObject.SetActive(false);
            }
        }
    }
}
