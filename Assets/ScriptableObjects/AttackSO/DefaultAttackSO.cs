using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "Attacks/Default", order = 0)]
public class DefaultAttackSO : ScriptableObject
{
    [Header("Default Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;
}