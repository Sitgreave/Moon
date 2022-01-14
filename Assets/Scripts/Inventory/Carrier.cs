using System.Collections;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    [SerializeField] private Inventory _inventoryFrom;
    private ResourceType _requiredType;
    private bool _doTransfering = false;
    private Coroutine _transfering;

    private void OnTriggerStay(Collider other)
    {
        if (!_doTransfering)
        {
            if (other.TryGetComponent<FactoryInventory>(out FactoryInventory factoryInventory))
            {
                if (_inventoryFrom.Resources.Count > 0)
                {
                    _doTransfering = true;
                    if (_transfering == null)
                    {
                     
                        _transfering = StartCoroutine(ResourceTransfer(factoryInventory));
                    }
                }
            }
        }
    }

    public void CarryResource(Inventory to)
    {
        to.GetNewResource(_inventoryFrom.ResourcesToTransfering.Pop());
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FactoryInventory>(out FactoryInventory factoryInventory))
        {
            _doTransfering = false;
            _transfering = null;
        }
    }

     private IEnumerator ResourceTransfer(Inventory to)
    {
       
        while (_doTransfering)
        {
            _inventoryFrom.PrepareToTransfering(ResourceType.Type_1);
            if (_inventoryFrom.ResourcesToTransfering.Count > 0)
            {
                CarryResource(to);
                yield return new WaitForSeconds(.4f);
            }
            else break;
        }
        
    }
}
