using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    private CharacterController _controller;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    public float Gravity = -9.81f;
    private float _yVeloctiy = 0f;  // 중력에 의해 누적될 y값

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 중력을 누적한다.
        _yVeloctiy += Gravity * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, y);
        direction.Normalize();

        if(Input.GetButtonDown("Jump") && _controller.isGrounded)
        {
            _yVeloctiy = _jumpPower;
        }

        // - 카메라가 쳐다보는 방향으로 변환한다.
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = _yVeloctiy;

        //transform.position += direction * _moveSpeed * Time.deltaTime;
        _controller.Move(direction * _moveSpeed * Time.deltaTime);
    }
}
