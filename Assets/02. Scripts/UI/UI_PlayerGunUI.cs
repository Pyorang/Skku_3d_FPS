using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerGunUI : MonoBehaviour
{
    [Header("플레이어 총 발사")]
    [Space]
    [SerializeField] private PlayerGunFire _gunFire;

    [Header("총알 텍스트")]
    [Space]
    [SerializeField] private TextMeshProUGUI _gunUIText;

    [Header("재장전 Slider")]
    [Space]
    [SerializeField] private Slider _reloadSlider;

    private void Awake()
    {
        PlayerGunFire.OnReloading += UpdateGunUI;
        PlayerGunFire.OnReloadingTimeChanged += UpdateReloadingUI;
    }
    private void OnDestroy()
    {
        PlayerGunFire.OnReloading -= UpdateGunUI;
        PlayerGunFire.OnReloadingTimeChanged -= UpdateReloadingUI;
    }

    private void UpdateGunUI(int value, int maxValue)
    {
        _gunUIText.text = $"{value} / {maxValue}";
    }

    private void UpdateReloadingUI(float value, float maxValue)
    {
        _reloadSlider.value = Mathf.Min((value / maxValue), 1f);
    }
}
