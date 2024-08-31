using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
    public EnemyName enemyName;
    public int baseHp;
    public int baseDmg;
    public float baseSpeed;

    public abstract void InitializeData(int hp, int dmg, float speed);
}