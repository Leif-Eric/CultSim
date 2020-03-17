using UnityEngine;

public class SubMenuView : MonoBehaviour
{
    public GameObject View;

    public void Close()
    {
        View.SetActive(false);

        if(StartMenu.Instance != null)
        {
            StartMenu.Instance.IsMenuActive = false;
        }

    }

    public virtual void Open()
    {
        View.SetActive(true);
        if(StartMenu.Instance != null)
        {
            StartMenu.Instance.IsMenuActive = true;
        }
    }
}
