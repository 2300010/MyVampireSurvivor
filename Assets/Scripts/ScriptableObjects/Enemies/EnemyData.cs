using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    public string enemyName;
    public int baseHp;
    public int baseDmg;
    public float baseSpeed;

    public abstract void InitializeData(int hp, int dmg, float speed);
}

[CreateAssetMenu(fileName = "SkeletonSoldierData", menuName = "ScriptableObjects/Enemies/SkeletonSoldier")]
public class SkeletonSoldierData : EnemyData
{
    public override void InitializeData(int hp, int dmg, float speed)
    {
        baseHp = hp;
        baseDmg = dmg;
        baseSpeed = speed;
    }
}
