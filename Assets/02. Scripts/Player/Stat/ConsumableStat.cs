using System;
using UnityEngine;

[Serializable]
public class ConsumableStat
{
    [SerializeField] private float _maxValue;
    [SerializeField] private float _value;
    [SerializeField] private float _regenValue;

    public float Value
    {
        get { return _value; }
        private set
        {
            if (Mathf.Approximately(_value, value)) return;
            _value = value;
            OnValueChanged?.Invoke(_value, _maxValue);
        }
    }

    public float MaxValue => _maxValue;

    public event Action<float, float> OnValueChanged;

    public void Initialize()
    {
        SetValue(MaxValue);
    }

    public void Regenerate(float time)
    {
        Value = Mathf.Clamp(_value + _regenValue * time, 0f, _maxValue);
    }

    public bool TryConsume(float amount)
    {
        if (Value < amount) return false;

        Consume(amount);

        return true;
    }

    public void Consume(float amount)
    {
        Value -= amount;
    }

    public void IncreaseMax(float amount)
    {
        _maxValue += amount;
    }

    public void Increase(float amount)
    {
        SetValue(Value + amount);
    }

    public void DecreaseMax(float amount)
    {
        _maxValue -= amount;
    }
    public void Decrease(float amount)
    {
        Value -= amount;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
    }
    public void SetValue(float value)
    {
        Value = Mathf.Clamp(value, 0f, _maxValue);
    }
}
