using System;
using UnityEngine;

[Serializable]
public class ConsumableItem
{
    [SerializeField] private int _maxValue;
    private int _value;

    public float Value => _value;
    public float MaxValue => _maxValue;

    public void Initialize()
    {
        SetValue(_maxValue);
    }

    public bool TryConsume(int amount)
    {
        if (_value < amount) return false;

        Consume(amount);

        return true;
    }

    public void Consume(int amount)
    {
        _value -= amount;
    }

    public void IncreaseMax(int amount)
    {
        _maxValue += amount;
    }

    public void Increase(int amount)
    {
        SetValue(_value + amount);
    }

    public void DecreaseMax(int amount)
    {
        _maxValue -= amount;
    }
    public void Decrease(int amount)
    {
        _value -= amount;
    }

    public void SetMaxValue(int value)
    {
        _maxValue = value;
    }
    public void SetValue(int value)
    {
        _value = value;

        if (_value > _maxValue)
        {
            _value = _maxValue;
        }
    }
}
