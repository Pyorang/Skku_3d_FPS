using UnityEngine;
using UnityEngine.Pool;

public class PlayerFire : MonoBehaviour
{
    [Header("폭탄 설정")]
    [Space]
    public ConsumableItem Bomb;

    [Header("던지기 위한 속성")]
    [Space]
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private float _throwPower = 15f;

    private IObjectPool<Bomb> _bombPool;

    private void Awake()
    {
        _bombPool = new ObjectPool<Bomb>(CreateBomb, OnGetBomb, OnReleaseBomb, OnDestroyBomb, maxSize:2);
    }

    private void Start()
    {
        Bomb.Initialize();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(Bomb.TryConsume(1))
            {
                Bomb bomb = _bombPool.Get();
                bomb.transform.position = _fireTransform.position;
                Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();


                rigidbody.AddForce(Camera.main.transform.forward * _throwPower, ForceMode.Impulse);
            }
        }
    }

    private Bomb CreateBomb()
    {
        Bomb bomb = Instantiate(_bombPrefab). GetComponent<Bomb>();
        bomb.SetManagedPool(_bombPool);
        return bomb;
    }

    private void OnGetBomb(Bomb bomb)
    {
        bomb.gameObject.SetActive(true);
    }

    private void OnReleaseBomb(Bomb bomb)
    {
        bomb.gameObject.SetActive(false);
    }

    private void OnDestroyBomb(Bomb bomb)
    {
        Destroy(bomb.gameObject);
    }
}
