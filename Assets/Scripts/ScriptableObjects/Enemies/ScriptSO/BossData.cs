using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObjects/Enemies/Boss")]
public class BossData : EnemyData
{
    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }
}
