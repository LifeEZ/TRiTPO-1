using UnityEngine;
using UnityEngine.Serialization;

public class Fan : MonoBehaviour
{
    [SerializeField] private int power = 10;

    [SerializeField] private LayerMask playerLayerMask;
    private BoxCollider2D[] _boxColliders;

    private void Start()
    {
        _boxColliders = GetComponents<BoxCollider2D>();
    }

    private void Update()
    {
        if (_boxColliders[1].IsTouchingLayers(playerLayerMask))
        {
            GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity = 
                new Vector2(GameObject.Find("Player").gameObject.GetComponent<Rigidbody2D>().velocity.x, power);
        }
    }
}
