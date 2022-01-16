using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    
    [SerializeField] private Transform _parent;
    [SerializeField] private uint _rowLimit = 3; //x
    [SerializeField] private uint _floorLimit = 3; //y
    [SerializeField] private uint _columnLimit = 3; //z
    [SerializeField] private Mark _markPrefab;

    private List<Mark> _marks = new List<Mark>();
    public List<Mark> Marks => _marks;

    private uint _currentRow = 0;
    private uint _currentFloor = 0;
    private uint _currentColumn = 0;

     private float xOffset = 0;
     private float yOffset = 0;
     private float zOffset = 0;

     private float xIncreacer = .6f;
     private float yIncreacer = .6f;
     private float zIncreacer = .6f;

    private void Awake()
    {
        CreateAllMarks(_rowLimit * _floorLimit * _columnLimit);
    }
    public void SetIncreacers(float x, float y, float z)
    {
        xIncreacer = x;
        yIncreacer = y;
        zIncreacer = z;
    }
   
    public void CreateAllMarks(uint count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateNewPoint();
        }
    }
    private void CreateNewPoint()
    {
        if (!RowNotFull() && ColumnNotFull())
        {
            NewColumn();
        }
        if (FloorNotFull() && !ColumnNotFull())
        {
            NewFloor();
        }
        MarkNewPoint();
    }
  

    private void MarkNewPoint()
    {
        Vector3 newPos = new Vector3(
                       x: xOffset,
                       y: yOffset,
                       z: zOffset
                       );
        newPos += _parent.position;
        Mark newMark = Instantiate(_markPrefab, newPos, Quaternion.identity, _parent);
        Marks.Add(newMark);
       
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


