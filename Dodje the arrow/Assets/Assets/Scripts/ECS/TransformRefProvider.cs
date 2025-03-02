using UnityEngine;
using Voody.UniLeo;

public class TransformRefProvider : MonoProvider<TransformRefComponent>
{
    private void Awake()
    {
        value.Value = transform;
    }
}