using System.Collections;
using UnityEngine;

public class Drum : MonoBehaviour
{
    [Header("체력 설정")]
    [Space]
    [SerializeField] private ConsumableStat _health;

    [Header("데미지 설정")]
    [Space]
    [SerializeField] private ValueStat _damage;

    [Header("폭탄 범위 설정")]
    [Space]
    [SerializeField] private float _distance;

    [Header("대상 설정")]
    [Space]
    [SerializeField] private LayerMask _targetLayerMask;

    [Header("폭발 힘 설정")]
    [Space]
    [SerializeField] private float _explosionPower;

    [Header("폭탄 효과 설정")]
    [Space]
    [SerializeField] private ParticleSystem _explosionParticle;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        _health.Initialize();
    }

    public bool TryTakeDamage(float damage)
    {
        if (_health.Value <= 0) return false;

        _health.Decrease(damage);

        if (_health.Value <= 0)
        {
            StartCoroutine(Explode_Coroutine());
        }

        return true;
    }

    private IEnumerator Explode_Coroutine()
    {
        _explosionParticle.transform.position = transform.position;
        _explosionParticle.Play();

        _rigidBody.AddForce(Vector3.up * _explosionPower);
        _rigidBody.AddTorque(Random.insideUnitSphere * 90f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _distance, _targetLayerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent<PlayerStat>(out PlayerStat player))
            {
                player.Health.Decrease(_damage.Value);
            }

            if (colliders[i].TryGetComponent<Monster>(out Monster monster))
            {
                monster.TryTakeDamage(_damage.Value);
            }

            if (colliders[i].TryGetComponent<Drum>(out Drum drum))
            {
                drum.TryTakeDamage(_damage.Value);
            }

        }

        yield return new WaitForSeconds(3f);

        Destroy(this.gameObject);
    }
}
