using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        slider.value = AudioManager.Instance.GetMusicVolume();

        slider.onValueChanged.AddListener(UpdateVolume);
    }

    private void UpdateVolume(float volume)
    {
        AudioManager.Instance.SetMusicVolume(volume);
    }
}