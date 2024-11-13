using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector] public HealthSystem healthSystem { get; private set; }

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }
}