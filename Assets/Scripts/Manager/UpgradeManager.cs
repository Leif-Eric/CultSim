﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    private static UpgradeManager _instance;
    [HideInInspector]
    public static UpgradeManager Instance { get { return _instance; } }

    public Upgrade[] faithUpgrades;
    [Space(15)]
    public Upgrade roomUpgradeWatchescoreOne;
    public Upgrade roomUpgradeWatchescoreTwo;
    [Space(10)]
    public Upgrade roomUpgradeMoneyOne;
    public Upgrade roomUpgradeMoneyTwo;
    [Space(10)]
    public Upgrade roomUpgradeWorkerOne;
    public Upgrade roomUpgradeWorkerTwo;
    [Space(15)]
    public Upgrade workerUpgradeWatchescoreOne;
    public Upgrade workerUpgradeWatchescoreTwo;
    [Space(10)]
    public Upgrade workerUpgradeMoneyOne;
    public Upgrade workerUpgradeMoneyTwo;
    [Space(10)]
    public Upgrade workerUpgradeWorkerOne;
    public Upgrade workerUpgradeWorkerTwo;

    [HideInInspector]
    public Panel watchscorePanel;
    [HideInInspector]
    public Panel moneyPanel;
    [HideInInspector]
    public Panel workerPanel;

    public void InitializeUpdateManager()
    {
        watchscorePanel = new Panel(1,roomUpgradeWatchescoreOne,roomUpgradeWatchescoreTwo,workerUpgradeWatchescoreOne,workerUpgradeWatchescoreTwo);
        moneyPanel = new Panel(2,roomUpgradeMoneyOne,roomUpgradeMoneyTwo,workerUpgradeMoneyOne,workerUpgradeMoneyTwo);
        workerPanel = new Panel(3,roomUpgradeWorkerOne,roomUpgradeWorkerTwo,workerUpgradeWorkerOne,workerUpgradeWorkerTwo);
    }


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

    public Panel GetRoomData(int index)
    {
        switch(index)
        {
            case 0:
                //FAITH WO BIST DUUUUUUUU
                return null;
            case 1:
                return watchscorePanel;
            case 2:
                return moneyPanel;
            case 3:
                return workerPanel;
        }

        return null;
    }
}
