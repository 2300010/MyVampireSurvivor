using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    private static GameplayUIManager instance;

    public static GameplayUIManager Instance => instance;

    [SerializeField] Text levelValueText;
    [SerializeField] Text expValueText;
    [SerializeField] Slider hpSlider;

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
        HealthBarSetup();
        GameManager.LevelUp += UpdateLevelValue;
        GameManager.ExpGained += UpdateExpValue;
        HpManager.PlayerIsTakingDamage += UpdateHpValue;
    }
    
    private void OnDestroy()
    {
        GameManager.LevelUp -= UpdateLevelValue;
        GameManager.ExpGained -= UpdateExpValue;
        HpManager.PlayerIsTakingDamage -= UpdateHpValue;
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

    public void HealthBarSetup()
    {
        hpSlider.maxValue = PlayerManager.Instance().CharacterData.baseHp;
        UpdateHpValue();
        Debug.Log("Slider max value = " + hpSlider.maxValue);
    }

    private void UpdateHpValue()
    {
        hpSlider.value = PlayerManager.Instance().PlayerHpManager.CurrentHp;
    }
    #endregion
}
