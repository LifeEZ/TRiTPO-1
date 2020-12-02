using UnityEngine;

public class RockHead : MonoBehaviour
{
    [SerializeField] private int damage = 15;
    [SerializeField] private int speed = 5;
    
    private enum MoveDirection
    {
        Right,Top,Left,Bottom
    }
    
    private enum AnimatorState
    {
        Idle,Left,Right,Bottom,Top
    }
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private AnimatorState _animatorState;
    private MoveDirection _moveDirection;
    private Collider2D _rightSideCollider;
    private Collider2D _topSideCollider;
    private Collider2D _leftSideCollider;
    private Collider2D _bottomSideCollider;

    [SerializeField] private Transform leftSide;
    [SerializeField] private Transform rightSide;
    [SerializeField] private Transform topSide;
    [SerializeField] private Transform bottomSide;
    [SerializeField] private LayerMask foreground;
    [SerializeField] private LayerMask wall;
    [SerializeField] private LayerMask highground;
    private static readonly int State = Animator.StringToHash("state");

    private void Start()
    {
        
        _bottomSideCollider = bottomSide.gameObject.GetComponent<Collider2D>();
        _leftSideCollider = leftSide.gameObject.GetComponent<Collider2D>();
        _topSideCollider = topSide.gameObject.GetComponent<Collider2D>();
        _rightSideCollider = rightSide.gameObject.GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetDirection();
        Move();
        _animator.SetInteger(State,(int)_animatorState);
    }

    private void Move()
    {
        switch (_moveDirection)
        {
            case MoveDirection.Right:
                _rigidbody.velocity = new Vector2(speed,0f);
                break;
            case MoveDirection.Top:
                _rigidbody.velocity = new Vector2(0,-speed);
                break;
            case MoveDirection.Left:
                _rigidbody.velocity = new Vector2(-speed,0);
                break;
            case MoveDirection.Bottom:
                _rigidbody.velocity = new Vector2(0,speed);
                break;
        }
    }
    
    private void GetDirection()
    {
        if (_rightSideCollider.IsTouchingLayers(wall))
        {
            _moveDirection = MoveDirection.Bottom;
            _animatorState = AnimatorState.Right;
        }
        if (_topSideCollider.IsTouchingLayers(highground))
        {
            _moveDirection = MoveDirection.Left;
            _animatorState = AnimatorState.Top;
        }
        if (_leftSideCollider.IsTouchingLayers(wall))
        {
            _moveDirection = MoveDirection.Top;
            _animatorState = AnimatorState.Left;
        }

        if (!_bottomSideCollider.IsTouchingLayers(foreground) || !_leftSideCollider.IsTouchingLayers(wall)) return;
        _moveDirection = MoveDirection.Right;
        _animatorState = AnimatorState.Bottom;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
