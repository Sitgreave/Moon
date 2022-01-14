using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private PlaceHolder _placeHolder;
    public PlaceHolder PlaceHolder => _placeHolder;
    protected List<Resource> resources = new List<Resource>();
    public List<Resource> Resources => resources;
    private Stack<Resource> _resourcesToTransfering = new Stack<Resource>();
    public Stack<Resource> ResourcesToTransfering => _resourcesToTransfering;

    private int _resourceCount = 0;
    public void PrepareToTransfering(ResourceType type)
    {
       
            for (int i = 0; i < Resources.Count; i++)
            {
                if (Resources[i].Type == type)
                {
                    _resourcesToTransfering.Push(Resources[i]);
                    Resources[i].LockedToTake = true;
                    DropResource(Resources[i]);
                }
            }
        
    }
    public virtual void GetNewResource(Resource resource)
    {
        if (NotFull())
        {
            _placeHolder.HoldResource(resource);
            _resourceCount++;
            Resources.Add(resource);
        }
    }

    public virtual void DropResource(Resource resource)
    {
        if (NotEmpty())
        {
            _placeHolder.TransferResource(resource);
            _resourceCount--;
            Resources.Remove(resource);
        }
    }

    protected bool NotFull()
    {
        return _resourceCount < _capacity;
    }

    protected bool NotEmpty()
    {
        return _resourceCount > 0;
    }
}
