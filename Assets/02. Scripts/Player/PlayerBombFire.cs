using UnityEngine;

public class PlayerBombFire : MonoBehaviour
{
    [Header("폭탄 설정")]
    [Space]
    public ConsumableItem Bomb;

    [Header("던지기 위한 속성")]
    [Space]
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private Transform _fireTransform;
    [SerializeField] private float _throwPower = 15f;

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
                Bomb bomb = Instantiate(_bombPrefab, _fireTransform.position, Quaternion.identity);
                Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();


                rigidbody.AddForce(Camera.main.transform.forward * _throwPower, ForceMode.Impulse);
            }
        }
    }
}
