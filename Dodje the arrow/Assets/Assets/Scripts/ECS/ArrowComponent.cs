using Leopotam.Ecs;

public struct ArrowComponent
{
    public bool IsMoving;
    public bool IsActive;
    public float Speed;
    public ArrowDirection Direction;
}

public enum ArrowDirection
{
    Left,
    Right
}
