using UnityEngine;

public class Collectable : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Collected = Animator.StringToHash("Collected");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        other.gameObject.GetComponent<PlayerController>().ScoreIncrement();
        _animator.SetTrigger(Collected);
        Destroy(gameObject,0.3f);
    }
}
