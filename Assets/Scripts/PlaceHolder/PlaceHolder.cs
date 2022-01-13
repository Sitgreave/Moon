using System.Collections;
using UnityEngine;
using DG.Tweening;
public class PlaceHolder : MonoBehaviour
{
    [SerializeField] private RectTransform _place;
    Tween tween;
    public void HoldResource(Resource resource)
    {
        //resource.transform.SetParent(_place);
        resource.transform.DOMove(_place.position, .3f).OnComplete( () => EndMove(resource)); 
        
        //resource.transform.position = _place.position;
    }

    private void EndMove(Resource resource)
    {
        
        resource.transform.position = _place.position;
        resource.transform.SetParent(_place.transform);
        
    }
   
}
