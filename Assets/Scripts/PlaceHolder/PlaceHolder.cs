using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PlaceHolder : MonoBehaviour
{
    [SerializeField] private GridSystem _grid;
    [SerializeField] [Range(.1f, 10)] private float _moveTime;
    private Dictionary<Resource, Mark> _markMaps = new Dictionary<Resource, Mark>();
    private int _markIndex = 0;
    
    public void HoldResource(Resource resource)
    {
        SearchFreeMark();
        
        _markMaps.Add(resource, _grid.Marks[_markIndex]);
       
        resource.transform.DOMove(_grid.Marks[_markIndex].transform.position, _moveTime, false).
                                    OnComplete( () => EndMove(resource));   
    }

    public void TransferResource(Resource resource)
    {
        if (_markMaps.TryGetValue(resource, out Mark mark))
        {
            mark.IsEmpty = true;
            
            _markMaps.Remove(resource);
        }
    }
    private void EndMove(Resource resource)
    {
       resource.transform.position = _markMaps[resource].transform.position;
        resource.transform.SetParent(_markMaps[resource].transform);
        resource.transform.rotation = new Quaternion(0, 0, 0, 0);
        resource.LockedToTake = false;
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
