using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMove))]
public class PlayerStat : MonoBehaviour
{
    [Header("플레이어 스텟")]
    [Space]
    [SerializeField] private int _maxHealthPoint = 100;
    [SerializeField] private int _maxStaminaPoint = 100;
    [SerializeField] private int _regenerateStaminaPoint = 1;

    private int _healthPoint;
    private int _staminaPoint;
    private float _timeElapsed;
    private readonly float _staminaGainCycle = 0.1f;

    public int HealthPoint
    {
        get { return _healthPoint; }
        set
        {
            if (value < 0) return;
            _healthPoint = Mathf.Min(value, _maxHealthPoint);
            OnHpValueChanged?.Invoke((float)_healthPoint / _maxHealthPoint);
        }
    }

    public int StaminaPoint
    {
        get { return _staminaPoint; }
        set
        {
            if(value < 0) return;
            _staminaPoint = Mathf.Min(value, _maxStaminaPoint);
            OnSpValueChanged?.Invoke((float)_staminaPoint / _maxStaminaPoint);
        }
    }

    public static event Action<float> OnHpValueChanged;
    public static event Action<float> OnSpValueChanged;

    private PlayerMove _playerMove;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        _healthPoint = _maxHealthPoint;
        _staminaPoint = _maxStaminaPoint;
    }

    private void Update()
    {
        GainStamina();
    }

    private void GainStamina()
    {
        if(!_playerMove.Running)
        {
            if (_timeElapsed >= _staminaGainCycle)
            {
                _timeElapsed = 0f;
                StaminaPoint += _regenerateStaminaPoint;
            }

            _timeElapsed += Time.deltaTime;
        }
    }

    public void UseStamina(int amount)
    {
        StaminaPoint -= amount;
    }
}
