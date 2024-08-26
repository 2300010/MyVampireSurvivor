using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    private string characterName;
    private int currentHp;
    private int currentDmg;
    private float currentSpeed;

    public string CharacterName { get => characterName; set => characterName = value; }
    public int CurrentHp { get => currentHp; set => currentHp = value; }
    public int CurrentDmg { get => currentDmg; set => currentDmg = value; }
    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
}
