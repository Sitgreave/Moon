using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlaceHolder : MonoBehaviour
{
    [SerializeField] private GridSystem _grid;
    private Dictionary<Resource, Mark> _markMaps = new Dictionary<Resource, Mark>();
    private int _markIndex = 0;

    public void HoldResource(Resource resource)
    {
        SearchFreeMark();
        _markMaps.Add(resource, _grid.Marks[_markIndex]);
        resource.transform.DOMove(_grid.Marks[_markIndex].transform.position, .2f).
                                    OnComplete( () => EndMove(resource));   
    }

    public void TransferResource(Resource resource)
    {
        _markMaps[resource].IsEmpty = true;
        _markMaps.Remove(resource);
    }
    private void EndMove(Resource resource)
    {
        resource.transform.position = _grid.Marks[_markIndex].transform.position;
        resource.transform.SetParent(_grid.Marks[_markIndex].transform);
    }

    private void SearchFreeMark()
    {
        for (int i = 0; i < _grid.Marks.Count; i++)
        {
            if (_grid.Marks[i].IsEmpty)
            {
                _markIndex = i;
                _grid.Marks[_markIndex].IsEmpty = false;
                
                break;
            }
        }
        
    }
   
}
