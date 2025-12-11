using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float RotationSpeed = 200f;

    private float _accumulationX = 0;

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        _accumulationX += mouseX * RotationSpeed * Time.deltaTime;

        // 4. 누적한 회전 방향으로 카메라 회전하기
        transform.eulerAngles = new Vector3(0, _accumulationX);
    }
}
