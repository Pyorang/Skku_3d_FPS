using System;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class Magazine
{
    private int _currentBulletCount;
    [SerializeField] private int _maxBulletCount;

    public int CurrentBulletCount
    {
        get { return  _currentBulletCount; } 
        private set
        {
            if (value < 0) return;
            _currentBulletCount = value;
        }
    }

    public int MaxBulletCount => _maxBulletCount;

    public void Init()
    {
        _currentBulletCount = _maxBulletCount;
    }

    public bool TryConsume(int amount)
    {
        if (CurrentBulletCount < amount) return false;
        CurrentBulletCount -= amount;
        return true;
    }

    public int Reload(int amount)
    {
        int leftBullet = amount - (_maxBulletCount - _currentBulletCount);
        _currentBulletCount = Mathf.Min( _currentBulletCount + amount, _maxBulletCount);

        return Mathf.Max(0, leftBullet);
    }

    public void SetMaxBulletCount(int amount)
    {
        _maxBulletCount = amount;
    }
}
