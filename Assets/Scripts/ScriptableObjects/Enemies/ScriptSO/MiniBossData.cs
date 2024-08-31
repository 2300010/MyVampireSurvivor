using UnityEngine;

[CreateAssetMenu(fileName = "MiniBossData", menuName = "ScriptableObjects/Enemies/MiniBoss")]
public class MiniBossData : EnemyData
{
    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }
}
