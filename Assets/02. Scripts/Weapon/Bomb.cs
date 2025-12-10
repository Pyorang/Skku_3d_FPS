using UnityEngine;
using UnityEngine.Pool;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionEffectPrefab;

    private IObjectPool<Bomb> _managedPool;

    public void SetManagedPool(IObjectPool<Bomb> managedPool)
    {
        _managedPool = managedPool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effectObject = Instantiate(ExplosionEffectPrefab);
        effectObject.transform.position = transform.position;

        DestroyBomb();
    }

    public void DestroyBomb()
    {
        _managedPool.Release(this);
    }
}
