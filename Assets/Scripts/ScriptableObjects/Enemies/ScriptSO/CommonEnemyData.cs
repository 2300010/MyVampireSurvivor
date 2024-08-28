using UnityEngine;

[CreateAssetMenu(fileName = "CommonEnemyData", menuName = "ScriptableObjects/Enemies/CommonEnemy")]
public class CommonEnemyData : EnemyData
{

    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }

}
