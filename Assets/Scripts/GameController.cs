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
        ResourceManager.Instance.Initialise();
        UpgradeManager.Instance.InitializeUpdateManager();
        WorkerManager.Instance.Initialize();
    }

    public void OpenUI(int room)
    {
        if(room == 0)
        {
            //faith room
        }
        else
        {
            if(!DefaultRoomUi.IsActive)
            {
                DefaultRoomUi.OpenRoom(room);
            }
        }
    }
}
