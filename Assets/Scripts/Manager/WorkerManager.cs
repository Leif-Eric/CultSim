using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorkerManager : MonoBehaviour
{
    private static WorkerManager _instance;
    [HideInInspector]
    public static WorkerManager Instance { get { return _instance; } }

    [HideInInspector]
    public int faithWorker;

    [HideInInspector]
    public int watchscoreWorkerNormal;
    [HideInInspector]
    public int watchscoreWorkerMiddle;
    [HideInInspector]
    public int watchscoreWorkerHigh;

    [HideInInspector]
    public int moneyWorkerNormal;
    [HideInInspector]
    public int moneyWorkerMiddle;
    [HideInInspector]
    public int moneyWorkerHigh;

    [HideInInspector]
    public int workerWorkerNormal;
    [HideInInspector]
    public int workerWorkerMiddle;
    [HideInInspector]
    public int workerWorkerHigh;

    [HideInInspector]
    public int workersSacrifycedSinceLastUpdate;
    [HideInInspector]
    public int freeWorkers;

    public int startWorker;

    //***** worker pooling stuff
    public const int StartPool = 10;

    public GameObject WorkerPrefab;
    public Transform WorkerHolder;
    public Transform SpawnPoint;
    private List<Worker> _workerPool;

    //Singelton Awake
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        _workerPool = new List<Worker>();

        for (int i = 0; i < StartPool; i++)
        {
            Worker worker = GameObject.Instantiate(WorkerPrefab, WorkerHolder).GetComponent<Worker>();
            worker.IsActivePool = false;
            _workerPool.Add(worker);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // todo some test stuff
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Spawn();
        }
        if(Input.GetKey(KeyCode.Z))
        {
            Remove(_workerPool.FirstOrDefault(each => each.IsActivePool));
        }
    }

    public void Spawn()
    {
        Worker w = GetFreeWorker();
        w.transform.position = SpawnPoint.position;
        w.IsActivePool = true;
    }

    /// <summary>
    /// remove specific worker
    /// </summary>
    /// <param name="w"></param>
    public void Remove(Worker w)
    {
        w.IsActivePool = false;
    }

    /// <summary>
    /// remove any active worker from room
    /// </summary>
    /// <param name="roomIndex">room-index</param>
    public void RemoveWorkerFromRoom(int roomIndex)
    {
        if(_workerPool.Any(each => each.RoomIndex == roomIndex))
        {
            _workerPool.FirstOrDefault(each => each.RoomIndex == roomIndex).IsActivePool = false; ;
        }
    }  
    
    /// <summary>
    /// remove any worker of worker-type and rooom-index
    /// </summary>
    /// <param name="roomIndex">room-index</param>
    /// <param name="workerType">worker-type</param>
    public void RemoveWorkerFromRoomWithType(int roomIndex, int workerType)
    {
        if(_workerPool.Any(each => each.RoomIndex == roomIndex && each.WorkerType == workerType))
        {
            _workerPool.FirstOrDefault(each => each.RoomIndex == roomIndex && each.WorkerType == workerType).IsActivePool = false; ;
        }
    }

    private Worker GetFreeWorker()
    {
        if(_workerPool.Any(each => !each.IsActivePool))
        {
            return _workerPool.First(each => !each.IsActivePool);
        }
        
        // add new pool entity
        Worker worker = GameObject.Instantiate(WorkerPrefab, WorkerHolder).GetComponent<Worker>();
        _workerPool.Add(worker);
        return worker;
    }
}
