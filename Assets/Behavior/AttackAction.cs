using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Agent] attacks [Target]", category: "Action", id: "0b78d866ed35a81c05a3aa42abf20f81")]
public partial class AttackAction : Action
{
    private float _attackTimer = 0f;
    private float _attackTime = 2f;

    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    private Monster _monster;
    private PlayerStat _playerStat;

    protected override Status OnStart()
    {
        _monster = Agent.Value.GetComponent<Monster>();
        _playerStat = Target.Value.GetComponent<PlayerStat>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer > _attackTime)
        {
            if(_monster != null && _playerStat != null)
            {
                _attackTimer = 0f;
                _playerStat.Health.TryConsume(_monster.AttackPower);
            }
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

