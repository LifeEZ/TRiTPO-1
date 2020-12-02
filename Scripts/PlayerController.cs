using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int _score;
    private int _extraJumps = 1;
    [SerializeField] private int speed = 14;
    [SerializeField] private int jump = 14;
    [SerializeField] private int health = 60;

    private enum AnimatorState
    {
        Idle,Run,Jump,Fall,WallJump,DoubleJump
    }

    private AnimatorState _animatorState = AnimatorState.Idle;
    private static readonly int Hit = Animator.StringToHash("Hit");
    private static readonly int Death = Animator.StringToHash("Death");
    private HealthBar _healthBar;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Collider2D _collider;
    private TextMeshProUGUI _scoreText;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask wall;

    private void Start()
    {
        
        gameObject.name = "Player";
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        InputManager();
        AnimationManager();
        _animator.SetInteger("State",(int)_animatorState);
    }

    private void InputManager()
    {
        var hDirection = Input.GetAxis("Horizontal");
        if (hDirection < 0)
        {
            _rigidbody.velocity = new Vector2(-speed,_rigidbody.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        if (hDirection > 0)
        {
            _rigidbody.velocity = new Vector2(speed,_rigidbody.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (_collider.IsTouchingLayers(ground) || _extraJumps > 0)
            {
                if (_collider.IsTouchingLayers(ground))
                {
                    _animatorState = AnimatorState.Jump;
                }
                else if (!_collider.IsTouchingLayers(wall))
                {
                    _extraJumps--;
                    _animatorState = AnimatorState.DoubleJump;
                }

                if (!_collider.IsTouchingLayers(wall)) {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jump);
                }
            }
        }

        if (_collider.IsTouchingLayers(wall))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,_rigidbody.velocity.y/1.1f);
            _extraJumps = 1;
            _animatorState = AnimatorState.WallJump;
        }

        if (_collider.IsTouchingLayers(ground))
        {
            _extraJumps = 1;
        }
    }

    private void AnimationManager()
    {
        if (_animatorState == AnimatorState.Jump || _animatorState == AnimatorState.DoubleJump)
        {
            if (_rigidbody.velocity.y < .1f)
            {
                _animatorState = AnimatorState.Fall;
            }
        }
        else if (_animatorState == AnimatorState.Fall)
        {
            if (_collider.IsTouchingLayers(ground))
            {
                _animatorState = AnimatorState.Idle;
            }
        }
        else if (_animatorState == AnimatorState.WallJump)
        {
            if (!_collider.IsTouchingLayers(wall) && !_collider.IsTouchingLayers(ground))
            {
                _animatorState = AnimatorState.Fall;
            }
            else if (_collider.IsTouchingLayers(ground) && _collider.IsTouchingLayers(wall))
            {
                _animatorState = AnimatorState.Idle;
            }
        }
        else if (Mathf.Abs(_rigidbody.velocity.x) > 2f)
        {
            _animatorState = _rigidbody.IsTouchingLayers(ground) ? AnimatorState.Run : AnimatorState.Fall;
        }
        else
        {
            _animatorState = AnimatorState.Idle;
        }
    }

    public void TakeDamage(int damage)
    {
        _animator.SetTrigger(Hit);
        health -= damage;
        _healthBar.SetHealth(health);
        if (health > 0) return;
        _animator.SetTrigger(Death);
        Invoke(nameof(Respawn), 0.3f);
    }

    private void Respawn()
    {
        
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ScoreIncrement()
    {
        _score++;
        _scoreText.SetText("X " + _score);
    }
}
