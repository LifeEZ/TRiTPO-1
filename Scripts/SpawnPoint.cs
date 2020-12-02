using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static string Character = "MaskDude";
    [SerializeField] private GameObject player;
    
    private void Start()
    {
        player = (GameObject) Resources.Load(Character, typeof(GameObject));
        Instantiate(player, transform.position, transform.rotation);
    }
}
