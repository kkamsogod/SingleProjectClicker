using UnityEngine;
using System.Collections.Generic;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField] private RangedAttackSO initialProjectileData;
    private RangedAttackRuntimeData currentProjectileData;

    private void Start()
    {
        if (initialProjectileData != null)
        {
            currentProjectileData = new RangedAttackRuntimeData(initialProjectileData);
        }
    }

    public RangedAttackRuntimeData GetCurrentProjectileData()
    {
        return currentProjectileData;
    }

    public void UpgradeProjectile(RangedAttackSO newProjectileData)
    {
        if (newProjectileData != null)
        {
            currentProjectileData = new RangedAttackRuntimeData(newProjectileData);
            Debug.Log($"[ProjectileManager] Projectile upgraded to: {newProjectileData.name}");
        }
    }
}