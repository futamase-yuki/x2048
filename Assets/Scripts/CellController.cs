using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    private List<List<Cell>> _cells = new();
    
    private enum Direction{ Up, Down, Right, Left}
    
    private void Move(Direction direction)
    {
        if (direction == Direction.Left)
        {
            
        }
    }

    private void Spawn()
    {
        
    }

    private bool CheckGameEnd()
    {
        return true;
    }
}
