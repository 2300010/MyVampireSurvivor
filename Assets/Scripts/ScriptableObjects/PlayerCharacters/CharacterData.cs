using UnityEngine;

public abstract class CharacterData : ScriptableObject
{
    public string characterName;
    public int baseHp;
    public int baseDmg;
    public float baseSpeed;

    public abstract void InitializeData(int characterBaseHp, int characterBaseDmg, float characterBaseSpeed);
}


[CreateAssetMenu(fileName = "WizardData", menuName = "ScriptableObjects/Characters/Wizard")]
public class WizardData : CharacterData
{
    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }
}
