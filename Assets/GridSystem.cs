using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject block;
    public Transform parent;

    [SerializeField] private uint _rowLimit = 3; //x
    [SerializeField] private uint _floorLimit = 3; //y
    [SerializeField] private uint _columnLimit = 3; //z

    private uint _currentRow = 0;
    private uint _currentFloor = 0;
    private uint _currentColumn = 0;


     private float xOffset = 0;
     private float yOffset = 0;
     private float zOffset = 0;

     private float xIncreacer = 0;
     private float yIncreacer = 0;
     private float zIncreacer = 0;

    
    void Start()
    {
        StartCoroutine(enumerator());
    }

    IEnumerator enumerator()
    {
        for (int i = 0; i < 9; i++)
        {

            if (!RowNotFull() && ColumnNotFull())
            {
                NewColumn();
            }
            else if (FloorNotFull())
            {
                NewFloor();
            }

            SpawnInCell();
            yield return new WaitForSeconds(1);
        }
    }

    private void SpawnInCell()
    {
        Vector3 newPos = new Vector3(
                       x: xOffset,
                       y: yOffset,
                       z: zOffset
                       );
        Instantiate(block, newPos, Quaternion.identity, parent);
        xOffset += xIncreacer;
        _currentRow++;
    }

    private void NewColumn()
    {
        xOffset = 0;
        zOffset += zIncreacer;
        _currentColumn++;
        _currentRow = 0;
    }

    private void NewFloor()
    {
        xOffset = 0;
        zOffset = 0;
        yOffset += yIncreacer;

        _currentColumn = 0;
        _currentRow = 0;
        _currentFloor++;
    }
    private bool RowNotFull()
    {
        return _currentRow < _rowLimit;
    }

    private bool ColumnNotFull()
    {
        return _currentColumn < _columnLimit;
    }

    private bool FloorNotFull()
    {
        return _currentFloor < _floorLimit;
    }
}


