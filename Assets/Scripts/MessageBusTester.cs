using UnityEngine;

public class MessageBusTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("** start to send message");
        GameController.MessageBus.Publish(new Messages.TestMessage1("cool message"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
           
            
        }
    }
}
