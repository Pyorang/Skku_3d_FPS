using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float RotationSpeed = 200f;

    // 유니티는 0~360도 체계이므로 우리가 따로 저장할 -360 ~ 360 체계로 누적할 변수
    private float _accumulationX = 0;
    private float _accumulationY = 0;

    private void Update()
    {
        if(!Input.GetMouseButton(1))
        {
            return;
        }

        // 1. 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Debug.Log($"{mouseX} : {mouseY}");

        // 2. 마우스 입력을 누적한 방향을 구한다.
        _accumulationX += mouseX * RotationSpeed * Time.deltaTime;
        _accumulationY += mouseY * RotationSpeed * Time.deltaTime;

        // 3. 사람처럼 -90 ~ 90도 사이로 제한한다.
        _accumulationY = Mathf.Clamp(_accumulationY, -90f, 90f);

        // 4. 누적한 회전 방향으로 카메라 회전하기
        transform.eulerAngles = new Vector3(-_accumulationY, _accumulationX);

    }
}
