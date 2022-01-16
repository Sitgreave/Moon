using DG.Tweening;
using UnityEngine;

public class Resource : MonoBehaviour
{    
   
    [SerializeField] private ResourceType _type;
    [SerializeField] private Transform _transform;

    public bool LockedToTake;
    public ResourceType Type => _type;
    public new Transform transform => _transform;

    private void Start()
    {
        _transform.DOShakeScale(3, .3f, 3, 90, true);
    }
}

public enum ResourceType
{
    Type_1,
    Type_2,
    Type_3
}
