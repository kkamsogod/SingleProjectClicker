using System;
using System.Numerics;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;
    private CharacterStatHandler statHandler;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;
    private BigInteger coinReward;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;
    public event Action OnHealthChanged;

    public float CurrentHealth
    {
        get => statHandler.playerStat.currentHealth;
        private set => statHandler.playerStat.currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }

    public float MaxHealth
    {
        get => statHandler.playerStat.maxHealth;
        private set => statHandler.playerStat.maxHealth = value;
    }

    private void Awake()
    {
        statHandler = GetComponent<CharacterStatHandler>();
    }

    private void Start()
    {
        MaxHealth = statHandler.playerStat.maxHealth;
        CurrentHealth = MaxHealth;
        OnDeath += HandleDeath;
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
        if (CurrentHealth <= 0)
        {
            return false;
        }

        CurrentHealth += change;
        OnHealthChanged?.Invoke();

        if (CurrentHealth <= 0f)
        {
            CurrentHealth = 0;
            OnHealthChanged?.Invoke();
            OnDeath?.Invoke();
            return true;
        }

        if (change > 0)
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

    public void SetMaxHealthMultiplier(float multiplier)
    {
        MaxHealth *= multiplier;
        CurrentHealth = MaxHealth;
        OnHealthChanged?.Invoke();
    }

    public void InitializeCurrentHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public void SetCoinReward(BigInteger reward)
    {
        coinReward = reward;
    }

    private void HandleDeath()
    {
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddCoins(coinReward);
        }

        Destroy(gameObject);
    }
}