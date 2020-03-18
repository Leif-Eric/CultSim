using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel 
{
    public int roomID;

    public Upgrade roomUpgradeOne;
    public Upgrade roomUpgradeTwo;
    public Upgrade workerUpgradeOne;
    public Upgrade workerUpgradeTwo;

    public int workerTypeZero;
    public int workerTypeOne;
    public int workerTypeTwo;

    public bool[] upgradeStatus;
    private ResourceManager rm;
    private WorkerManager wm;

    public bool upgradeButtonRoom;
    public bool upgradeButtonWorker;

    public string upgradeButtonRoomText;
    public string upgradeButtonWorkerText;

    public string workerUpgradeInfoText;
    public string ressourceInfoPanelText;

    public Panel(int id,Upgrade rOne, Upgrade rTwo, Upgrade wOne, Upgrade wTwo)
    {
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
        workerTypeOne = 1;
        workerTypeTwo = 2;

        upgradeStatus = new bool[]{ false, false, false, false }; 

        roomUpgradeOne = rOne;
        roomUpgradeTwo = rTwo;

        workerUpgradeOne = wOne;
        workerUpgradeTwo = wTwo;
    }
    //Aufrufen wenn slider benutzt wurden 
    public void changeWorkerConstellation(int wTypeZero,int wTypeOne, int wTypeTwo)
    {
        workerTypeZero = wTypeZero;
        workerTypeOne = wTypeOne;
        workerTypeTwo = wTypeTwo;

        UpdatePanel();
    }
    //Aufrufen nur wenn das Panel geöffnet wird
    public void UpdatePanel()
    {

        UpdateRoomButton();
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
    }

    public void UpdateResourceInfo()
    {
        switch (roomID)
        {
            case 1:
                //watchscore
                ressourceInfoPanelText =
                    " wathchscore growth reduction: " + ((workerTypeZero * rm.watchscoreDividerNormal + workerTypeOne * rm.watchscoreDividerMiddle + workerTypeTwo * rm.watchscoreDividerHigh) * rm.watchscoreGrowthModifyer) + "/n" +
                    "Worker killrate: " + (rm.standardWorkerKillRateOnLevelThree - workerTypeTwo * rm.wKillrateDividerHigh  *rm.actualWatchscorePhase > 1 ? 1 : 0) + "/n" +
                    "Militia Cost: " + (workerTypeTwo * rm.kostPerMilitaWorker) + "/n" +
                    "Money Confiscation: " + (rm.moneyLoss - workerTypeOne * rm.moneyLossDividerMiddle*rm.actualWatchscorePhase>0?1:0) + "/n"+
                    "Aktual Phase: "+ rm.actualWatchscorePhase+"/n"
                    ;
                if (rm.actualWatchscorePhase == 0)
                    ressourceInfoPanelText += "Your Cult is not estimated to be dangerouse. But be carefull this assesment can change quickly!";
                if (rm.actualWatchscorePhase == 1)
                    ressourceInfoPanelText += "There are People in this Country who try to bring your Cult down with every peacefull Methode possible. /n"+
                        "Some of your Moneysources will dryout. Be carefull, if the Watchescore gets higher, the police will try to storm your farm";
                if (rm.actualWatchscorePhase == 2)
                    ressourceInfoPanelText += "Not only do they try to cut your Ressources, they also try to storm the farm./n"+
                        " The Skirmish with the police endanger your followers. Hurryup, or the Millitary will end all your plans!";

                break;
            case 2:
                //money
                ressourceInfoPanelText =
                    "Money Growth: " + ((workerTypeZero * rm.moneyPWNormal + workerTypeOne * rm.moneyPWMiddle + workerTypeTwo * rm.moneyPWHigh) * rm.moneyGrowthModifyer) + "/n" +
                    "Faith Growth: " + workerTypeOne * rm.faithPMWMiddle * rm.faithGrowthModifyer + "/n" +
                    "Watchscore rise: " + workerTypeTwo * rm.watchscoreGrowthPMWHigh;
                    ;

                break;
            case 3:
                //worker
                ressourceInfoPanelText =
                    "Worker recruitmantrate: " + ((workerTypeZero * rm.workerPWNormal + workerTypeOne * rm.workerPWMiddle + workerTypeTwo * rm.workerPWHigh) * rm.workerGrowthModifyer) + "/n" +
                    "Faith Growth: " + workerTypeOne * rm.faithPWWMiddle * rm.faithGrowthModifyer + "/n" +
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
        }
        else
        {
            upgradeStatus[0] = true;
            roomUpgradeOne.BuyUpgrade();
            rm.money -= roomUpgradeOne.cost;
        }

        UpdateResourceInfo();
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
            case 1:
                wm.watchscoreWorkerNormal++;
                break;
            case 2:
                wm.moneyWorkerNormal++;
                break;
            case 3:
                wm.workerWorkerNormal++;
                break;
        }
    }
    public void RemoveWorker(int workerType)
    {
        switch (roomID)
        {
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
       
    }
    public void killWorker()
    {
        if (workerTypeZero > 0)
        {
            RemoveWorker(0);
            //pool worker
        }
        else if (workerTypeOne > 0)
        {
            RemoveWorker(1);
            //pool worker
        }
        else
        {
            RemoveWorker(2);
            //pool worker
        }
    }
}
