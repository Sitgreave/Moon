using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private LayerMask _resourceLayer;
    private Queue<Resource> _nearResources = new Queue<Resource>();
    public Queue<Resource> NearResources => _nearResources;
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
            Collider[] hits = Physics.OverlapSphere(transform.position, 2, _resourceLayer);
            if (hits.Length > 0)
            {
                int i = hits.Length - 1;
                if (hits[i].TryGetComponent<Resource>(out Resource resource))
                {
                    if (!resource.LockedToTake)
                    {
                        _nearResources.Enqueue(resource);
                        if (ResourceDetected != null) ResourceDetected.Invoke();
                    }
                }
            }
            yield return new WaitForSeconds(.4f);
        }
    }
}
