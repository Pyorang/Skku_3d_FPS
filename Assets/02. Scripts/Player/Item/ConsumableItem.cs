using System;
using UnityEngine;

[Serializable]
public class ConsumableItem
{
    [SerializeField] private int _maxValue;
    [SerializeField] private int _value;

    public int Value
    {
        get { return _value; }
        private set
        {
            if (Mathf.Approximately(_value, value)) return;
            _value = value;
            OnValueChanged?.Invoke();
        }
    }
    public int MaxValue => _maxValue;

    public event Action OnValueChanged;

    public void Initialize()
    {
        SetValue(_maxValue);
    }

    public bool TryConsume(int amount)
    {
        if (Value < amount) return false;

        Consume(amount);

        return true;
    }

    public void Consume(int amount)
    {
        Value -= amount;
    }

    public void IncreaseMax(int amount)
    {
        _maxValue += amount;
    }

    public void Increase(int amount)
    {
        SetValue(Value + amount);
    }

    public void DecreaseMax(int amount)
    {
        _maxValue -= amount;
    }
    public void Decrease(int amount)
    {
        Value -= amount;
    }

    public void SetMaxValue(int value)
    {
        _maxValue = value;
    }
    public void SetValue(int value)
    {
        Value = value;

        if (Value > _maxValue)
        {
            Value = _maxValue;
        }
    }
}
