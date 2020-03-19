using UnityEngine;

public class SubMenuView : MonoBehaviour
{
    public GameObject View;

    public bool IsActive;

    public void Close()
    {
        IsActive = false;
        View.SetActive(false);

        if(StartMenu.Instance != null)
        {
            StartMenu.Instance.IsMenuActive = false;
        }

        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySound(AudioController.Sounds.Button);
        }
    }

    public virtual void Open()
    {
        IsActive = true;

        View.SetActive(true);
        if(StartMenu.Instance != null)
        {
            StartMenu.Instance.IsMenuActive = true;
        }
        if(AudioController.Instance != null)
        {
            AudioController.Instance.PlaySound(AudioController.Sounds.OpenUi);
        }
    }
}
