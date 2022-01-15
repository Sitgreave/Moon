using System.Collections;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private float _delay;
    [SerializeField] private Resource _production;
   

    private void Start()
    {
        StartCoroutine(Producing());
    }
    private IEnumerator Producing()
    {
        while (true)
        {
            while (_inventory.NotFull())
            {
                Resource newResource = Instantiate(_production, transform);
                _inventory.GetNewResource(newResource);
                yield return new WaitForSeconds(_delay);
            }
        }
    }
   

}
