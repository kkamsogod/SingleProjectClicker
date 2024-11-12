using UnityEngine;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "Attacks/Ranged", order = 1)]
public class RangedAttackSO : DefaultAttackSO
{
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public int numberofProjectilesPerShot;
    public float multipleProjectilesAngle;
    public int level;
    public int upgradeCost;
    public GameObject equipequippedProjectilePrefab;
}