using System.Collections.Generic;
using UnityEngine;

public class ComplexFactory : Factory
{
    [SerializeField] protected List<ResourceType> _requiredTypes;
    [SerializeField] protected Inventory _storage;
    protected override bool ProduceConditionsMet()
    {
        return HaveResources();
    }

    private bool HaveResources()
    {
        for (int i = 0; i < _requiredTypes.Count; i++)
        {
            if (_storage.CountOfOneType.TryGetValue(_requiredTypes[i], out uint value))
            {
                if (value > 0) return true;
            }
        }
        return false;
    }
}
