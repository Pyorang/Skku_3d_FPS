using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStats : MonoBehaviour
{
    [Header("플레이어 스탯")]
    [Space]
    [SerializeField] private PlayerStat _playerStat;

    [Header("UI 슬라이더")]
    [Space]
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _spSlider;

    private void Awake()
    {
        _playerStat.Health.OnValueChanged += UpdateHpStatSlider;
        _playerStat.Stamina.OnValueChanged += UpdateSpStatSlider;
    }

    private void OnDestroy()
    {
        _playerStat.Health.OnValueChanged -= UpdateHpStatSlider;
        _playerStat.Stamina.OnValueChanged -= UpdateSpStatSlider;
    }

    public void UpdateHpStatSlider(float value, float maxValue)
    {
        _hpSlider.value = _playerStat.Health.Value / _playerStat.Health.MaxValue;
    }

    public void UpdateSpStatSlider(float value, float maxValue)
    {
        _spSlider.value = _playerStat.Stamina.Value / _playerStat.Stamina.MaxValue;
    }
}
