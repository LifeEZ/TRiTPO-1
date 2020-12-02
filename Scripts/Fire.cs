using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private int damage = 60;
    [SerializeField] private int time = 3;

    private Animator _animator;
    
    private enum AnimatorState
    {
        Off,On
    }

    private AnimatorState _animatorState;
    private static readonly int State = Animator.StringToHash("state");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(FireController());
    }

    private IEnumerator FireController()
    {
        while (true)
        {
            _animatorState = AnimatorState.Off;
            _animator.SetInteger(State, (int) _animatorState);
            yield return new WaitForSecondsRealtime(time);
            _animatorState = AnimatorState.On;
            _animator.SetInteger(State, (int) _animatorState);
            yield return new WaitForSecondsRealtime(time);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_animatorState == AnimatorState.On && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
