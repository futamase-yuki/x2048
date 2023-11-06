using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellController : MonoBehaviour
{
    [SerializeField] private CellSpawner _cellSpawner;
    
    private List<List<Cell>> _cells = new();
    
    public enum Direction { Up, Down, Right, Left }

    //1行あたりの最大セル数
    private int _maxCellSizePerLine;
    
    //  1つ前のターンで取得した合計スコア
    public int PreviousObtainedTotalScore { get; private set; }

    public void Initialize(int size)
    {
        Clean();
        
        // size * sizeの2次元配列を作る
        _cells = Enumerable.Range(0, size)
            .Select(_ => Enumerable.Range(0, size)
                .Select(_ => (Cell) null)
                .ToList())
            .ToList();
        
        _maxCellSizePerLine = size;
        
        FirstSpawn();
        
        LogCells();
    }
    
    public void Move(Direction direction)
    {
        PreviousObtainedTotalScore = 0;
        
        if (direction == Direction.Left)
        {
            //左に入力されたら左端に寄せる
            MoveLeft();
        }
        else if (direction == Direction.Right)
        {
            MoveRight();
        }
        else if (direction == Direction.Up)
        {
            MoveUp();
        }
        else if (direction == Direction.Down)
        {
            MoveDown();
        }
    }

    // 左側
    public void MoveLeft()
    {
        bool moved = false;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = j + 1; k < 4; k++)
                {
                    if (_cells[i][k] is not null)
                    {
                        if (_cells[i][j] is null)
                        {
                            _cells[i][j] = _cells[i][k];
                            _cellSpawner.SetPosition(_cells[i][j], j, i);
                            _cells[i][k] = null;
                            moved = true;
                        }
                        else if (_cells[i][j].Number == _cells[i][k].Number)
                        {
                            _cells[i][j].SetNumber(_cells[i][j].Number * 2);
                            // スコア加算
                            PreviousObtainedTotalScore += _cells[i][j].Number;
                            
                            _cellSpawner.MoveAndKill(_cells[i][k], j, i, () =>
                            {
                            });
                            _cells[i][k] = null;
                            moved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        LogCells();
        if (moved)
        {
            GenerateNewNumber();
        }
    }

    // 右側
    private void MoveRight()
    {
        bool moved = false;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 3; j >= 0; j--)
            {
                for (int k = j - 1; k >= 0; k--)
                {
                    if (_cells[i][k] is not null)
                    {
                        if (_cells[i][j] is null)
                        {
                            _cells[i][j] = _cells[i][k];
                            _cellSpawner.SetPosition(_cells[i][j], j, i);
                            _cells[i][k] = null;
                            moved = true;
                        }
                        else if (_cells[i][j].Number == _cells[i][k].Number)
                        {
                            _cells[i][j].SetNumber(_cells[i][j].Number * 2);
                            // スコア加算
                            PreviousObtainedTotalScore += _cells[i][j].Number;
                            
                            _cellSpawner.MoveAndKill(_cells[i][k], j, i, () =>
                            {
                            });
                            _cells[i][k] = null;
                            moved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        LogCells();
        if (moved)
        {
            GenerateNewNumber();
        }
    }

    // 上側
    private void MoveUp()
    {
        bool moved = false;
        for (int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int k = i + 1; k < 4; k++)
                {
                    if (_cells[k][j] is not null)
                    {
                        if (_cells[i][j] is null)
                        {
                            _cells[i][j] = _cells[k][j];
                            _cellSpawner.SetPosition(_cells[i][j], j, i);
                            _cells[k][j] = null;
                            moved = true;
                        }
                        else if (_cells[i][j].Number == _cells[k][j].Number)
                        {
                            _cells[i][j].SetNumber(_cells[i][j].Number * 2);
                            // スコア加算
                            PreviousObtainedTotalScore += _cells[i][j].Number;
                            
                            _cellSpawner.MoveAndKill(_cells[k][j], j, i, () =>
                            {
                            });
                            _cells[k][j] = null;
                            moved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        LogCells();
        if (moved)
        {
            GenerateNewNumber();
        }
    }
    
    // 下側
    private void MoveDown()
    {
        bool moved = false;
        for (int j = 0; j < 4; j++)
        {
            for (int i = 3; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    if (_cells[k][j] is not null)
                    {
                        if (_cells[i][j] is null)
                        {
                            _cells[i][j] = _cells[k][j];
                            _cellSpawner.SetPosition(_cells[i][j], j, i);
                            _cells[k][j] = null;
                            moved = true;
                        }
                        else if (_cells[i][j].Number == _cells[k][j].Number)
                        {
                            _cells[i][j].SetNumber(_cells[i][j].Number * 2);
                            // スコア加算
                            PreviousObtainedTotalScore += _cells[i][j].Number;
                            
                            _cellSpawner.MoveAndKill(_cells[k][j], j, i, () =>
                            {
                            });
                            _cells[k][j] = null;
                            moved = true;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        LogCells();
        if (moved)
        {
            GenerateNewNumber();
        }
    }

    private System.Random random = new();
    private void GenerateNewNumber()
    {
        if (!IsFull())
        {
            int x, y;
            do
            {
                x = random.Next(0, 4);
                y = random.Next(0, 4);
            }
            while (_cells[x][y] is not null);
            _cells[x][y] = _cellSpawner.Spawn(y, x);
            _cells[x][y].SetNumber(random.Next(0, 4) == 0 ? 4 : 2);
        }
        LogCells();
    }

    public bool IsFull()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_cells[i][j] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    private void FirstSpawn()
    {
        var (a, b) = ChoiceRandomPoint(_maxCellSizePerLine * _maxCellSizePerLine);
        
        var cell1 = _cellSpawner.Spawn(a % _maxCellSizePerLine, a / _maxCellSizePerLine);
        cell1.SetNumber(2);
        _cells[a / _maxCellSizePerLine][a % _maxCellSizePerLine] = cell1;
        
        var cell2 = _cellSpawner.Spawn(b % _maxCellSizePerLine, b / _maxCellSizePerLine);
        cell2.SetNumber(2);
        _cells[b / _maxCellSizePerLine][b % _maxCellSizePerLine] = cell2;
    }
    
    public void Spawn()
    {
    }

    private void Clean()
    {
        // cellを消す
        foreach (var cell in _cells.SelectMany(cells => cells))
        {
            if (cell is not null)
            {
                Destroy(cell.gameObject);
            }
        }
    }

    private bool CheckGameEnd()
    {
        return true;
    }
    
    /// <summary>
    /// 1からlengthまでの数字からランダムに2つ選ぶ
    /// </summary>
    /// <param name="length">1より大きい数字</param>
    /// <returns></returns>
    public (int, int) ChoiceRandomPoint(int length)
    {
        var rand = new System.Random();
        var numbers = Enumerable.Range(1, length).ToArray();

        // 配列をシャッフル
        for (var i = numbers.Length - 1; i > 0; i--)
        {
            var j = rand.Next(i + 1);
            (numbers[i], numbers[j]) = (numbers[j], numbers[i]);
        }

        // シャッフルした配列の最初の2つの要素を取得
        return (numbers[0] - 1, numbers[1] - 1);
    }
    
    // cellsの中身を一回のLogで表示する
    // こんな感じに
    // 2 0 0 0
    // 0 0 0 0
    // 0 0 0 0
    // 0 0 0 0
    private void LogCells()
    {
        var log = "";
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                var cell = _cells[i][j];
                log += cell is null ? "0 " : $"{cell.Number} ";
            }
            log += "\n";
        }
        Debug.Log(log);
    }
}
