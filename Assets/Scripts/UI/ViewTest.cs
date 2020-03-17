using UnityEngine;

public class ViewTest : MonoBehaviour
{
    public RoomUiView RoomUi;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.X))
        {
            RoomUi.Open();
        }
    }
}
