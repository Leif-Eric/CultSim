using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMethodeHolder : MonoBehaviour
{
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

    //faithupgrades
    public static GameObject bloodRainObjekt;
    public static Material background;
    public static Color redTint;
    [Space(15)]
    public  GameObject SetbloodRainObjekt;
    public  Material Setbackground;
    public  Color SetredTint;


    private void Awake()
    {
        waRoomBack= SetwaRoomBack;
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
        background.color = redTint;
        //Sound?
        Debug.Log("open the gates");
    }










}
