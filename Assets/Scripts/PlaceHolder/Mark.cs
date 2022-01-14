using UnityEngine;

public class Mark : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    public new Transform transform => _transform;   
    public bool IsEmpty = true;

 
}
