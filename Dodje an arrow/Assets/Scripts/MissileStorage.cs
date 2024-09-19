using UnityEngine;

[CreateAssetMenu(fileName = "New Storage", menuName = "MissileStorage", order = 51)]
public class MissileStorage : ScriptableObject
{
    public GameObject prefab;
    [SerializeField] public float speed;
    [SerializeField] public bool rightSide;

    public GameObject GetPrefab()
    {
        return prefab;
    }

    public GameObject CreateObject()
    {
        var prefab = GetPrefab();
        var obj = Instantiate(prefab);
        return obj;
    }
}
