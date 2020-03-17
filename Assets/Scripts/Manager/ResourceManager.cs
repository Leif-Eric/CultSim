using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    /*
     * Für Maxie :*
     * 
     * Danke für alle die Viele Arbeit 
     * 
     * Diese Klasse ist wie folgt auf gebaut:
     *  - erstens Singelton
     *  - zweitens attribute geordnet nach den vier Ressourcen 
     *  - Faith (0-100);
     *      - steigt immer
     *      - uFaith= unused Faith (0-Faith).
     *          - kann verbraucht werden
     *          - Faith kann am schrein gegen Segen (Blutregen etc getauscht werden)getauscht werden
     *          - Faith bleibt auf der selben höhe aber uFaith wird verringert
     *  - Watchscore (0-100)
     *      - steigt immer
     *      - 4 Levels. 
     *      - 0 = start Level
     *      - 1 = observation Level
     *      - 2 = attack Level
     *      - 3 U LOOSE
     *  - Money (0-X)
     *      - kann verbraucht werden
     *  - Worker(0-50)
     *      - kann verbraucht werden
     *      - fWorker = Free Worker(0-Worker).
     *    
     * -Als nächstes wird in den Abschnitten nach Konstant, 
     *      lauf variable (also die die angezeigt werden müssen), 
     *      sub attribute (steigungs rate etc.) 
     *      und als letztes Stellschrauben 
     * 
     */


    //Standart shit
    private static ResourceManager _instance;
    [HideInInspector]
    public static ResourceManager Instance { get { return _instance; } }
    private WorkerManager wm;

    //--------------------------------------------------------------------------------------------------------
    //Faith
    //--------------------------------------------------------------------------------------------------------

    //Konstanten
    public readonly int WIN_FAITH = 100;  

    //MainVariables
    [HideInInspector]
    public float faith;
    [HideInInspector]
    public float faithGrowth;
    [HideInInspector]
    public float faithGrowthModifyer;
    [HideInInspector]
    public float uFaith;

    //subvariables
    [HideInInspector]
    public float faithPW;
    [HideInInspector]
    public float faithPerSacrifice;
    [HideInInspector]
    public float watchscoreGrowthPerSacrifice;
    //Stellschrauben
    public float standardFaithPW;
    public float standardFaithPerSacrifice;
    public float standardWatchscoreGrowthPerSacrifice;
    [Space(20)]

    //--------------------------------------------------------------------------------------------------------
    //Watchscore
    //--------------------------------------------------------------------------------------------------------
    //Konstanten
    public readonly int LOOSE_WATCHSCORE = 100;
    public readonly int LEVEL_ONE_WATCHSCORE = 25;
    public readonly int LEVEL_TWO_WATCHSCORE = 75;

    //MainVariables
    [HideInInspector]
    public float watchscore;
    [HideInInspector]
    public float watchscoreGrowth;
    [HideInInspector]
    public float watchscoreGrowthModifyer;
    [HideInInspector]
    public float moneyLoss;
    [HideInInspector]
    public float workerKillRate;
    private float killRateCounter;

    //subvariables
    [HideInInspector]
    public float watchscoreDividerNormal;
    [HideInInspector]
    public float watchscoreDividerMiddle;
    [HideInInspector]
    public float watchscoreDividerHigh;

    [HideInInspector]
    public float moneyLossDividerMiddle;
    

    [HideInInspector]
    public float wKillrateDividerHigh;

    [HideInInspector]
    public float kostPerMilitaWorker;
    //Stellschrauben
    public float standardWatchscoreGrowth;
    public float standardMoneyLossOnLevelTwo;
    public float standardWorkerKillRateOnLevelThree;

    [Space(10)]
    public float standardWatchscoreDividerNormal;
    public float standardWatchscoreDividerMiddle;
    public float standardWatchscoreDividerHigh;
    [Space(10)]
    public float standardMoneyLossDividerMiddle;
    [Space(10)]
    public float standardWKillrateDividerHigh;
    public float standardKostPerMilitaWorker;
    [Space(20)]
    //--------------------------------------------------------------------------------------------------------
    //Money
    //--------------------------------------------------------------------------------------------------------
    //Konstanten
    //MainVariables
    [HideInInspector]
    public float money;
    [HideInInspector]
    public int roundedMoney;
    [HideInInspector]
    public float moneyGrowth;
    [HideInInspector]
    public float moneyGrowthModifyer;
    //subvariables
    [HideInInspector]
    public float moneyPWNormal;
    [HideInInspector]
    public float moneyPWMiddle;
    [HideInInspector]
    public float moneyPWHigh;

    [HideInInspector]
    public float faithPMWMiddle;

    [HideInInspector]
    public float watchscoreGrowthPMWHigh;
    //Stellschrauben
    public int startMoney;
    [Space(10)]
    public float standardMoneyPWNormal;
    public float standardMoneyPWMiddle;
    public float standardMoneyPWHigh;
    [Space(10)]
    public float standardFaithPMWMiddle;
    public float standardwatchscoreGrowthPMWHigh;
    [Space(20)]
    //--------------------------------------------------------------------------------------------------------
    //Workers
    //--------------------------------------------------------------------------------------------------------
    //Konstanten
    //MainVariables
    [HideInInspector]
    public float workers;
    [HideInInspector]
    public int roundedWorkers;
    [HideInInspector]
    public int freeWorkers;
    [HideInInspector]
    public float workerGrowth;
    [HideInInspector]
    public float workerGrowthInPercent;
    [HideInInspector]
    public float workerGrowthModifyer;
    //subvariables
    [HideInInspector]
    public float workerPWNormal;
    [HideInInspector]
    public float workerPWMiddle;
    [HideInInspector]
    public float workerPWHigh;

    [HideInInspector]
    public float faithPWWMiddle;
    [HideInInspector]
    public float watchscoreGrowthPWWHigh;


    //Stellschrauben
    public float standardWorkerGrowth;
    [Space(10)]
    public float standardWorkerPWNormal;
    public float standardWorkerPWMiddle;
    public float standardWorkerPWHigh;
    [Space(10)]
    public float standardFaithPWWMiddle;
    public float standardWatchscoreGrowthPWWHigh;

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

    public void Initialise()
    {
        wm = WorkerManager.Instance;
        //Faith
  
        faith=0;
        faithGrowth = 0;
        faithGrowthModifyer = 1;
        uFaith =0;
        faithPW=standardFaithPW;
        faithPerSacrifice=standardFaithPerSacrifice;
        watchscoreGrowthPerSacrifice=standardWatchscoreGrowthPerSacrifice;

        //Watchscore

        watchscore=0;
        watchscoreGrowth=standardWatchscoreGrowth;
        watchscoreGrowthModifyer = 1;
        moneyLoss=0;

        workerKillRate=0;
        killRateCounter=0;

        watchscoreDividerNormal=standardWatchscoreDividerNormal;
        watchscoreDividerMiddle = standardWatchscoreDividerMiddle;
        watchscoreDividerHigh = standardWatchscoreDividerHigh;

        moneyLossDividerMiddle=standardMoneyLossDividerMiddle;

        wKillrateDividerHigh=standardWKillrateDividerHigh;
        kostPerMilitaWorker=standardKostPerMilitaWorker;

        //Money

        money=startMoney;
        roundedMoney=(int)money;
        moneyGrowth=0;
        moneyGrowthModifyer = 1;

        moneyPWNormal=standardMoneyPWNormal;
        moneyPWMiddle=standardMoneyPWMiddle;
        moneyPWHigh= standardMoneyPWHigh;

        faithPMWMiddle=standardFaithPMWMiddle;
        watchscoreGrowthPMWHigh=standardwatchscoreGrowthPMWHigh;

        //Worker

        workers=wm.startWorker;
        roundedWorkers=(int)workers;
        freeWorkers=roundedWorkers;
        workerGrowth=standardWorkerGrowth;
        workerGrowthInPercent=(int)(workerGrowth*100);
        workerGrowthModifyer = 1;

        workerPWNormal=standardWorkerPWNormal;
        workerPWMiddle = standardWorkerPWMiddle;
        workerPWHigh = standardWorkerPWHigh;

        faithPWWMiddle=standardFaithPWWMiddle;
        watchscoreGrowthPWWHigh=standardWatchscoreGrowthPWWHigh;

    }

    public void UpdateRessources()
    {
        //evry sec. one update
        faithGrowth = faithGrowthModifyer * (
            faithPW * wm.faithWorker +
            faithPMWMiddle * wm.moneyWorkerMiddle +
            faithPWWMiddle * wm.watchscoreWorkerMiddle);
        faith += faithGrowth+wm.workersSacrifycedSinceLastUpdate*faithPerSacrifice;
        uFaith += faithGrowth + wm.workersSacrifycedSinceLastUpdate * faithPerSacrifice;

        watchscoreGrowth = watchscoreGrowthModifyer * (
            watchscoreDividerNormal * wm.watchscoreWorkerNormal +
            watchscoreDividerMiddle * wm.watchscoreWorkerMiddle +
            watchscoreDividerHigh * wm.watchscoreWorkerHigh +
            watchscoreGrowthPMWHigh * wm.moneyWorkerHigh +
            watchscoreGrowthPWWHigh * wm.workerWorkerHigh
            );
        watchscore += watchscoreGrowth;

        moneyGrowth = moneyGrowthModifyer * (
            moneyPWNormal * wm.moneyWorkerNormal +
            moneyPWMiddle * wm.moneyWorkerMiddle +
            moneyPWHigh * wm.moneyWorkerHigh
            );
        money += moneyGrowth;
        roundedMoney = (int)money;

        workerGrowth = workerGrowthModifyer * (
            workerPWNormal*wm.workerWorkerNormal+
            workerPWMiddle*wm.workerWorkerMiddle+
            workerPWHigh*wm.workerWorkerHigh
            );
        workerGrowthInPercent = (int)(workerGrowth * 100);
        workers += workerGrowth;
        workers -= wm.workersSacrifycedSinceLastUpdate;
        roundedWorkers = (int)workers;
        freeWorkers = wm.freeWorkers;
    }


}
