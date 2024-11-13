using UnityEngine;
using System.Collections;

public class ProjectileAttack : MonoBehaviour
{
    private CharacterStatHandler characterStatHandler;
    private PlayerAnimationController animationController;
    [SerializeField] private Transform firePoint;
    private bool canAttack = true;

    private void Start()
    {
        characterStatHandler = GameManager.Instance.player.characterStatHandler;
        animationController = GameManager.Instance.player.playerAnimationController;

        GameManager.Instance.player.OnAttack += TryFire;
    }

    public void TryFire()
    {
        if (canAttack)
        {
            Fire();
            animationController?.TriggerAttackAnimation();
            StartCoroutine(AttackCooldown());
        }
    }

    public void Fire()
    {
        if (characterStatHandler == null || firePoint == null) return;

        var attackData = ProjectileManager.Instance.GetCurrentProjectileData();
        if (attackData == null)
        {
            Debug.LogError("Attack data is null.");
            return;
        }

        Debug.Log($"Attempting to fire: BulletTag={attackData.bulletNameTag}, Projectiles={attackData.numberofProjectilesPerShot}");

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
            else
            {
                Debug.LogError($"Failed to load projectile prefab with tag: {attackData.bulletNameTag}");
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(characterStatHandler.FinalAttackSpeed);
        canAttack = true;
    }

    public void UpgradeAttackSpeed(float speedIncrease)
    {
        characterStatHandler.UpgradeAttackSpeed(speedIncrease);
    }
}