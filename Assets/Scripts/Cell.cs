using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private Image _background;

    public void SetNumber(int number)
    {
        Assert.IsTrue(number % 2 == 0);

        _numberText.text = number.ToString();
    }
}
