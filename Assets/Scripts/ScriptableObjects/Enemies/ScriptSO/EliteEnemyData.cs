using UnityEngine;

[CreateAssetMenu(fileName = "EliteEnemyData", menuName = "ScriptableObjects/Enemies/EliteEnemy")]
public class EliteEnemyData : EnemyData
{
    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }
}
