using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider monsterHealthSlider;

    private HealthSystem playerHealthSystem;
    private HealthSystem monsterHealthSystem;

    private void Start()
    {
        if (GameManager.Instance.player != null)
        {
            playerHealthSystem = GameManager.Instance.player.healthSystem;
            if (playerHealthSystem != null)
            {
                playerHealthSlider.maxValue = playerHealthSystem.MaxHealth;
                playerHealthSlider.value = playerHealthSystem.CurrentHealth;
                playerHealthSystem.OnHealthChanged += UpdatePlayerSlider;
            }
        }

        UpdateMonsterHealthSystem();
    }

    private void OnEnable()
    {
        GameManager.Instance.OnMonsterSpawned += UpdateMonsterHealthSystem;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnMonsterSpawned -= UpdateMonsterHealthSystem;
    }

    private void UpdatePlayerSlider()
    {
        if (playerHealthSystem != null)
        {
            playerHealthSlider.value = playerHealthSystem.CurrentHealth;
        }
    }

    private void UpdateMonsterSlider()
    {
        if (monsterHealthSystem != null)
        {
            monsterHealthSlider.maxValue = monsterHealthSystem.MaxHealth;
            monsterHealthSlider.value = monsterHealthSystem.CurrentHealth;
        }
    }

    public void UpdateMonsterHealthSystem()
    {
        if (monsterHealthSystem != null)
        {
            monsterHealthSystem.OnHealthChanged -= UpdateMonsterSlider;
            monsterHealthSystem.OnDeath -= ResetMonsterSlider;
        }

        if (GameManager.Instance.monster != null)
        {
            monsterHealthSystem = GameManager.Instance.monster.healthSystem;
            if (monsterHealthSystem != null)
            {
                monsterHealthSlider.maxValue = monsterHealthSystem.MaxHealth;
                monsterHealthSlider.value = monsterHealthSystem.CurrentHealth;
                monsterHealthSystem.OnHealthChanged += UpdateMonsterSlider;
                monsterHealthSystem.OnDeath += ResetMonsterSlider;
            }
        }
    }

    private void ResetMonsterSlider()
    {
        monsterHealthSlider.value = 0;
    }
}