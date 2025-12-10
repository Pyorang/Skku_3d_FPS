using TMPro;
using UnityEngine;

public class UI_PlayerBomb : MonoBehaviour
{
    [Header("플레이어 발사 컴포넌트")]
    [Space]
    [SerializeField] private PlayerFire _playerFire;

    [Header("폭탄 텍스트")]
    [Space]
    [SerializeField] private TextMeshProUGUI _bombCount;
    private void Awake()
    {
        _playerFire.Bomb.OnValueChanged += UpdateBombUI;
    }

    private void Start()
    {
        UpdateBombUI();
    }

    private void OnDestroy()
    {
        _playerFire.Bomb.OnValueChanged -= UpdateBombUI;
    }

    private void UpdateBombUI()
    {
        _bombCount.text = $"{_playerFire.Bomb.Value} / {_playerFire.Bomb.MaxValue}";
    }
}
