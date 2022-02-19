using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _dropResourceFrom;
    [SerializeField] [Range(1, 30)] private float _delay;
    [SerializeField] private Resource _production;


    private void Start()
    {
        StartCoroutine(Producing()); 
    }
    private IEnumerator Producing()
    {
        while (true)
        {
           if(ConditionsMet())
            {
                Resource newResource = Instantiate(
                    _production, 
                    _dropResourceFrom.position, 
                    Quaternion.identity,
                    _dropResourceFrom);

                _inventory.GetNewResource(newResource);
            }
            yield return new WaitForSeconds(_delay);
        }
    }
    
    
    private bool ConditionsMet()
    {
        if (_inventory.NotFull() && ProduceConditionsMet()) return true;
        return false;
    }
    protected virtual bool ResourceReceived()
    {
        return true;
    }
    protected virtual bool ProduceConditionsMet()
    {
        return true;
    }
    

}
