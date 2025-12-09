using TMPro;
using UnityEngine;

public class UI_PlayerBomb : MonoBehaviour
{
    [Header("폭탄 텍스트")]
    [Space]
    [SerializeField] private TextMeshProUGUI _bombCount;

    [Header("플레이어 발사 컴포넌트")]
    [Space]
    [SerializeField] private PlayerFire _playerFire;

    private void Update()
    {
        _bombCount.text = $"{_playerFire.Bomb.Value} / {_playerFire.Bomb.MaxValue}";
    }
}
