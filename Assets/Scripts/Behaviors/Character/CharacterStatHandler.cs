using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    public CharacterStat playerStat;

    public float TotalAttackPower => playerStat.basePower + ProjectileManager.Instance.GetCurrentProjectileData().power;
    public float FinalAttackSpeed => ProjectileManager.Instance.GetCurrentProjectileData().delay / (1 + playerStat.baseAttackSpeed);

    public void IncreasePower(float amount)
    {
        playerStat.basePower += amount;
    }

    public void UpgradeAttackSpeed(float speedIncrease)
    {
        playerStat.baseAttackSpeed += speedIncrease;
    }

    public void IncreaseMaxHealth(float amount)
    {
        playerStat.maxHealth += amount;
    }

    public void ChangeCurrentHealth(float amount)
    {
        playerStat.currentHealth = Mathf.Clamp(playerStat.currentHealth + amount, 0, playerStat.maxHealth);
    }

    public void SetCurrentHealth(float amount)
    {
        playerStat.currentHealth = Mathf.Clamp(amount, 0, playerStat.maxHealth);
    }

    public void SetMaxHealth(float amount)
    {
        playerStat.maxHealth = amount;
        playerStat.currentHealth = Mathf.Clamp(playerStat.currentHealth, 0, playerStat.maxHealth);
    }
}