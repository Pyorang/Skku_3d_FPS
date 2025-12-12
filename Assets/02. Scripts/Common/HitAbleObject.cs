using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public abstract class HitAbleObject
{
    [Header("체력 설정")]
    [Space]
    [SerializeField] protected float _health;
    [SerializeField] protected float _maxHealth;
    [SerializeField] private float _regenValue;

    public float Health
    {
        get { return _health; }
        private set
        {
            value = Math.Clamp(value, 0, MaxHealth);
            _health = value;
            OnValueChanged?.Invoke(_health, _maxHealth);
        }
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            value = Math.Max(0, value);
            _maxHealth = value;
            OnValueChanged?.Invoke(_health, _maxHealth);
        }
    }

    public event Action<float, float> OnValueChanged;

    public void InitHealth()
    {
        Health = MaxHealth;
    }

    public virtual bool TryTakeDamage(int amount)
    {
        if(Health <= 0)
        {
            return false;
        }

        Health -= amount;

        if(Health <= 0)
        {
            Death();
        }

        return true;
    }

    public virtual bool TryHeal(int amount)
    {
        if(amount < 0)
        {
            return false;
        }

        Health += amount;

        return true;
    }

    public void Regenerate(float time)
    {
        Health = Health + _regenValue * time;
    }

    public abstract void Death();
}
