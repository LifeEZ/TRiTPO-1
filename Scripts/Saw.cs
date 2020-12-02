using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    
    private Vector3 _pos1;
    private Vector3 _pos2;
    [SerializeField] private Transform leftBottom;
    [SerializeField] private Transform rightTop;
    
    private enum MoveDirection
    {
        Horizontal,
        Vertical
    }

    [SerializeField] private MoveDirection direction;

    private void Start()
    {
        if (direction == MoveDirection.Horizontal)
        {
            _pos1 = new Vector3(leftBottom.position.x,0,0);
            _pos2 = new Vector3(rightTop.position.x,0,0);
        }
        else
        {
            _pos1 = new Vector3(0,leftBottom.position.y,0);
            _pos2 = new Vector3(0,rightTop.position.y,0);
        }
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(_pos1, _pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(60);
        }
    }
}
