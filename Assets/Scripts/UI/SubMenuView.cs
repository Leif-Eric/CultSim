using UnityEngine;

public class SubMenuView : MonoBehaviour
{
    public GameObject View;

    public void Close()
    {
        View.SetActive(false);
        StartMenu.Instance.IsMenuActive = false;
    }

    public void Open()
    {
        View.SetActive(true);
        StartMenu.Instance.IsMenuActive = true;
    }
}
