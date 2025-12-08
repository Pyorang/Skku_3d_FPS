using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [Header("UI 슬라이더")]
    [Space]
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _spSlider;

    private void Start()
    {
        PlayerStat.OnHpValueChanged += UpdateHpStatSlider;
        PlayerStat.OnSpValueChanged += UpdateSpStatSlider;
    }

    private void OnDisable()
    {
        PlayerStat.OnHpValueChanged -= UpdateHpStatSlider;
        PlayerStat.OnSpValueChanged -= UpdateSpStatSlider;
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
