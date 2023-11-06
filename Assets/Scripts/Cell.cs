using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private Image _background;

    public int Number { get; private set; } = 2;

    public void Animate()
    {
        // 0.5秒かけて拡大
        gameObject.transform.DOScale(1, 0.3f).SetEase(Ease.InBounce).From(Vector3.zero);
    }

    public void SetNumber(int number)
    {
        Assert.IsTrue(number % 2 == 0);

        _numberText.text = number.ToString();

        Number = number;
    }
    
    public void SetPosition(Vector2 position)
    {
        var rect = transform as RectTransform;
        rect.anchoredPosition = position;
    }

    public void Move(Vector2 position, UnityAction callback = null)
    {
        var rect = transform as RectTransform;
        rect.DOAnchorPos(position, 0.3f).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }
}
