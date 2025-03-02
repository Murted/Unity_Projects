using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform[] leftSpawnPoints;
    public Transform[] rightSpawnPoints;

    private List<GameObject> arrowPool = new List<GameObject>();
    private float spawnRate = 2f;
    private float speedIncrease = 1f;
    private float maxSpeed = 5f;
    private float currentSpeed = 3f;
    private EcsWorld _world;

    private void Start()
    {
        _world = WorldHandler.Instance.GetWorld();
        InvokeRepeating(nameof(SpawnArrow), 1f, spawnRate);
    }

    private void SpawnArrow()
    {
        bool isLeftSide = Random.Range(0, 2) == 0;

        Transform spawnPoint = isLeftSide
            ? leftSpawnPoints[Random.Range(0, leftSpawnPoints.Length)]
            : rightSpawnPoints[Random.Range(0, rightSpawnPoints.Length)];

        ArrowDirection direction = isLeftSide ? ArrowDirection.Right : ArrowDirection.Left;

        GameObject arrow = GetArrowFromPool();
        arrow.transform.position = spawnPoint.position;
        arrow.transform.rotation = isLeftSide
            ? Quaternion.Euler(0, 180, 0)
            : Quaternion.identity;
        arrow.SetActive(true);

        var entity = _world.NewEntity();
        ref var arrowComponent = ref entity.Get<ArrowComponent>();
        ref var transformRef = ref entity.Get<TransformRefComponent>();

        arrowComponent.IsActive = true;
        arrowComponent.Speed = currentSpeed;
        arrowComponent.Direction = direction;

        transformRef.Value = arrow.transform;

        currentSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);
    }

    private GameObject GetArrowFromPool()
    {
        foreach (var arrow in arrowPool)
        {
            if (!arrow.activeInHierarchy)
            {
                return arrow;
            }
        }

        GameObject newArrow = Instantiate(arrowPrefab);
        newArrow.SetActive(false);
        arrowPool.Add(newArrow);
        return newArrow;
    }
}
