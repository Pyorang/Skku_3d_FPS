using System;
using System.Collections;
using UnityEngine;
public class PlayerGunFire : MonoBehaviour
{
    [Header("총알 설정")]
    [Space]
    public ConsumableItem ReloadedAmmo;
    public ConsumableItem TotalAmmo;

    [Header("총 재장전 설정")]
    [Space]
    [SerializeField] private float _reloadingTime = 2f;
    private bool _isReloading = false;

    [Header("총 설정")]
    [Space]
    [SerializeField] private float _shootCoolTime = 0.1f;
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private ParticleSystem _hitEffect;
    private float _timeElapsed;

    public static event Action<float, float> OnReloading;

    private Camera _mainCamera;

    private void Awake()
    {
        _timeElapsed = _shootCoolTime;
        ReloadedAmmo.Initialize();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        if(!_isReloading)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }

            if (Input.GetMouseButton(0))
            {
                TryFire();
            }

            _timeElapsed += Time.deltaTime;
        }
    }
    private void TryFire()
    {
        if((_timeElapsed >= _shootCoolTime) && ReloadedAmmo.TryConsume(1))
        {
            _timeElapsed = 0f;
            Ray ray = new Ray(_fireTransform.position, _mainCamera.transform.forward);
            RaycastHit hitInfo = new RaycastHit(); // 충돌한 대상의 정보를 저장
            Fire(ray, hitInfo);
        }
    }

    private void Reload()
    {
        _isReloading = true;

        StartCoroutine(ReloadingProcess());
    }

    private IEnumerator ReloadingProcess()
    {
        float timeElapsed = 0;
        
        while(timeElapsed < _reloadingTime)
        {
            OnReloading?.Invoke(timeElapsed, _reloadingTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        int reloadingBulletCount = Mathf.Min(ReloadedAmmo.MaxValue - ReloadedAmmo.Value, TotalAmmo.Value);
        if (TotalAmmo.TryConsume(reloadingBulletCount))
        {
            ReloadedAmmo.Increase(reloadingBulletCount);
        }

        _isReloading = false;
    }

    private void Fire(Ray ray, RaycastHit hitInfo)
    {
        CameraRotate.Instance.GiveRebound(new Vector2(0, 2));

        bool isHit = Physics.Raycast(ray, out hitInfo);
        if (isHit == true)
        {
            // 1. Instantiate 방식 (+ 풀링) -> 새로 생성
            // 2. 하나를 캐싱해두고 Play    -> 단점 : 재실행이므로 기존 것이 삭제
            // 3. 하나를 캐싱해두고 Emit

            Debug.Log($"Hit : {hitInfo.transform.name}");
            _hitEffect.transform.position = hitInfo.point;
            _hitEffect.transform.forward = hitInfo.normal;

            /*ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            emitParams.position = hitInfo.point;
            emitParams.rotation3D = Quaternion.LookRotation(hitInfo.normal).eulerAngles;*/

            _hitEffect.Play();
        }
    }
    // Ray: 레이저 (시작 위치, 방향, 거리)
    // hitInfo: 충돌한 대상의 정보 저장
    // RaycastHit: 충돌한 대상의 정보 저장
}