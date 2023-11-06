using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CellSpawner : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Vector2 _upperLeftCellPosition;
    [SerializeField] private GameObject _root;
    
    private float _cellSize = 100f;
    private float _spacerWidth = 4f;
    private float _padding => _cellSize + _spacerWidth;

    // (0, 0) to (3, 3) 
    public Cell Spawn(int x, int y)
    {
        // -156 156
        // 1増えるごとに(104, -104)
        var p_x = _upperLeftCellPosition.x + _padding * x;
        var p_y = _upperLeftCellPosition.y - _padding * y;

        var cell = Instantiate(_cellPrefab, _root.transform);
        cell.SetPosition(new Vector2(p_x, p_y));
        cell.Animate();

        return cell;
    }

    public void SetPosition(Cell cell, int x, int y)
    {
        var p_x = _upperLeftCellPosition.x + _padding * x;
        var p_y = _upperLeftCellPosition.y - _padding * y;

        cell.Move(new Vector2(p_x, p_y));
    }

    public void MoveAndKill(Cell cell, int x, int y, UnityAction callback = null)
    {
        var p_x = _upperLeftCellPosition.x + _padding * x;
        var p_y = _upperLeftCellPosition.y - _padding * y;

        cell.Move(new Vector2(p_x, p_y), () =>
        {
            Destroy(cell.gameObject);
            callback?.Invoke();
        });
        
    }
}
