using UnityEngine;
using System.Numerics;

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "Attacks/Ranged", order = 1)]
public class RangedAttackSO : DefaultAttackSO
{
    [Header("Ranged Attack Info")]
    public string bulletNameTag;
    public int numberofProjectilesPerShot;
    public float multipleProjectilesAngle;
    public float duration;
    [SerializeField] private string upgradeCostString;
    public GameObject equippedProjectilePrefab;

    public BigInteger upgradeCost;

    private void OnValidate()
    {
        if (BigInteger.TryParse(upgradeCostString, out BigInteger result))
        {
            upgradeCost = result;
        }
        else
        {
            Debug.LogWarning("유효하지 않은 BigInteger 값입니다.");
        }
    }
}