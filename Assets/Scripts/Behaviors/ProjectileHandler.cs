using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private float power;
    private float size;
    private float speed;
    private LayerMask targetLayer;

    public void Initialize(RangedAttackSO attackSO)
    {
        power = attackSO.power;
        size = attackSO.size;
        speed = attackSO.speed;
        targetLayer = attackSO.target;

        transform.localScale = Vector3.one * size;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            Debug.Log($"Hit {other.gameObject.name} with power {power}");
            Destroy(gameObject);
        }
    }
}