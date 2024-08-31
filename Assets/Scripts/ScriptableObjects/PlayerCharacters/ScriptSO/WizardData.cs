using UnityEngine;

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
