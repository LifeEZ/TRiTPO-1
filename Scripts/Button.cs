using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private TextMeshProUGUI _button;

    private void Start()
    {
        _button = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level " + _button.text);
    }
}
