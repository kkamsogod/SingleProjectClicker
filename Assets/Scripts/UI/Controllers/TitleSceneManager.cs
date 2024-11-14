using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleSceneManager : MonoBehaviour
{
    public TextMeshProUGUI titleText1;
    public TextMeshProUGUI titleText2;

    public float scaleSpeed = 1.5f;
    public float minScale = 0.9f;
    public float maxScale = 1.1f;

    public Color startColor = Color.white;
    public Color endColor = new Color(1f, 0.5f, 0.5f, 1f);
    public float colorSpeed = 1f;
    public float minAlpha = 0.3f;
    public float maxAlpha = 1f;

    private float colorTimer = 0f;
    private float scaleTimer = 0f;
    private float alphaTimer = 0f;

    void Update()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            LoadNextScene();
        }

        AnimateText(titleText1);
        AnimateText(titleText2);
    }

    void AnimateText(TextMeshProUGUI text)
    {
        scaleTimer += Time.deltaTime * scaleSpeed;
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(scaleTimer, 1f));
        text.transform.localScale = new Vector3(scale, scale, 1f);

        colorTimer += Time.deltaTime * colorSpeed;
        Color newColor = Color.Lerp(startColor, endColor, Mathf.PingPong(colorTimer, 1f));

        alphaTimer += Time.deltaTime * colorSpeed;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(alphaTimer, 1f));
        newColor.a = alpha;

        text.color = newColor;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("InGameScene");
    }
}