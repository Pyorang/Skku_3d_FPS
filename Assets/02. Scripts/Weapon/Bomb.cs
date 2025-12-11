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

        Monster monster = collision.gameObject.GetComponent<Monster>();
        if(monster != null)
        {
            monster.TryTakeDamage(_damage);
        }

        DestroyBomb();
    }

    public void DestroyBomb()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _managedPool.Release(this);
    }
}
