using UnityEngine;

public class ExpFlameManager : MonoBehaviour
{
    int expGiven;
    Color expColor;
    GameObject deadEnemy;

    public int ExpGiven { get => expGiven; set => expGiven = value; }
    public Color ExpColor { get => expColor; set => expColor = value; }
    public GameObject DeadEnemy { get => deadEnemy; set => deadEnemy = value; }
}
