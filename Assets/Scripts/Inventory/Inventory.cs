using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private PlaceHolder _placeHolder;
    public PlaceHolder PlaceHolder => _placeHolder;
    protected List<Resource> Resources = new List<Resource>();
    private int _resourceCount = 0;
    
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
