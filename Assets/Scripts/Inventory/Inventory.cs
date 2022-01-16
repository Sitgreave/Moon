using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _capacity;
    [SerializeField] private InventoryType _inventoryType;
    [SerializeField] private PlaceHolder _placeHolder;
    protected List<Resource> resources = new List<Resource>();
    public List<Resource> Resources => resources;
    private Stack<Resource> _resourcesToTransfering = new Stack<Resource>();
    public Stack<Resource> ResourcesToTransfering => _resourcesToTransfering;
    public InventoryType InventoryType => _inventoryType;
    private int _resourceCount = 0;

    private Dictionary<ResourceType, uint> _countOfOneType = new Dictionary<ResourceType, uint>();
    public Dictionary<ResourceType, uint> CountOfOneType => _countOfOneType;
    public void PrepareToTransfering(ResourceType type)
    {
        if (Resources.Count > 0)
        {
           for (int i = 0; i < Resources.Count; i++)
            {
                if (Resources[i].Type == type && !Resources[i].LockedToTake)
                {
                    _resourcesToTransfering.Push(Resources[i]);
                }
            }
        }
    }
    


    public virtual void GetNewResource(Resource resource)
    {
        if (NotFull())
        {
            _placeHolder.HoldResource(resource);
            _resourceCount++;
            if (_countOfOneType.TryGetValue(resource.Type, out _))
            {
                _countOfOneType[resource.Type]++;
            }
            else _countOfOneType.Add(resource.Type, 1);
            Resources.Add(resource);
        }
    }

    public virtual void DropResource(Resource resource)
    {
        if (NotEmpty())
        {
            _placeHolder.TransferResource(resource);
            _resourceCount--;
            _countOfOneType[resource.Type]--;
            Resources.Remove(resource);
        }
    }

    public bool NotFull()
    {
        return _resourceCount < _capacity;
    }

    public bool NotEmpty()
    {
        return _resourceCount > 0;
    }
}

public enum InventoryType{
    Fillable,
    Transmitting,
    Universal
}