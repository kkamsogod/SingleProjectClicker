using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;
    [SerializeField] private AudioClip damageClip;

    private CharacterStatHandler statHandler;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => statHandler.CurrentStat.maxHealth;

    private void Awake()
    {
        statHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        Debug.Log("MaxHealth at Start: " + MaxHealth);
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (isAttacked && timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (CurrentHealth == 0)
        {
            return false;
        }

        CurrentHealth += change;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0f)
        {
            OnDeath?.Invoke();
            return true;
        }
        if (change >= 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }

        return true;
    }
}