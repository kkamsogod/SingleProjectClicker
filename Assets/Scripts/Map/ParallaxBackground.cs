using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private float scrollAmount;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveDirection;

    private void Update()
    {
        foreach (Transform background in backgrounds)
        {
            background.position += moveDirection * moveSpeed * Time.deltaTime;

            if (background.position.x <= -scrollAmount)
            {
                float maxRightX = GetMaxRightPosition();
                background.position = new Vector3(maxRightX + scrollAmount - 0.1f, background.position.y, background.position.z);
            }
        }
    }

    private float GetMaxRightPosition()
    {
        float maxX = backgrounds[0].position.x;
        foreach (Transform background in backgrounds)
        {
            if (background.position.x > maxX)
            {
                maxX = background.position.x;
            }
        }
        return maxX;
    }
}