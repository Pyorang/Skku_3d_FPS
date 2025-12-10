using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerGunFire : MonoBehaviour
{
    private bool _isReloading = false;

    [Header("총 설정")]
    [Space]
    [SerializeField] private Gun _gun;
    [SerializeField] private Transform _fireTransform;

    private Queue<Magazine> _magazines = new Queue<Magazine>();

    [Header("이펙트 설정")]
    [Space]
    [SerializeField] private ParticleSystem _hitEffect;
    private float _timeElapsed;

    public static event Action<float, float> OnReloadingTimeChanged;
    public static event Action<int, int> OnReloading;

    private Camera _mainCamera;

    private void Awake()
    {
        _timeElapsed = _gun.ShootCoolTime;

        _gun.Init();

        // NOTE : 테스트 코드
        for(int i = 0; i < 4; i++)
        {
            Magazine magazine = new Magazine();
            magazine.SetMaxBulletCount(_gun.CurrentBulletCount);
            magazine.Init();
            _magazines.Enqueue(magazine);
        }
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        OnReloading?.Invoke(_gun.CurrentBulletCount, GetTotalBullet());
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
        if((_timeElapsed >= _gun.ShootCoolTime) && _gun.Shoot())
        {
            OnReloading?.Invoke(_gun.CurrentBulletCount, GetTotalBullet());

            _timeElapsed = 0f;
            Ray ray = new Ray(_fireTransform.position, _mainCamera.transform.forward);
            RaycastHit hitInfo = new RaycastHit(); // 충돌한 대상의 정보를 저장
            Fire(ray, hitInfo);
        }
    }

    private void Reload()
    {
        StartCoroutine(ReloadingProcess());
    }

    private IEnumerator ReloadingProcess()
    {
        int reloadingCount = _gun.MaxBulletCount - _gun.CurrentBulletCount;

        if(reloadingCount <= 0)
        {
            yield break;
        }

        _isReloading = true;
        float timeElapsed = 0;
        
        while(timeElapsed < _gun.ReloadTime)
        {
            OnReloadingTimeChanged?.Invoke(timeElapsed, _gun.ReloadTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _gun.Reload(Mathf.Min(reloadingCount, GetTotalBullet()));

        while ((reloadingCount > 0) && (GetTotalBullet() > 0))
        {
            Magazine recentUsedMagazine = _magazines.Peek();

            int getBulletCount = Mathf.Min(reloadingCount, recentUsedMagazine.CurrentBulletCount);
            recentUsedMagazine.TryConsume(getBulletCount);

            reloadingCount -= getBulletCount;

            if(recentUsedMagazine.CurrentBulletCount == 0)
            {
                _magazines.Dequeue();
            }
        }

        OnReloading?.Invoke(_gun.CurrentBulletCount, GetTotalBullet());

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

    public int GetTotalBullet()
    {
        int totalBulelt = 0;

        foreach(var magaizne in _magazines)
        {
            totalBulelt += magaizne.CurrentBulletCount;
        }

        return totalBulelt;
    }
}