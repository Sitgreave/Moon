using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterInventory _inventory;


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.TryGetComponent<Resource>(out Resource resource))
        {
            if(!resource.LockedToMove) _inventory.GetNewResource(resource);
        }
        else if(other.TryGetComponent<FactoryInventory>(out FactoryInventory factoryInventory))
        {
            _inventory.GetToDropResorources(ResourceType.Type_1);
            _inventory.DropResource(resource);
            _inventory.ResourcesToDrop.Peek().LockedToMove = true;
            factoryInventory.PlaceHolder.HoldResource(_inventory.ResourcesToDrop.Pop());
        }
    }

}
