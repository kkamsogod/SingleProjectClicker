using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerInput playerInput { get; private set; }
    [HideInInspector] public ProjectileAttack projectileAttack { get; private set; }
    [HideInInspector] public PlayerAnimationController playerAnimationController { get; private set; }
    [HideInInspector] public HealthSystem healthSystem { get; private set; }
    [HideInInspector] public CharacterStatHandler characterStatHandler { get; private set; }

    public event Action OnAttack;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        projectileAttack = GetComponent<ProjectileAttack>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
        healthSystem = GetComponent<HealthSystem>();
        characterStatHandler = GetComponent<CharacterStatHandler>();
    }

    public void TriggerAttack()
    {
        OnAttack?.Invoke();
    }
}