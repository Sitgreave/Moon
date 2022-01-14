using UnityEngine;

public class Resource : MonoBehaviour
{    
   
    [SerializeField] private ResourceType _type;
    [SerializeField] private Transform _transform;
    [SerializeField] private BoxCollider _boxCollider;
    public bool LockedToTake;
    public ResourceType Type => _type;
    public new Transform transform => _transform;
    
    public float Height()
    {
        return _boxCollider.size.y;
    }
    //public ResoruceType Type;
}

public enum ResourceType
{
    Type_1,
    Type_2,
    Type_3
}
