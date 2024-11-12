using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] private RangedAttackSO attackData;
    [SerializeField] private Transform firePoint;

    private void Start()
    {
        var player = GetComponent<Player>();
        if (player != null)
        {
            player.OnAttack += Fire;
        }
    }

    public void Fire()
    {
        if (attackData == null || firePoint == null)
        {
            return;
        }

        for (int i = 0; i < attackData.numberofProjectilesPerShot; i++)
        {
            float angleOffset = (i - (attackData.numberofProjectilesPerShot - 1) / 2f) * attackData.multipleProjectilesAngle;
            Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angleOffset);

            GameObject projectile = Instantiate(Resources.Load<GameObject>(attackData.bulletNameTag), firePoint.position, rotation);

            if (projectile != null)
            {
                projectile.GetComponent<ProjectileHandler>().Initialize(attackData);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = rotation * Vector2.right * attackData.speed;
                }
            }
        }
    }

    public void UpgradeAttack(RangedAttackSO newAttackData)
    {
        if (newAttackData != null)
        {
            attackData = newAttackData;
        }
    }
}