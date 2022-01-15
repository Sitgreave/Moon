using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private LayerMask _resourceLayer;
    [SerializeField] private float _radius;
    [SerializeField] private new Transform transform;
    private Stack<Resource> _nearResources = new Stack<Resource>();
    public Stack<Resource> NearResources => _nearResources;
    public delegate void ObjectDetected();
    public event ObjectDetected ResourceDetected;
    private void Start()
    {
        StartCoroutine(VisionInterval());
    }
    private IEnumerator VisionInterval()
    {
        while (true)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _resourceLayer);
            if (hits.Length > 0)
            {
                int i = hits.Length - 1;
                if (hits[i].TryGetComponent<Resource>(out Resource resource))
                {
                    if (!resource.LockedToTake)
                    {
                        _nearResources.Push(resource);
                        if (ResourceDetected != null) ResourceDetected.Invoke();
                    }
                }
            }
            yield return new WaitForSeconds(.3f);
        }
    }
}
