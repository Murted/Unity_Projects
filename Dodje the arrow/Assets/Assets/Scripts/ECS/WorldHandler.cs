using Leopotam.Ecs;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
    public static WorldHandler Instance { get; private set; }

    private EcsWorld _world;
    private EcsSystems _systems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeECS();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeECS()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _systems
            .Add(new ArrowMoveSystem())
            .Add(new ArrowCollisionSystem())
            .Init();
    }

    private void Update()
    {
        _systems?.Run();
    }

    public EcsWorld GetWorld()
    {
        return _world;
    }

    private void OnDestroy()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }

        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }
}
