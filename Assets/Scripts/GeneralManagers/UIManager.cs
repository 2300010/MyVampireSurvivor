using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance => instance;

    [SerializeField] Text levelValueText;
    [SerializeField] Text expValueText;

    private void Start()
    {
        instance = this;
        Reset();
    }

    public void Reset()
    {
        UpdateExpValue();
        UpdateLevelValue();
        GameManager.LevelUp += UpdateLevelValue;
        GameManager.ExpGained += UpdateExpValue;
    }

    private void UpdateLevelValue()
    {
        levelValueText.text = GameManager.Instance.PlayerLevel.ToString();
    }

    private void UpdateExpValue()
    {
        expValueText.text = GameManager.Instance.PlayerExp.ToString();
    }

    private void OnDestroy()
    {
        GameManager.LevelUp -= UpdateLevelValue;
        GameManager.ExpGained -= UpdateExpValue;
    }
}
