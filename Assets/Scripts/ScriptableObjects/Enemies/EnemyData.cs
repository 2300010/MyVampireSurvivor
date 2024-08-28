using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    public string enemyName;
    public int baseHp;
    public int baseDmg;
    public float baseSpeed;

    public abstract void InitializeData(int hp, int dmg, float speed);
}

//[CreateAssetMenu(fileName = "EliteEnemyData", menuName = "ScriptableObjects/Enemies/EliteEnemy")]
public class EliteEnemyData : EnemyData
{
    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }
}

//[CreateAssetMenu(fileName = "MiniBossData", menuName = "ScriptableObjects/Enemies/MiniBoss")]
public class MiniBossData : EnemyData
{
    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }
}

//  [CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObjects/Enemies/Boss")]
public class BossData : EnemyData
{
    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }
}
