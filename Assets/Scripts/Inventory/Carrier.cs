using System.Collections;
using UnityEngine;

public class Carrier : MonoBehaviour
{ 
    [SerializeField] [Range(.1f, 10)] private float _transferInterval = .3f;
    private Inventory _inventoryFrom;
    private Inventory _inventoryTo;
    public bool _doTransfering = false;
    private Coroutine _transfering;

    public void DefineInventories(Inventory to, Inventory from)
    {
        if(_inventoryFrom == null) _inventoryFrom = from;
        if(_inventoryTo == null) _inventoryTo = to;
    }
    public void TryTransfer(Inventory to, Inventory from)
    {
        DefineInventories(to, from);
        if (TransferConditionsMet())
        {
            if(!Input.GetMouseButton(0))
            _transfering = StartCoroutine(ResourceTransfering());
        }
    } 
    public void TryTransfer(Inventory to, Inventory from, ResourceType type)
    {
        DefineInventories(to, from);
            if (TransferConditionsMet())
            {
                _transfering = StartCoroutine(ResourceTransfering(type));
        } 
    }

    public void StopTransfering()
    {
        _doTransfering = false;
        _transfering = null;
        _inventoryFrom.ResourcesToTransfering.Clear();
        _inventoryTo = null;
        _inventoryFrom = null;
    }
    private bool TransferConditionsMet()
    {
        if (_inventoryFrom != null && _inventoryTo != null)
        {
            if (!_doTransfering)
            {
                if (_transfering == null)
                {
                    return true;
                }
            }
        }
        else Debug.Log("Conditions error");
        return false;
    }



    private IEnumerator ResourceTransfering(ResourceType resourceType)
    {
        _doTransfering = true;
        while (_doTransfering)
        {
            GetResource(resourceType);
            yield return new WaitForSeconds(_transferInterval);
        }
    }     
    private IEnumerator ResourceTransfering()
    {
        _doTransfering = true;
        while (_doTransfering)
        {
            if (!Input.GetMouseButton(0)) GetResource();
            yield return new WaitForSeconds(_transferInterval);
        }
    }

    private void GetResource()
    {
        if (_inventoryFrom.NotEmpty() && _inventoryTo.NotFull())
        {
            if (_inventoryFrom.ResourcesToTransfering.Count == 0)
            {
                _inventoryFrom.PrepareToTransfering();
            }
            else
            {
                Resource resource = _inventoryFrom.ResourcesToTransfering.Pop();
                _inventoryFrom.DropResource(resource);
                _inventoryTo.GetNewResource(resource);
            }
        }
    } 
    
    private void GetResource(ResourceType type)
    {
        if (_inventoryFrom.NotEmpty() && _inventoryTo.NotFull())
        {
            if (_inventoryFrom.ResourcesToTransfering.Count == 0)
            {
                _inventoryFrom.PrepareToTransfering(type);
            }
            else
            {
                Resource resource = _inventoryFrom.ResourcesToTransfering.Pop();
                _inventoryFrom.DropResource(resource);
                _inventoryTo.GetNewResource(resource);
            }
        }
    }
}
