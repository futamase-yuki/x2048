using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class GameGlobalManager : SingletonMonoBehaviour<GameGlobalManager>
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private CellController _cellController;
    [SerializeField] private GameObject _finishText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _totalScore = 0;

    void Start()
    {
        InitializeGame();
        
        _restartButton.onClick.AddListener(InitializeGame);
    }

    private void InitializeGame()
    {
        _cellController.Initialize(4);

        _finishText.SetActive(false);

        _totalScore = 0;
    }
    
    // 上下左右の入力を受け付ける
    private void Update()
    {
        if (Input.anyKey == false)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _cellController.Move(CellController.Direction.Up);
            _totalScore += _cellController.PreviousObtainedTotalScore;
            _scoreText.text = _totalScore.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _cellController.Move(CellController.Direction.Down);
            _totalScore += _cellController.PreviousObtainedTotalScore;
            _scoreText.text = _totalScore.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _cellController.Move(CellController.Direction.Right);
            _totalScore += _cellController.PreviousObtainedTotalScore;
            _scoreText.text = _totalScore.ToString();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _cellController.Move(CellController.Direction.Left);
            _totalScore += _cellController.PreviousObtainedTotalScore;
            _scoreText.text = _totalScore.ToString();
        }
        

        if (_cellController.IsFull())
        {
            FinishGame();
        }
    }

    private void FinishGame()
    {
        _finishText.SetActive(true);
        
        _finishText.gameObject.transform.localScale = Vector3.zero;
        _finishText.gameObject.transform.DOScale(1, 0.1f);
    }
}
