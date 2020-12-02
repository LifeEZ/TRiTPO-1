using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused;
    [SerializeField] private GameObject pauseMenuUI;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (IsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Quit()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }

    public void MaskDudeClicked()
    {
        SpawnPoint.Character = "MaskDude";
        Respawn();
    }

    public void NinjaFrogClicked()
    {
        SpawnPoint.Character = "NinjaFrog";
        Respawn();
    }

    public void PinkManClicked()
    {
        SpawnPoint.Character = "PinkMan";
        Respawn();
    }

    public void VirtualGuyClicked()
    {
        SpawnPoint.Character = "VirtualGuy";
        Respawn();
    }

    private void Respawn()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
