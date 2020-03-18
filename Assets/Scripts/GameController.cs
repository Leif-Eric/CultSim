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
    public RoomUiView DefaultRoomUi;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    private void Start()
    {
      
    }
}
