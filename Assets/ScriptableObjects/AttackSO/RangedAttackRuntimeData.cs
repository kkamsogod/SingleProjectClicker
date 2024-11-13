using UnityEngine;
using System.Numerics;

[System.Serializable]
public class RangedAttackRuntimeData
{
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    public string bulletNameTag;
    public int numberofProjectilesPerShot;
    public float multipleProjectilesAngle;
    public int level;
    public float duration;
    public BigInteger upgradeCost;
    public GameObject equippedProjectilePrefab;

    public RangedAttackRuntimeData(RangedAttackSO rangedData)
    {
        size = rangedData.size;
        delay = rangedData.delay;
        power = rangedData.power;
        speed = rangedData.speed;
        target = rangedData.target;
        bulletNameTag = rangedData.bulletNameTag;
        numberofProjectilesPerShot = rangedData.numberofProjectilesPerShot;
        multipleProjectilesAngle = rangedData.multipleProjectilesAngle;
        duration = rangedData.duration;
        upgradeCost = rangedData.upgradeCost;
        equippedProjectilePrefab = rangedData.equippedProjectilePrefab;
    }
}