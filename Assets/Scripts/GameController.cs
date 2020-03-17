using UnityEngine;
using GameEventBus;

public class GameController : MonoBehaviour
{
    #region static stuff
    public static EventBus MessageBus = new EventBus();

    public static GameController Instance;
    #endregion

    public WayPointHandler WayPointHandler;

    public RessourceInfoComponent RessourceInfo;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    //    MessageBus.Subscribe<Messages.TestMessage1>(OnTestMessageReceived);
    }

    private void Start()
    {
      
    }

    //public void OnTestMessageReceived(Messages.TestMessage1 msg)
    //{
    //    Debug.Log("*** cool, message received: " + msg.Message);

    //    //unsubscribe
    //    MessageBus.Unsubscribe<Messages.TestMessage1>(OnTestMessageReceived);
    //}
}
