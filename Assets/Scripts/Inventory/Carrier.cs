using System.Collections;
using UnityEngine;

public class Carrier : MonoBehaviour
{ 
    [SerializeField] [Range(.1f, 1)] private float _transferInterval;
    private Inventory _inventoryFrom;
    private Inventory _inventoryTo;
    public bool _doTransfering = false;
    private Coroutine _transfering;
   

    public void DefineInventories(Inventory to, Inventory from)
    {
        if(_inventoryFrom == null) _inventoryFrom = from;
        if(_inventoryTo == null) _inventoryTo = to;
       
    }

    public void TryTransfer(Inventory to, Inventory from, bool needStayOnPlace)
    {
        DefineInventories(to, from);
            if (TransferConditionsMet())
            {
                if(!needStayOnPlace)
                _transfering = StartCoroutine(ResourceTransfering(needStayOnPlace));
                else if(Input.GetMouseButton(0)) 
                _transfering = StartCoroutine(ResourceTransfering(needStayOnPlace)); ;
        } 
    }

    public void StopTransfering()
    {
        _doTransfering = false;
        _transfering = null;
        if (_inventoryFrom.ResourcesToTransfering.Count != 0)
        {
            _inventoryFrom.ResourcesToTransfering.Clear();
        }
        _inventoryTo = null;
        _inventoryFrom = null;
    }
    private bool TransferConditionsMet()
    {
        if (_inventoryFrom != null && _inventoryTo != null)
        {
            if (!_doTransfering)
            {
                if (_inventoryFrom.NotEmpty())
                {
                    if (_inventoryTo.NotFull())
                    {
                        if (_transfering == null)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        else Debug.Log("Conditions error");
        return false;
    }

    

     private IEnumerator ResourceTransfering(bool needStayOnPlace)
    {
            _inventoryFrom.PrepareToTransfering(ResourceType.Type_1);
        _doTransfering = true;
        while (_doTransfering)
        {

            if (_inventoryFrom.ResourcesToTransfering.Count > 0)
            {
                if (!needStayOnPlace)
                {
                    GetResource();
                }
                else if (!Input.GetMouseButton(0)) GetResource();
            }
            else _inventoryFrom.PrepareToTransfering(ResourceType.Type_1);
            yield return new WaitForSeconds(_transferInterval);
        }
    }

    private void GetResource()
    {
        _inventoryFrom.DropResource(_inventoryFrom.ResourcesToTransfering.Peek());
        _inventoryTo.GetNewResource(_inventoryFrom.ResourcesToTransfering.Pop());
    }
}
