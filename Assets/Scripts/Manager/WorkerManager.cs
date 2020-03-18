using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int CurrentWorker;

    public const int StartPool = 10;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
