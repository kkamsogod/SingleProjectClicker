using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (Time.timeScale == 0 || EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.TriggerAttack();
        }
    }
}