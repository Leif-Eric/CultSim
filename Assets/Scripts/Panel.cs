using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Messages;

public class Panel 
{
    public int roomID;

    public Upgrade roomUpgradeOne;
    public Upgrade roomUpgradeTwo;
    public Upgrade workerUpgradeOne;
    public Upgrade workerUpgradeTwo;

    public Upgrade faithUpgradeOne;
    public Upgrade faithUpgradeTwo;
    public Upgrade faithUpgradeThree;

    public string sacrificeButtonText;
    public string faithUpgradeOneText;
    public string faithUpgradeTwoText;
    public string faithUpgradeThreeText;

    public bool isFUOne;
    public bool isFUTwo;
    public bool isFUThree;

    public int workers;

    public int workerTypeZero;
    public int workerTypeOne;
    public int workerTypeTwo;

    public bool[] upgradeStatus;
    private ResourceManager rm;
    private WorkerManager wm;
    private UpgradeManager um;

    public bool upgradeButtonRoom;
    public bool upgradeButtonWorker;

    public string upgradeButtonRoomText;
    public string upgradeButtonWorkerText;

    public string workerUpgradeInfoText;
    public string ressourceInfoPanelText;



    public Panel(int id,Upgrade rOne, Upgrade rTwo, Upgrade wOne, Upgrade wTwo)
    {
        workers = 0;
        roomID = id;
        ressourceInfoPanelText = "";
        upgradeButtonRoomText = "";
        upgradeButtonWorkerText = "";
        workerUpgradeInfoText = "";
        upgradeButtonRoom = false;
        upgradeButtonWorker = false;
        rm = ResourceManager.Instance;
        wm = WorkerManager.Instance;
        workerTypeZero = 0;
        workerTypeOne = 0;
        workerTypeTwo = 0;

        upgradeStatus = new bool[]{ false, false, false, false };

            roomUpgradeOne = rOne;
            roomUpgradeTwo = rTwo;

            workerUpgradeOne = wOne;
            workerUpgradeTwo = wTwo;
       
    }
    public Panel()
    {

        workers = 0;
        roomID = 0;

        sacrificeButtonText="";
        faithUpgradeOneText="";
        faithUpgradeTwoText="";
        faithUpgradeThreeText="";

        isFUOne = false;
        isFUTwo = false;
        isFUThree = false;

        rm = ResourceManager.Instance;
        wm = WorkerManager.Instance;

        workerTypeZero = 0;
        workerTypeOne = 0;
        workerTypeTwo = 0;

        um = UpgradeManager.Instance;

        faithUpgradeOne = um.GetFaithUpgrade();
        faithUpgradeTwo = um.GetFaithUpgrade();
        faithUpgradeThree = um.GetFaithUpgrade();

        upgradeStatus = new bool[] { false, false, false, false };
    }

    //Aufrufen wenn slider benutzt wurden 
    public void changeWorkerConstellation(int wTypeZero,int wTypeOne, int wTypeTwo)
    {
        int oldZero = workerTypeZero;
        int oldOne = workerTypeOne;
        int oldTwo = workerTypeTwo;
        workerTypeZero = wTypeZero;
        workerTypeOne = wTypeOne;
        workerTypeTwo = wTypeTwo;
        switch (roomID)
        {
            case 1:
                wm.watchscoreWorkerNormal -=oldZero-workerTypeZero;
                wm.watchscoreWorkerMiddle -= oldOne - workerTypeOne;
                wm.watchscoreWorkerHigh -= oldTwo - workerTypeTwo;
                break;
            case 2:
                wm.moneyWorkerNormal -= oldZero - workerTypeZero;
                wm.moneyWorkerMiddle -= oldOne - workerTypeOne;
                wm.moneyWorkerHigh -= oldTwo - workerTypeTwo;
                break;
            case 3:
                wm.workerWorkerNormal -= oldZero - workerTypeZero;
                wm.workerWorkerMiddle -= oldOne - workerTypeOne;
                wm.workerWorkerHigh -= oldTwo - workerTypeTwo;
                break;
        }
        workers = workerTypeZero + workerTypeOne + workerTypeTwo;
        
        UpdatePanel();
    }
    //Aufrufen nur wenn das Panel geöffnet wird
    public void UpdatePanel()
    {
        if (roomID == 0)
        {
            UpdateFaithButtons();
        }
        else
        {
            UpdateRoomButton();
            UpdateResourceInfo();
        }
    }
    private void UpdateFaithButtons()
    {
        isFUOne = faithUpgradeOne.cost < rm.uFaith;
        isFUTwo = faithUpgradeTwo.cost < rm.uFaith;
        isFUThree = faithUpgradeThree.cost < rm.uFaith;

        sacrificeButtonText = "Sacrifice one of your Worker's for the geater Good." + '\n' + "Every sacrifice gives you " + rm.faithPerSacrifice*rm.faithGrowthModifyer+" Faith but raises the Watchescore by "+ rm.watchscoreGrowthPerSacrifice*rm.watchscoreGrowthModifyer+".";
        faithUpgradeOneText = "Pay "+ faithUpgradeOne.cost+ " unused Faith to get the "+ faithUpgradeOne.name+ " Upgrade:" + '\n'+ faithUpgradeOne.description;
        faithUpgradeTwoText = "Pay " + faithUpgradeTwo.cost + " unused Faith to get the " + faithUpgradeTwo.name + " Upgrade:" + '\n' + faithUpgradeTwo.description; ;
        faithUpgradeThreeText = "Pay " + faithUpgradeThree.cost + " unused Faith to get the " + faithUpgradeThree.name + " Upgrade:" + '\n' + faithUpgradeThree.description; ;
    }



    private void UpdateRoomButton()
    {
        if (upgradeStatus[1])
        {
            upgradeButtonRoom = false;
            upgradeButtonRoomText = "All Upgrades perchased!";
        }
        else if (!upgradeStatus[0])
        {
            upgradeButtonRoom = rm.money > roomUpgradeOne.cost;
            upgradeButtonRoomText = "Pay " + roomUpgradeOne.cost + " money to upgrade the room!";
        }
        else
        {
            upgradeButtonRoom = rm.money > roomUpgradeTwo.cost;
            upgradeButtonRoomText = "Pay " + roomUpgradeTwo.cost + " money to upgrade the room!";
        }
    }

    //nutzen beim klick auf einen der beiden permanenten Arbeiter Felder
    //Datet den infotext up
    //Datet den Buttontext up
    //setzt die klickbar variable
    public void UpdateWorkerButton(int witchWorkerWasKlicked)
    {
        int worker = workerTypeZero + workerTypeOne + workerTypeTwo;
        upgradeButtonWorker = (worker > 0) && !upgradeStatus[2 + witchWorkerWasKlicked];

        if (upgradeButtonWorker)
        {
            upgradeButtonWorkerText = "deploy permananent worker!";
        }
        else
        {
            if (worker == 0)
            {
                upgradeButtonWorkerText = "Not enough Worker in this Room!";
            }
            else
            {
                upgradeButtonWorkerText = "Upgrade Already bought.";
            }
        }
        if (witchWorkerWasKlicked == 0)
            workerUpgradeInfoText = workerUpgradeOne.description;
        else
            workerUpgradeInfoText = workerUpgradeTwo.description;

        GameController.MessageBus.Publish<RoomUpdatedMessage>(new RoomUpdatedMessage(roomID, false, true));
    }

    public void UpdateResourceInfo()
    {
        switch (roomID)
        {
            case 1:
                //watchscore
                ressourceInfoPanelText =
                    " wathchscore growth reduction: " + ((workerTypeZero * rm.watchscoreDividerNormal + workerTypeOne * rm.watchscoreDividerMiddle + workerTypeTwo * rm.watchscoreDividerHigh) * rm.watchscoreGrowthModifyer) + "/n" +
                    "Worker killrate: " + (rm.standardWorkerKillRateOnLevelThree - workerTypeTwo * rm.wKillrateDividerHigh  *rm.actualWatchscorePhase > 1 ? 1 : 0) + '\n' +
                    "Militia Cost: " + (workerTypeTwo * rm.kostPerMilitaWorker) + '\n' +
                    "Money Confiscation: " + (rm.moneyLoss - workerTypeOne * rm.moneyLossDividerMiddle*rm.actualWatchscorePhase>0?1:0) + '\n' +
                    "Aktual Phase: " + rm.actualWatchscorePhase + '\n';

                if (rm.actualWatchscorePhase == 0)
                    ressourceInfoPanelText += "Your Cult is not estimated to be dangerouse. But be carefull this assesment can change quickly!";
                if (rm.actualWatchscorePhase == 1)
                    ressourceInfoPanelText += "There are People in this Country who try to bring your Cult down with every peacefull Methode possible." + '\n' +
                        "Some of your Moneysources will dryout. Be carefull, if the Watchescore gets higher, the police will try to storm your farm";
                if (rm.actualWatchscorePhase == 2)
                    ressourceInfoPanelText += "Not only do they try to cut your Ressources, they also try to storm the farm." + '\n' +
                        " The Skirmish with the police endanger your followers. Hurryup, or the Millitary will end all your plans!";

                break;
            case 2:
                //money
                ressourceInfoPanelText =
                    "Money Growth: " + ((workerTypeZero * rm.moneyPWNormal + workerTypeOne * rm.moneyPWMiddle + workerTypeTwo * rm.moneyPWHigh) * rm.moneyGrowthModifyer) + '\n' +
                    "Faith Growth: " + workerTypeOne * rm.faithPMWMiddle * rm.faithGrowthModifyer + '\n' +
                    "Watchscore rise: " + workerTypeTwo * rm.watchscoreGrowthPMWHigh;
                    ;

                break;
            case 3:
                //worker
                ressourceInfoPanelText =
                    "Worker recruitmantrate: " + ((workerTypeZero * rm.workerPWNormal + workerTypeOne * rm.workerPWMiddle + workerTypeTwo * rm.workerPWHigh) * rm.workerGrowthModifyer) + '\n' +
                    "Faith Growth: " + workerTypeOne * rm.faithPWWMiddle * rm.faithGrowthModifyer + '\n' +
                    "Watchscore rise: " + workerTypeTwo * rm.watchscoreGrowthPWWHigh;
                break;
        }
    }

    //bools für kaufbar
    public void UpgradeRoom()
    {
        if (upgradeStatus[0])
        {
            upgradeStatus[1] = true;
            roomUpgradeTwo.BuyUpgrade();
            rm.money -= roomUpgradeTwo.cost;
            UpdateRoomButton();
            GameController.MessageBus.Publish<RoomUpdatedMessage>(new RoomUpdatedMessage(roomID, false));
        }
        else
        {
            upgradeStatus[0] = true;
            roomUpgradeOne.BuyUpgrade();
            rm.money -= roomUpgradeOne.cost;
            UpdateRoomButton();
            GameController.MessageBus.Publish<RoomUpdatedMessage>(new RoomUpdatedMessage(roomID, true));
        }
    }

    public void BuyFaithUpgrade(int buttonNumber)
    {
        switch (buttonNumber)
        {
            case 1:
                faithUpgradeOne.BuyUpgrade();
                rm.uFaith -= faithUpgradeOne.cost;
                faithUpgradeOne = um.GetFaithUpgrade();
                break;
            case 2:
                faithUpgradeTwo.BuyUpgrade();
                rm.uFaith -= faithUpgradeTwo.cost;
                faithUpgradeTwo = um.GetFaithUpgrade();
                break;
            case 3:
                faithUpgradeThree.BuyUpgrade();
                rm.uFaith -= faithUpgradeThree.cost;
                faithUpgradeThree = um.GetFaithUpgrade();
                break;
        }
        UpdatePanel();
    }

    public void SacrificeWorker()
    {
        rm.faith += rm.faithPerSacrifice * rm.faithGrowthModifyer;
        rm.uFaith += rm.faithPerSacrifice * rm.faithGrowthModifyer;
        rm.watchscore += rm.watchscoreGrowthPerSacrifice * rm.watchscoreGrowthModifyer;
        killWorker();
    }

    public void UpgradeWorker()
    {
        if (upgradeStatus[2])
        {
            upgradeStatus[3] = true;
            workerUpgradeTwo.BuyUpgrade();
            killWorker();
        }
        else
        {
            upgradeStatus[2] = true;
            workerUpgradeOne.BuyUpgrade();
            killWorker();
        }
        GameController.MessageBus.Publish<RoomUpdatedMessage>(new RoomUpdatedMessage(roomID, true, true));
    }

    private void PayUpgrade(Upgrade.CostType costType, float cost)
    {
        if (costType == Upgrade.CostType.money)
        {
            rm.money -= cost;
        }
        else
        {
            rm.uFaith -= cost;
        }
    }

    public void AddWorker()
    {
        switch (roomID)
        {
            case 0:
                wm.faithWorker++;
                wm.freeWorkers--;
                    break;
            case 1:
                wm.watchscoreWorkerNormal++;
                wm.freeWorkers--;
                break;
            case 2:
                wm.moneyWorkerNormal++;
                wm.freeWorkers--;
                break;
            case 3:
                wm.workerWorkerNormal++;
                wm.freeWorkers--;
                break;
        }
        changeWorkerConstellation((workerTypeZero + 1),workerTypeOne,workerTypeTwo);
    }
    public void RemoveWorker()
    {
        int workerType = GetWorkerType();
        switch (roomID)
        {
            case 0:
                wm.faithWorker--;
                break;
            case 1:
                switch (workerType)
                {
                    case 0:
                        wm.watchscoreWorkerNormal--;
                        break;
                    case 1:
                        wm.watchscoreWorkerMiddle--;
                        break;
                    case 2:
                        wm.watchscoreWorkerMiddle--;
                        break;
                }
                break;
            case 2:
                switch (workerType)
                {
                    case 0:
                        wm.moneyWorkerNormal--;
                        break;
                    case 1:
                        wm.moneyWorkerMiddle--;
                        break;
                    case 2:
                        wm.moneyWorkerHigh--;
                        break;
                }
                break;
            case 3:
                switch (workerType)
                {
                    case 0:
                        wm.workerWorkerNormal--;
                        break;
                    case 1:
                        wm.workerWorkerMiddle--;
                        break;
                    case 2:
                        wm.workerWorkerHigh--;
                        break;
                }
                break;
        }
        switch (workerType)
        {
            case 0:
                changeWorkerConstellation(workerTypeZero - 1, workerTypeOne, workerTypeTwo);
                break;
            case 1:
                changeWorkerConstellation(workerTypeZero, workerTypeOne - 1, workerTypeTwo);
                break;
            case 2:
                changeWorkerConstellation(workerTypeZero - 1, workerTypeOne, workerTypeTwo - 1);
                break;
        }
        wm.freeWorkers++;
    }


    public void killWorker()
    {
        RemoveWorker();
        wm.freeWorkers--;
        wm.RemoveWorkerFromRoom(roomID);
    }
    private int GetWorkerType()
    {

        if (workerTypeZero > 0)
            return 0;
        else if (workerTypeOne > 0)
            return 1;
        else
            return 2;

    }
}
