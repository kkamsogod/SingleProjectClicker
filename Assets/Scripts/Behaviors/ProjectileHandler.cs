using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private float power;
    private float size;
    private float speed;
    private LayerMask targetLayer;
    private float duration;

    private void Start()
    {
        Initialize(ProjectileManager.Instance.GetCurrentProjectileData());
    }

    public void Initialize(RangedAttackRuntimeData attackData)
    {
        if (attackData == null) return;

        power = attackData.power;
        size = attackData.size;
        speed = attackData.speed;
        targetLayer = attackData.target;
        duration = attackData.duration;

        transform.localScale = Vector3.one * size;
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            HealthSystem targetHealth = other.GetComponent<HealthSystem>();

            if (targetHealth != null)
            {
                var characterStatHandler = GameManager.Instance.player.characterStatHandler;
                var totalDamage = characterStatHandler.TotalAttackPower;
                targetHealth.ChangeHealth(-totalDamage);
            }

            Debug.Log($"Hit {other.gameObject.name} with combined power {power} + player power");
            Destroy(gameObject);
        }
    }
}