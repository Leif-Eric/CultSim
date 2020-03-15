using UnityEngine;
using GameEventBus;

public class GameController : MonoBehaviour
{
    //initialize global instance of messagebus
    public static EventBus MessageBus = new EventBus();

    public static GameController Instance;

    private object token1;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
        MessageBus.Subscribe<Messages.TestMessage1>(OnTestMessageReceived);
    }

    private void Start()
    {
      
    }

    public void OnTestMessageReceived(Messages.TestMessage1 msg)
    {
        Debug.Log("*** cool, message received: " + msg.Message);

        //unsubscribe
        MessageBus.Unsubscribe<Messages.TestMessage1>(OnTestMessageReceived);
    }
}
