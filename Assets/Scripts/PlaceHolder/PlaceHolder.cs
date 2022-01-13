using System.Collections;
using UnityEngine;
using DG.Tweening;
public class PlaceHolder : MonoBehaviour
{
    [SerializeField] private GridSystem _grid;
    private int _markIndex = 0;

    public void HoldResource(Resource resource)
    {

        SearchFreeMark();
        resource.transform.DOMove(_grid.Marks[_markIndex].transform.position, .3f).
                                    OnComplete( () => EndMove(resource));
        _markIndex++;
        
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
