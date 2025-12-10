using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController s_CameraController;
    public static CameraController Instance => s_CameraController;

    [Header("카메라 뷰")]
    [Space]
    [SerializeField] private Transform _fpsView;
    [SerializeField] private Transform _tpsView;
    private Transform _target;

    [Header("카메라 이동 설정")]
    [Space]
    [SerializeField] private float _duration = 1.0f;

    private Vector3 _startPos;

    private bool _isFPS = false;
    public bool IsFPS
    {
        get {  return _isFPS; }
        private set
        {
            _isFPS = value;
            _target = _isFPS ? _fpsView : _tpsView;
        }
    }

    private bool _isMoving = false;

    private void Awake()
    {
        IsFPS = true;

        if(s_CameraController == null)
        {
            s_CameraController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        SelectCameraView();
    }

    private void LateUpdate()
    {
        if(!_isMoving)
        {
            transform.position = _target.position;
        }
    }

    private void SelectCameraView()
    {
        if (Input.GetKeyDown(KeyCode.T) && !_isMoving)
        {
            _startPos = _isFPS ? _fpsView.position : _tpsView.position;
            
            IsFPS = !IsFPS;

            _isMoving = true;

            DOVirtual.Float(0f, 1f, _duration, UpdateTweenByParam)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                _isMoving = false;
            });
        }
    }

    private void UpdateTweenByParam(float t)
    {
        transform.position = Vector3.Lerp(_startPos, _target.position, t);
    }
}
