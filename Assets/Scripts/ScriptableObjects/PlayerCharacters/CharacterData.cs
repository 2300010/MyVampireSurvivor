using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    public string characterName;
    public int baseHp;
    public int baseDmg;
    public float baseSpeed;

    public abstract void InitializeData(int characterBaseHp, int characterBaseDmg, float characterBaseSpeed);
}