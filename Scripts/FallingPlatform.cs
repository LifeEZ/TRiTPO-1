using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private int time = 2;

    private Animator _animator;
    private static readonly int Falling = Animator.StringToHash("fall");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Invoke(nameof(Fall),time);
        Destroy(gameObject,4f);
    }

    private void Fall()
    {
        _animator.SetTrigger(Falling);
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
