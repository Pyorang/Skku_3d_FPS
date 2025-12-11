using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ChainIKController : MonoBehaviour
{
    [Header("지속시간")]
    [Space]
    [SerializeField] private float _duration = 2f;

    [Header("컴포넌트")]
    [Space]
    [SerializeField] private ChainIKConstraint _ckc;

    private IEnumerator StartMove()
    {
        float timeElapsed = 0;
        float halfDuration = _duration / 2;

        while (timeElapsed < _duration)
        {
            float pingPongValue = Mathf.PingPong(timeElapsed, halfDuration);
            _ckc.weight = Mathf.Lerp(0, 1, pingPongValue / halfDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartMove());
        }
    }
}
