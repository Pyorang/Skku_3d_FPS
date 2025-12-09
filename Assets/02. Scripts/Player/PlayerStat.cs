using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMove))]
public class PlayerStat : MonoBehaviour
{
    public ConsumableStat Health;
    public ConsumableStat Stamina;
    public ValueStat Damage;
    public ValueStat MoveSpeed;
    public ValueStat RunSpeed;
    public ValueStat JumpPower;

    private PlayerMove _playerMove;

    private void Awake()
    {
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Start()
    {
        Health.Initialize();
        Stamina.Initialize();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        Health.Regenerate(deltaTime);
        if (!_playerMove.Running)
        {
            Stamina.Regenerate(deltaTime);
        }
    }
}
