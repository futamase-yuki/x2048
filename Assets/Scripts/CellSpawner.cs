using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CellSpawner : MonoBehaviour
{
    [SerializeField] private Cell _cellPrefab;
    [SerializeField] private Vector2 _upperLeftCellPosition;
    [SerializeField] private GameObject _root;
    
    private float _cellSize = 100f;
    private float _spacerWidth = 4f;
    private float _padding => _cellSize + _spacerWidth;

    private async void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            Spawn(i % 4, i / 4);
        }
    }

    // (0, 0) to (3, 3) 
    public Cell Spawn(int x, int y)
    {
        // -156 156
        // 1増えるごとに(104, -104)
        var p_x = _upperLeftCellPosition.x + _padding * x;
        var p_y = _upperLeftCellPosition.y - _padding * y;

        var cell = Instantiate(_cellPrefab);
        var rect = cell.transform as RectTransform;
        rect.SetParent(transform);
        rect.anchoredPosition = new Vector3(p_x, p_y);
        Debug.LogError(rect.position);

        return cell;
    }
}

// テストコード書くか...