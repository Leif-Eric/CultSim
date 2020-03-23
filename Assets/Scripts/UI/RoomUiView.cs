using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static Messages;

public class RoomUiView : SubMenuView
{
    public TextMeshProUGUI UpdateInfoTxt, CurrentWorkInfoTxt, OtherInfoTxt, PermanentBtnTxt;
    public Button PermanentBtn, UpgradeBtn;
    public Slider U1Slider, U2Slider;

    public RectTransform SliderRect;

    public List<UpgradeEntry> UpgradeEntries = new List<UpgradeEntry>();
    public List<WorkerEntry> WorkerEntries = new List<WorkerEntry>();

    public Image U1Part, U2Part, EndPart;

    public Sprite UpgradeActive, UpgradeInactive, UpgradeActiveEnd, UpgradeInactiveEnd;

    private int _u1Val, _u2Val, _sliderMax, _updateIndex;

    private bool _u1Active, _u2Active;

    private Panel _roomData;

    private float _xOffset;
    private int _roomIndex;

    private void Awake()
    {
        GameController.MessageBus.Subscribe<RoomUpdatedMessage>(OnRoomUpdated);
        _xOffset = SliderRect.rect.width / 10;
    }

    public void OpenRoom(int roomIndex)
    {
        _roomIndex = roomIndex;
        Open();

        _roomData = UpgradeManager.Instance.GetRoomData(roomIndex);
        _roomData.UpdatePanel();

        UpdateView();
    }

    private void UpdateView()
    {
        _u1Active = _roomData.upgradeStatus[0];
        _u2Active = _roomData.upgradeStatus[1];

        UpdateWorkers();
        UpdateWorkerUpgrades();

        PermanentBtn.interactable = _roomData.upgradeButtonWorker;
        UpgradeBtn.interactable = _roomData.upgradeButtonRoom;

        U1Part.sprite = _u1Active ? UpgradeActive : UpgradeInactive;
        U2Part.sprite = _u2Active ? UpgradeActive : UpgradeInactive;
        EndPart.sprite = _u2Active ? UpgradeActiveEnd : UpgradeInactiveEnd;

        UpdateInfoTxt.text = _roomData.upgradeButtonRoomText;
        CurrentWorkInfoTxt.text = _roomData.ressourceInfoPanelText;
        OtherInfoTxt.text = _roomData.workerUpgradeInfoText;
        PermanentBtnTxt.text = _roomData.upgradeButtonWorkerText;

        UpdateSliderActivation(_u1Active, _u2Active);
        if(_u1Active)
        {
            UpdateSliderValues();
        }
    }

    private void UpdateWorkers()
    {
        int defaultCount = 5;

        if (_u1Active)
            defaultCount += 2;
        if (_u2Active)
            defaultCount += 2;

        int unassigned = Mathf.Max(0, defaultCount - _roomData.workers);

        for (int i = 0; i < WorkerEntries.Count; i++)
        {
            if (i < _roomData.workers)
            {
                WorkerEntries[i].ChangeState(WorkerEntry.State.Occupied);
            }
            else
            {
                WorkerEntries[i].ChangeState(WorkerEntry.State.Locked);
            }
        }

        WorkerEntry[] unassignedWorkers = WorkerEntries.Where(each => each.CurrentState == WorkerEntry.State.Locked).ToArray();

        for (int i = 0; i < unassigned; i++)
        {
            unassignedWorkers[i].ChangeState(WorkerEntry.State.Free);
        }
    }

    private void UpdateWorkerUpgrades()
    {
        UpgradeEntries[0].Init(WorkerEntry.State.Locked);
        UpgradeEntries[1].Init(WorkerEntry.State.Locked);

        if(_u1Active)
        {
            UpgradeEntries[0].Setup(this);
            UpgradeEntries[0].ChangeState(_roomData.upgradeStatus[2] ? WorkerEntry.State.Occupied : WorkerEntry.State.Free);
        } 
        if(_u2Active)
        {
            UpgradeEntries[1].Setup(this);
            UpgradeEntries[1].ChangeState(_roomData.upgradeStatus[3] ? WorkerEntry.State.Occupied : WorkerEntry.State.Free);
        }
    }

    private void UpdateSliderActivation(bool u1, bool u2)
    {
        if(!u1)
        {
            U1Slider.gameObject.SetActive(false);
            U2Slider.gameObject.SetActive(false);
        }
        else
        {
            U1Slider.gameObject.SetActive(u1);
            U2Slider.gameObject.SetActive(u2);
        }      
    }

    private void UpdateSliderValues()
    {
        U1Slider.SetValueWithoutNotify(_roomData.workerTypeOne); 
        U2Slider.SetValueWithoutNotify(_roomData.workerTypeTwo);

        U2Slider.minValue = 0;
        U2Slider.maxValue = _roomData.workers+1;

        U1Slider.minValue = 0;
        U1Slider.maxValue = _roomData.workers+1 - U2Slider.value;

        RectTransform ru2 = U2Slider.GetComponent<RectTransform>();
        ru2.sizeDelta = new Vector2(_xOffset * _roomData.workers, 20);
        
        RectTransform ru1 = U1Slider.GetComponent<RectTransform>();
        ru1.sizeDelta = new Vector2(Mathf.Max(_xOffset * U1Slider.maxValue,1), 20);

        ru2.anchoredPosition = Vector3.zero;
        ru1.anchoredPosition = Vector3.zero;

        ru2.anchoredPosition = new Vector3(ru2.rect.width, 0);
        ru1.anchoredPosition = new Vector3(ru1.rect.width + _xOffset * U2Slider.value, 0); 
    }

    public void OnU1SliderChanged()
    {
        _roomData.changeWorkerConstellation(_roomData.workers - (int)U1Slider.value - (int)U2Slider.value, (int)U1Slider.value, (int)U2Slider.value);
    }

    public void OnU2SliderChanged()
    {
        int u2 = (int)U2Slider.value;

        U1Slider.maxValue = _roomData.workers - u2+1;
        if((int)U1Slider.value > U1Slider.maxValue)
        {
            U1Slider.SetValueWithoutNotify(U1Slider.maxValue);
        }

        _roomData.changeWorkerConstellation(_roomData.workers - (int)U1Slider.value - (int)U2Slider.value, (int)U1Slider.value, (int)U2Slider.value);
    }

    public void UpgradeEntryClicked(int workerupgardeId)
    {
        _roomData.UpdateWorkerButton(workerupgardeId);
    }

    public void BuyUpgradeClicked()
    {
        _roomData.UpgradeRoom();
    }

    public void AssignPermanentWorker()
    {
        _roomData.UpgradeWorker();
    }

    private void OnRoomUpdated(RoomUpdatedMessage msg)
    {
        if(msg.roomID == _roomIndex  && IsActive)
        {
            UpdateView();
        }
    }
}
