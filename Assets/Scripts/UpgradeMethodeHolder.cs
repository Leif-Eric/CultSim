using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMethodeHolder : MonoBehaviour
{
    private static UpgradeMethodeHolder _instance;
    [HideInInspector]
    public static UpgradeMethodeHolder Instance { get { return _instance; } }
    public static GameObject waRoomBack;
    public static GameObject moRoomBack;
    public static GameObject woRoomBack;
    public static GameObject outRoomBack;
    public  GameObject SetwaRoomBack;
    public  GameObject SetmoRoomBack;
    public  GameObject SetwoRoomBack;
    public  GameObject SetoutRoomBack;
    //RoomUpgrades
    
    public static GameObject moRoomBackOne;
    public static float moROneMoney;
    public static float moROneWorker;
    [Space(15)]
    public  GameObject SetmoRoomBackOne;
    public  float SetmoROneMoney;
    public  float SetmoROneWorker;
    
    public static GameObject woRoomBackOne;
    public static float woROneMoney;
    public static float woROneWorker;
    public static float woROneWatchscore;
    [Space(10)]
    public  GameObject SetwoRoomBackOne;
    public  float SetwoROneMoney;
    public  float SetwoROneWorker;
    public  float SetwoROneWatchscore;
    
    public static GameObject waRoomBackOne;
    public static GameObject outRoomBackOne;
    public static float waROneMoney;
    public static float waROneWatchscore;
    [Space(10)]
    public  GameObject SetwaRoomBackOne;
    public  GameObject SetoutRoomBackOne;
    public  float SetwaROneMoney;
    public  float SetwaROneWatchscore;
    [Space(15)]
    public GameObject christalLabor;
    public float drugfaith;
    public float drugworker;
    public float drugwatchscore;
    [Space(10)]
    public GameObject RoomThree;
    public float brainfaith;
    public float brainmoney;
    public float brainworker;
    public float brainwatchscore;
    [Space(10)]
    public GameObject FortNox;
    public float watchscoreFlat;
    public float watchscorediv;
    
    //WorkerUpgrades

    public static GameObject waWOne;
    public static float waWOneWatchscore;
    public static float waWOneMoneyloss;
    public static float waWOneKillrate;
    [Space(15)]
    public  GameObject SetwaWOne;
    public  float SetwaWOneWatchscore;
    public  float SetwaWOneMoneyloss;
    public  float SetwaWOneKillrate;
    
    public static GameObject woWOne;
    public static float woWOneMoney;
    public static float woWOneFaith;
    [Space(10)]
    public  GameObject SetwoWOne;
    public  float SetwoWOneMoney;
    public  float SetwoWOneFaith;

    public static GameObject moWOne;
    public static float moWOneWorker;
    [Space(10)]
    public  GameObject SetmoWOne;
    public  float SetmoWOneWorker;




    [Space(15)]
    public GameObject Smoke;
    public float flatMoneyBoost;
    public float flatFaithBoost;
    public float flatWatchscoreBoost;
    [Space(10)]
    public GameObject Doc;
    public float flatWatchscoreCut;
    public float MoneyToPaiflat;
    [Space(10)]
    public GameObject Martyr;
    public float flatWatchscoreCuttwo;
    public float flatWatchscoreDiv;
    public float killrateRaise;

    //faithupgrades
    public static GameObject bloodRainObjekt;
    public static GameObject background;
    public static Color redTint;
    [Space(15)]
    public  GameObject SetbloodRainObjekt;
    public  GameObject Setbackground;
    public  Color SetredTint;
    [Space(10)]
    public GameObject eyes;
    public float watchscoreEyDrop;
    [Space(10)]
    public GameObject BloodPit;
    public float faithPerSac;
    [Space(10)]
    public GameObject deamonProtector;

    [Space(10)]
    public GameObject money;
    public float moneypersec;


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


        waRoomBack = SetwaRoomBack;
        moRoomBack= SetmoRoomBack;
        woRoomBack = SetwoRoomBack;
        outRoomBack = SetoutRoomBack;

        //RoomUpgrades

        moRoomBackOne = SetmoRoomBackOne;
        moROneMoney = SetmoROneMoney;
        moROneWorker = SetmoROneWorker;

        woRoomBackOne = SetwoRoomBackOne;
        woROneMoney = SetwoROneMoney;
        woROneWorker = SetwoROneWorker;
        woROneWatchscore = SetwoROneWatchscore;

        waRoomBackOne = SetwaRoomBackOne;
        outRoomBackOne = SetoutRoomBackOne;
        waROneMoney = SetwaROneMoney;
        waROneWatchscore = SetwaROneWatchscore;

        //WorkerUpgrades

        waWOne = SetwaWOne;
        waWOneWatchscore = SetwaWOneWatchscore;
        waWOneMoneyloss = SetwaWOneMoneyloss;
        waWOneKillrate = SetwaWOneKillrate;

        woWOne = SetwoWOne;
        woWOneMoney = SetwoWOneMoney;
        woWOneFaith = SetwoWOneFaith;

        moWOne = SetmoWOne;
        moWOneWorker=SetmoWOneWorker;

        //faithupgrades
        bloodRainObjekt = SetbloodRainObjekt;
        background = Setbackground;
        redTint = SetredTint;



    }

    //RoomUpgrades
    public static void Trending()
    {
        moRoomBack.SetActive(false);
        moRoomBackOne.SetActive(true);

        ResourceManager.Instance.moneyGrowthModifyer += moROneMoney;
        ResourceManager.Instance.workerGrowthModifyer += moROneWorker;
    }

    public static void Politics()
    {
        waRoomBack.SetActive(false);
        waRoomBackOne.SetActive(true);
        outRoomBack.SetActive(false);
        outRoomBackOne.SetActive(true);


        ResourceManager.Instance.moneyGrowthModifyer += waROneMoney;
        ResourceManager.Instance.watchscoreGrowthModifyer += waROneWatchscore;
    }

    public static void SocialMedia()
    {
        woRoomBack.SetActive(false);
        woRoomBackOne.SetActive(true);

        ResourceManager.Instance.moneyGrowthModifyer += woROneMoney;
        ResourceManager.Instance.workerGrowthModifyer += woROneWorker;
        ResourceManager.Instance.standardWatchscoreGrowth += woROneWatchscore;
    }

    //workerUpgrades

    public static void HollywoodStar()
    {
        WorkerManager.Instance.freeWorkers += 3;
        WorkerManager.Instance.Spawn();
        WorkerManager.Instance.Spawn();
        WorkerManager.Instance.Spawn();

        moWOne.SetActive(true);
        ResourceManager.Instance.workerGrowthModifyer += moWOneWorker;
    }

    public static void Politicion()
    {

        waWOne.SetActive(true);
        ResourceManager.Instance.watchscoreGrowthModifyer += waWOneWatchscore;
        ResourceManager.Instance.standardMoneyLossOnLevelTwo -= waWOneMoneyloss;
        ResourceManager.Instance.standardWorkerKillRateOnLevelThree -= waWOneKillrate;
    }
    public static void Televangelist()
    {
        woWOne.SetActive(true);
        ResourceManager.Instance.moneyGrowthModifyer += woWOneMoney;
        ResourceManager.Instance.faithGrowthModifyer += woWOneFaith;
    }





    //FaithUpgrades

    public static void BloodRain()
    {
        bloodRainObjekt.SetActive(true);
        background.GetComponent<SpriteRenderer>().color = redTint;
        //Sound?
        Debug.Log("open the gates");
    }




    public void Upgrade(int i)
    {
        switch (i)
        {
            case 0:
                Trending();
                break;
            case 1:
                Politics();
                break;
            case 2:
                SocialMedia();
                break;
            case 3:
                HollywoodStar();
                break;
            case 4:
                Politicion();
                break;
            case 5:
                Televangelist();
                break;
            case 6:
                ChristalFaith();
                break;
            case 7:
                Brainwashing();
                break;
            case 8:
                Millitary();
                break;
            case 9:
                WalterWhite();
                break;
            case 10:
                DocT();
                break;
            case 11:
                MartyrT();
                break;
            case 12:
                BloodRain();
                break;
            case 13:
                BloodLust();
                break;
            case 14:
                PryingEy();
                break;
            case 15:
                DeamonProtector();
                break;
            case 16:
                Greed();
                break;

        }
    }

    private void Greed()
    {
        money.SetActive(true);
        ResourceManager.Instance.moneyGrowthModifyer += moneypersec;
    }

    private void DeamonProtector()
    {
        deamonProtector.SetActive(true);
        ResourceManager.Instance.standardWorkerKillRateOnLevelThree = 0;
    }

    private void PryingEy()
    {
         eyes.SetActive(true);
        ResourceManager.Instance.watchscoreGrowthModifyer += watchscoreEyDrop;
}

    private void BloodLust()
    {
        BloodPit.SetActive(true);
        ResourceManager.Instance.standardFaithPerSacrifice += faithPerSac;
    }




   
    
   
    


    private void WalterWhite()
    {

    Smoke.SetActive(true);
        ResourceManager.Instance.money+=flatMoneyBoost;
        ResourceManager.Instance.faith += flatFaithBoost;
        ResourceManager.Instance.uFaith += flatFaithBoost;
        ResourceManager.Instance.watchscore += flatWatchscoreBoost;
}

    private void Millitary()
    {

        FortNox.SetActive(true);

        ResourceManager.Instance.watchscore -= watchscoreFlat;
        ResourceManager.Instance.watchscoreDividerHigh += watchscorediv;

    }

    private void Brainwashing()
    {
        woRoomBackOne.SetActive(false);
        RoomThree.SetActive(true);
        ResourceManager.Instance.faithGrowthModifyer += brainfaith;
        ResourceManager.Instance.moneyGrowthModifyer += brainmoney;
        ResourceManager.Instance.workerGrowthModifyer += brainworker;
        ResourceManager.Instance.standardWatchscoreGrowth += brainwatchscore;
}

    private void ChristalFaith()
    {
        christalLabor.SetActive(true);
        ResourceManager.Instance.faithGrowthModifyer += drugfaith;
        ResourceManager.Instance.workerGrowth += drugworker;
        ResourceManager.Instance.standardWatchscoreGrowth += drugwatchscore;
}
    private void DocT()
    {
        Doc.SetActive(true);
        ResourceManager.Instance.faith -= flatWatchscoreCut;
        ResourceManager.Instance.money -= MoneyToPaiflat;
    }
    private void MartyrT()
    {
        Martyr.SetActive(true);
        ResourceManager.Instance.faith -= flatWatchscoreCuttwo;
        ResourceManager.Instance.watchscoreDividerHigh += flatWatchscoreDiv;
        ResourceManager.Instance.wKillrateDividerHigh -= killrateRaise;
    }


    
    
    

}
