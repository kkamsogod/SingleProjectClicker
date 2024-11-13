using UnityEngine;
using System.Collections;
using System;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] private RangedAttackSO rangedAttackData;
    private RangedAttackRuntimeData attackData;
    [SerializeField] private Transform firePoint;
    private bool canAttack = true;
    private float delayReduction = 0f;

    private PlayerAnimationController animationController;

    private void Start()
    {
        attackData = new RangedAttackRuntimeData(rangedAttackData);

        animationController = GetComponent<PlayerAnimationController>();

        var player = GetComponent<Player>();
        if (player != null)
        {
            player.OnAttack += TryFire;
        }

        ResetData();
    }

    private void ResetData()
    {
        delayReduction = 0f;
        attackData.delay = rangedAttackData.delay;
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

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackData.delay);
        canAttack = true;
    }

    public void UpgradeAttack(RangedAttackSO newAttackData)
    {
        if (newAttackData != null)
        {
            attackData = new RangedAttackRuntimeData(newAttackData);
            attackData.delay = Mathf.Max(attackData.delay - delayReduction, 0.1f);
        }
    }

    public void UpgradeAttackSpeed(float speedIncrease)
    {
        delayReduction += speedIncrease;
        attackData.delay = Mathf.Max(attackData.delay - speedIncrease, 0.1f);
    }
}