using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    /*public EMonsterState _state = EMonsterState.Idle;
    public EMonsterState State
    {
        get { return _state; }
        set
        {
            if (_state != value)
            {
                _state = value;

                if (value == EMonsterState.Idle)
                {
                    Idle();
                }
                if (value == EMonsterState.Attack)
                {
                    AttackTimer = 0;
                }
                if (value == EMonsterState.Patrol)
                {
                    PatrolTimer = PatrolThinkingTime;
                }
            }
        }
    }*/

    [SerializeField] private GameObject _player;
    [SerializeField] private CharacterController _characterController;

    private float _playerDistance;
    private float PlayerDistance
    {
        get { return _playerDistance; }
        set
        {
            _playerDistance = value;
        }
    }

    private Vector3 _startLocation;

    public float DetectDistance = 4f;
    public float AttackDistance = 1.2f;
    [SerializeField] private float _knockbackPower = 2f;

    public float MoveSpeed = 1f;
    public float AttackTimer = 0f;
    public float AttackSpeed = 2f;
    public float AttackPower = 10;

    public float PatrolTimer = 0f;
    public float PatrolThinkingTime = 2f;
    public float PatrolDistance = 2f;
    public Vector3 nextPosition;


    /*private void Awake()
    {
        _startLocation = transform.position;
        AttackTimer = AttackSpeed;
    }

    private void Update()
    {
        PlayerDistance = Vector3.Distance(transform.position, _player.transform.position);

        CheckPlayerDistanceForStateChange();

        switch (_state)
        {
            case EMonsterState.Patrol:
                Patrol();
                break;

            case EMonsterState.Trace:
                Trace();
                break;

            case EMonsterState.Comeback:
                Comeback();
                break;

            case EMonsterState.Attack:
                Attack();
                break;
        }
    }

    private void CheckPlayerDistanceForStateChange()
    {
        if (IsInvincible())
        {
            return;
        }

        if (_playerDistance <= AttackDistance)
        {
            State = EMonsterState.Attack;
        }
        else if (_playerDistance <= DetectDistance)
        {
            State = EMonsterState.Trace;
        }

        if (_playerDistance > DetectDistance && (State == EMonsterState.Trace ))
        {
            State = EMonsterState.Comeback;
        }
    }

    private void Idle()
    {
        StartCoroutine(Idle_Coroutine());
    }

    private void Patrol()
    {
        PatrolTimer += Time.deltaTime;

        if (PatrolTimer >= PatrolThinkingTime)
        {
            PatrolTimer = 0f;
            nextPosition = GetRandomPatrolPosition();
        }

        Vector3 direction = (nextPosition - transform.position).normalized;
        _characterController.Move(direction * MoveSpeed * Time.deltaTime); 
    }

    private Vector3 GetRandomPatrolPosition()
    {
        Vector3 randomDirection = new Vector3(Random.Range(0, 1f), 0, Random.Range(0, 1f)).normalized;
        return _startLocation + randomDirection * Random.Range(1, PatrolDistance);
    }

    private void Trace()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        _characterController.Move(direction * MoveSpeed * Time.deltaTime);
    }

    private void Comeback()
    {
        if(Vector3.Distance(_startLocation, transform.position) > 0.1f)
        {
            Vector3 direction = (_startLocation - transform.position).normalized;
            _characterController.Move(direction * MoveSpeed * Time.deltaTime);
        }
        else
        {
            _state = EMonsterState.Idle;
            Idle();
            return;
        }
    }

    private void Attack()
    {
        AttackTimer += Time.deltaTime;

        if(AttackTimer >= AttackSpeed)
        {
            AttackTimer = 0f;
            
            PlayerStat playerStat = _player.GetComponent<PlayerStat>();
            playerStat.Health.TryConsume(AttackPower);
        }
    }

    public float Health = 100;

    public bool TryTakeDamage(float damage)
    {
        if(IsInvincible())
        {
            return false;
        }

        Health -= damage;

        if(Health > 0)
        {
            _state = EMonsterState.Hit;
            StartCoroutine(Hit_Coroutine());
        }
        else
        {
            _state = EMonsterState.Death;
            StartCoroutine(Death_Coroutine());
        }

        return true;
    }

    public bool IsInvincible()
    {
        return _state == EMonsterState.Hit || _state == EMonsterState.Death;
    }

    public void ApplyKnockBack(Vector3 direction)
    {
        if(!IsInvincible())
        {
            _characterController.Move(direction.normalized * _knockbackPower);
        }
    }

    private IEnumerator Idle_Coroutine()
    {
        yield return new WaitForSeconds(2f);
        _state = EMonsterState.Patrol;
    }

    private IEnumerator Hit_Coroutine()
    {
        yield return new WaitForSeconds(0.15f);
        _state = EMonsterState.Idle;
        Idle();
    }

    private IEnumerator Death_Coroutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }*/
}
