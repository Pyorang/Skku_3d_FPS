using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Gun
{
    [Header("총 데이터 설정")]
    [Space]
    [SerializeField] private float _reloadTime;
    [SerializeField] private float _shootCoolTime;

    public float ReloadTime => _reloadTime;
    public float ShootCoolTime=> _shootCoolTime;

    [SerializeField] private Magazine _magazine;

    public int CurrentBulletCount => _magazine.CurrentBulletCount;
    public int MaxBulletCount => _magazine.MaxBulletCount;

    public void Init()
    {
        _magazine.Init();
    }

    public  bool Shoot()
    {
        if(_magazine.TryConsume(1))
        {
            return true;
        }

        return false;
    }

    public void Reload(int amount)
    {
        _magazine.Reload(amount);
    }
}
