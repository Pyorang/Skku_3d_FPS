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

    private void Update()
    {
        _hpSlider.value = _playerStat.Health.Value / _playerStat.Health.MaxValue;
        _spSlider.value = _playerStat.Stamina.Value / _playerStat.Stamina.MaxValue;
    }

    public void UpdateHpStatSlider(float value)
    {
        _hpSlider.value = value;
    }

    public void UpdateSpStatSlider(float value)
    {
        _spSlider.value = value;
    }
}
