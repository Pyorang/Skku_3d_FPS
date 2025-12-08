using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStat))]
public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 이동속도")]
    [Space]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _runSpeed;

    [Header("플레이어 점프력")]
    [Space]
    [SerializeField] private float _jumpPower;

    [Header("스태미나 소모량")]
    [Space]
    [SerializeField] private int _runStaminaUse = 4;
    [SerializeField] private int _secondJumpStaminaUse = 10;
    private readonly float _staminaUseCycle = 0.1f;
    private float _timeElapsed = 1f;

    private bool _running = false;
    private bool _secondJump = false;
    public bool Running => _running;

    public float Gravity = -9.81f;
    private float _yVeloctiy = 0f;  // 중력에 의해 누적될 y값

    private CharacterController _controller;
    private PlayerStat _playerStat;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerStat = GetComponent<PlayerStat>();
    }

    private void Update()
    {
        // 중력을 누적한다.
        _yVeloctiy += Gravity * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);
        direction.Normalize();

        ApplyJump();

        // - 카메라가 쳐다보는 방향으로 변환한다.
        direction = Camera.main.transform.TransformDirection(direction);

        if (Input.GetKey(KeyCode.LeftShift) && (_playerStat.StaminaPoint >= _runStaminaUse))
        {
            _running = true;
            direction *= _runSpeed;
            UseRunningStamina(_runStaminaUse);
        }
        else
        {
            _running = false;
            _timeElapsed = _staminaUseCycle;
        }

            direction.y = _yVeloctiy;

        //transform.position += direction * _moveSpeed * Time.deltaTime;
        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }

    private void UseRunningStamina(int amount)
    {
        if(_timeElapsed >= _staminaUseCycle)
        {
            _timeElapsed = 0f;
            _playerStat.UseStamina(amount);
        }

        _timeElapsed += Time.deltaTime;
    }

    private void ApplyJump()
    {
        if(_controller.isGrounded)
        {
            _secondJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!_controller.isGrounded)
            {
                if ((_playerStat.StaminaPoint >= _secondJumpStaminaUse) && !_secondJump)
                {
                    _secondJump = true;
                    _playerStat.UseStamina(_secondJumpStaminaUse);

                }
                else
                {
                    return;
                }
            }

            _yVeloctiy = _jumpPower;
        }
    }
}
