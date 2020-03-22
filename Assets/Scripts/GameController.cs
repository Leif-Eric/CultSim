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
    public FaithRoomUiView FaithRoomUi;
    private float timer;

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
        timer = 0;
    }

    public void OpenUI(int room)
    {
        if (!FaithRoomUi.IsActive && !DefaultRoomUi.IsActive)
        {
            if(room == 0)
            {
                FaithRoomUi.Open();
            }
            else 
            { 
                DefaultRoomUi.OpenRoom(room); 
            }            
        }
    }

    public void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > 1)
        {
            timer -= 1;
            ResourceManager.Instance.UpdateRessources();
        }
    }
}
