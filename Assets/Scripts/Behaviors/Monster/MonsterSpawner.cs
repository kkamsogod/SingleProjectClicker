using System.Collections;
using System.Numerics;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnDelay = 1.0f;
    [SerializeField] private float healthMultiplier = 1.5f;
    [SerializeField] private float coinMultiplier = 1.5f;
    [SerializeField] private string initialCoinRewardString = "10";
    [SerializeField] private float sizeMultiplier;

    private BigInteger initialCoinReward;
    private float currentHealthMultiplier = 1.0f;
    private BigInteger currentCoinReward;
    private bool isFirstMonsterSpawned = false;
    private float monsterOriginalScale = 1;

    private void Start()
    {
        initialCoinReward = BigInteger.Parse(initialCoinRewardString);
        currentCoinReward = initialCoinReward;
        SpawnInitialMonster();
    }

    private void SpawnInitialMonster()
    {
        GameObject initialMonster = Instantiate(monsterPrefab, spawnPoint.position, monsterPrefab.transform.rotation);
        InitializeMonster(initialMonster);
    }

    private void SpawnMonster()
    {
        GameObject newMonster = Instantiate(monsterPrefab, spawnPoint.position, monsterPrefab.transform.rotation);
        InitializeMonster(newMonster);
    }

    private void InitializeMonster(GameObject monsterObject)
    {
        monsterObject.transform.localScale *= monsterOriginalScale;
        HealthSystem healthSystem = monsterObject.GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.SetMaxHealthMultiplier(currentHealthMultiplier);
            healthSystem.SetCoinReward(currentCoinReward);

            healthSystem.OnDeath += () =>
            {
                if (!isFirstMonsterSpawned)
                {
                    isFirstMonsterSpawned = true;
                }
                StartCoroutine(RespawnWithDelay());
                IncreaseRewardAndHealth();
            };
        }

        GameManager.Instance.NotifyMonsterSpawned(monsterObject.GetComponent<Monster>());
    }

    private IEnumerator RespawnWithDelay()
    {
        if (isFirstMonsterSpawned)
        {
            yield return new WaitForSeconds(spawnDelay);
        }
        SpawnMonster();
    }

    private void IncreaseRewardAndHealth()
    {
        currentHealthMultiplier *= healthMultiplier;
        currentCoinReward = (currentCoinReward * 15) / 10;
        monsterOriginalScale *= sizeMultiplier;
    }
}