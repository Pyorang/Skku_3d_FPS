using UnityEngine;
using UnityEngine.Pool;

public class Bomb : MonoBehaviour
{
    [Header("폭탄 효과 설정")]
    [Space]
    public GameObject ExplosionEffectPrefab;

    [Header("폭탄 데미지 설정")]
    [Space]
    [SerializeField] private float _damage = 20;

    [Header("폭탄 범위 설정")]
    [Space]
    [SerializeField] private float _explosionRadius = 2;

    [Header("대상 레이어 설정")]
    [Space]
    [SerializeField] private LayerMask _monsterLayer;

    private Rigidbody _rigidbody;

    private IObjectPool<Bomb> _managedPool;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetManagedPool(IObjectPool<Bomb> managedPool)
    {
        _managedPool = managedPool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effectObject = Instantiate(ExplosionEffectPrefab);
        effectObject.transform.position = transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius, _monsterLayer);

        for(int i = 0; i < colliders.Length; i++)
        {
            Monster monster = colliders[i].gameObject.AddComponent<Monster>();

            float distance = Vector3.Distance(transform.position, monster.transform.position);
            distance = Mathf.Min(1f, distance);

            float fianlDamage = _damage / distance;

            monster.TryTakeDamage(fianlDamage);
        }

        /*Monster monster = collision.gameObject.GetComponent<Monster>();
        if(monster != null)
        {
            monster.TryTakeDamage(_damage);
        }*/

        DestroyBomb();
    }

    public void DestroyBomb()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _managedPool.Release(this);
    }
}
