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
        _gunFire.ReloadedAmmo.OnValueChanged += UpdateGunUI;
        _gunFire.TotalAmmo.OnValueChanged += UpdateGunUI;
        PlayerGunFire.OnReloading += UpdateReloadingUI;
    }

    private void Start()
    {
        UpdateGunUI();
    }

    private void OnDestroy()
    {
        _gunFire.ReloadedAmmo.OnValueChanged -= UpdateGunUI;
        _gunFire.TotalAmmo.OnValueChanged -= UpdateGunUI;
        PlayerGunFire.OnReloading -= UpdateReloadingUI;
    }

    private void UpdateGunUI()
    {
        _gunUIText.text = $"{_gunFire.ReloadedAmmo.Value} / {_gunFire.TotalAmmo.Value}";
    }

    private void UpdateReloadingUI(float value, float maxValue)
    {
        _reloadSlider.value = Mathf.Min((value / maxValue), 1f);
    }
}
