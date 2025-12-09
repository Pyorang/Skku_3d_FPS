using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject _explosionEffectPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effectObject = Instantiate(_explosionEffectPrefab);
        effectObject.transform.position = transform.position;

        Destroy(gameObject);
    }
}
