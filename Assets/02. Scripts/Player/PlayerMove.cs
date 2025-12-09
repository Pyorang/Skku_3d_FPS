using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStat))]
public class PlayerMove : MonoBehaviour
{
    [Header("스태미나 소모량")]
    [Space]
    [SerializeField] private int _runStaminaUse = 4;
    [SerializeField] private int _secondJumpStaminaUse = 10;

    private bool _running = false;
    private bool _secondJump = false;
    public bool Running => _running;

    public float Gravity = -9.81f;
    private float _yVeloctiy = 0f;

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

        direction = Camera.main.transform.TransformDirection(direction);

        if (Input.GetKey(KeyCode.LeftShift) && (_playerStat.Stamina.TryConsume(_runStaminaUse * Time.deltaTime)))
        {
            direction *= _playerStat.RunSpeed.Value;
        }

        direction.y = _yVeloctiy;

        _controller.Move(direction * _playerStat.MoveSpeed.Value * Time.deltaTime);
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
                if ((_playerStat.Stamina.TryConsume(_secondJumpStaminaUse)) && !_secondJump)
                {
                    _secondJump = true;

                }
                else
                {
                    return;
                }
            }

            _yVeloctiy = _playerStat.JumpPower.Value;
        }
    }
}
