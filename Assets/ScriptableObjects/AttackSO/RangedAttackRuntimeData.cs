using UnityEngine;

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
    public int upgradeCost;
    public GameObject equipequippedProjectilePrefab;

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
        level = rangedData.level;
        upgradeCost = rangedData.upgradeCost;
        equipequippedProjectilePrefab = rangedData.equipequippedProjectilePrefab;
    }
}