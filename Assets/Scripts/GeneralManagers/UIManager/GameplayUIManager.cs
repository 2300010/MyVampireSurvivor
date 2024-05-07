using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    private static GameplayUIManager instance;

    public static GameplayUIManager Instance => instance;

    [SerializeField] Text levelValueText;
    [SerializeField] Text expValueText;

    #region Unity Functions
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
    
    private void OnDestroy()
    {
        GameManager.LevelUp -= UpdateLevelValue;
        GameManager.ExpGained -= UpdateExpValue;
    }
    #endregion

    #region Custom region
    private void UpdateLevelValue()
    {
        levelValueText.text = GameManager.Instance.PlayerLevel.ToString();
    }

    private void UpdateExpValue()
    {
        expValueText.text = GameManager.Instance.PlayerExp.ToString();
    }
    #endregion
}
