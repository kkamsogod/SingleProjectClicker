using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStats;
    public CharacterStat CurrentStat { get; private set; } = new();

    public List<CharacterStat> statsModifiers = new List<CharacterStat>();

    private readonly float MinAttackDelay = 0.03f;
    private readonly float MinAttackPower = 0.5f;
    private readonly float MinAttackSize = 0.4f;
    private readonly float MinAttackSpeed = .1f;

    private readonly float MinSpeed = 0.8f;

    private readonly int MinMaxHealth = 5;

    private void Awake()
    {
        if (baseStats.defaultAttackSO != null)
        {
            baseStats.defaultAttackSO = Instantiate(baseStats.defaultAttackSO);
            CurrentStat.defaultAttackSO = Instantiate(baseStats.defaultAttackSO);
        }

        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        ApplyStatModifiers(baseStats);

        foreach (var modifier in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            ApplyStatModifiers(modifier);
        }
    }

    public void AddStatModifier(CharacterStat statModifier)
    {
        statsModifiers.Add(statModifier);
        UpdateCharacterStat();
    }

    public void RemoveStatModifier(CharacterStat statModifier)
    {
        statsModifiers.Remove(statModifier);
        UpdateCharacterStat();
    }

    private void ApplyStatModifiers(CharacterStat modifier)
    {
        Func<float, float, float> operation = modifier.statsChangeType switch
        {
            StatsChangeType.Add => (current, change) => current + change,
            StatsChangeType.Multiple => (current, change) => current * change,
            _ => (current, change) => change,
        };

        UpdateBasicStats(operation, modifier);
        UpdateAttackStats(operation, modifier);

        if (CurrentStat.defaultAttackSO is RangedAttackSO currentRanged && modifier.defaultAttackSO is RangedAttackSO newRanged)
        {
            UpdateRangedAttackStats(operation, currentRanged, newRanged);
        }
    }

    private void UpdateBasicStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        CurrentStat.maxHealth = Mathf.Max((int)operation(CurrentStat.maxHealth, modifier.maxHealth), MinMaxHealth);
        CurrentStat.speed = Mathf.Max(operation(CurrentStat.speed, modifier.speed), MinSpeed);
    }

    private void UpdateAttackStats(Func<float, float, float> operation, CharacterStat modifier)
    {
        if (CurrentStat.defaultAttackSO == null || modifier.defaultAttackSO == null) return;

        var currentAttack = CurrentStat.defaultAttackSO;
        var newAttack = modifier.defaultAttackSO;

        currentAttack.delay = Mathf.Max(operation(currentAttack.delay, newAttack.delay), MinAttackDelay);
        currentAttack.power = Mathf.Max(operation(currentAttack.power, newAttack.power), MinAttackPower);
        currentAttack.size = Mathf.Max(operation(currentAttack.size, newAttack.size), MinAttackSize);
        currentAttack.speed = Mathf.Max(operation(currentAttack.speed, newAttack.speed), MinAttackSpeed);
    }

    private void UpdateRangedAttackStats(Func<float, float, float> operation, RangedAttackSO currentRanged, RangedAttackSO newRanged)
    {
        currentRanged.multipleProjectilesAngle = operation(currentRanged.multipleProjectilesAngle, newRanged.multipleProjectilesAngle);
        currentRanged.numberofProjectilesPerShot = Mathf.CeilToInt(operation(currentRanged.numberofProjectilesPerShot, newRanged.numberofProjectilesPerShot));
    }

    private Color UpdateColor(Func<float, float, float> operation, Color current, Color modifier)
    {
        return new Color(
            operation(current.r, modifier.r),
            operation(current.g, modifier.g),
            operation(current.b, modifier.b),
            operation(current.a, modifier.a));
    }
}