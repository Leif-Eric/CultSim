using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public static StartMenu Instance;
    public List<SubMenuView> SubViews = new List<SubMenuView>();

    public bool IsMenuActive { get; set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void OpenCredits()
    {
        AudioController.Instance.PlaySound(AudioController.Sounds.Button);
        SubViews[0].Open();
    }

    public void StartGame()
    {
        AudioController.Instance.PlaySound(AudioController.Sounds.Button);
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        AudioController.Instance.PlaySound(AudioController.Sounds.Button);
        Application.Quit();
    }
}
