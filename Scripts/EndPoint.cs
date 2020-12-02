using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level " +
                                   (int.Parse(SceneManager.GetActiveScene()
                                       .name[SceneManager.GetActiveScene().name.Length - 1].ToString()) + 1));
        }
    }
}
