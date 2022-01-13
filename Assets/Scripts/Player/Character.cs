
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    //–≈… ¿—“
    [SerializeField] private CharacterInventory _inventory;
    private Coroutine resourceMoving;
    private Queue<Resource> resourcesToUse = new Queue<Resource>();

    private void Update()
    {
        if(resourcesToUse.Count > 0)
        {
            Invoke(nameof(CourMoving), .1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Resource>(out Resource resource))
        {
            if (!resource.LockedToMove)
            {
                resourcesToUse.Enqueue(resource);
               
            }
        }
        else if (other.TryGetComponent<FactoryInventory>(out FactoryInventory factoryInventory))
        {
            _inventory.GetToDropResorources(ResourceType.Type_1);
            _inventory.DropResource(resource);
            _inventory.ResourcesToDrop.Peek().LockedToMove = true;
            factoryInventory.PlaceHolder.HoldResource(_inventory.ResourcesToDrop.Pop());
        }
    }

    private void CourMoving()
    {
        if (resourceMoving == null) {
           resourceMoving = StartCoroutine(ResourceMoving());
            
        } 
    }
    IEnumerator ResourceMoving()
    {
        while (resourcesToUse.Count > 0)
        {
            _inventory.GetNewResource(resourcesToUse.Dequeue());
            yield return new WaitForSeconds(.2f);
        }
        resourceMoving = null;
    }
}
