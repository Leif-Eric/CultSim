using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomUiView : SubMenuView
{
    public TextMeshProUGUI UpdateInfoTxt, CurrentWorkInfoTxt, OtherInfoTxt;
    public Button PermanentBtn;
    public Slider U1Slider, U2Slider;

    public List<UpgradeEntry> UpgradeEntries = new List<UpgradeEntry>();
    public List<WorkerEntry> WorkerEntries = new List<WorkerEntry>();

    public Image U1Part, U2Part, EndPart;

    public Sprite UpgradeActive, UpgradeInactive, UpgradeActiveEnd, UpgradeInactiveEnd;

    private int _u1Val, _u2Val, _sliderMax, _updateIndex;

    private bool _canAssignPermanent;

    private void Start()
    {
        _sliderMax = 5;
    }

    public void OpenRoom(int roomIndex)
    {
        //get room data withj room index
        Open();

        UpdateView();
    }

    private void UpdateView()
    {
        //todo get index from room-data
        _updateIndex = 0;

        foreach (var worker in WorkerEntries)
        {
            worker.Init(WorkerEntry.State.Free);
        }

        foreach (var upgrade in UpgradeEntries)
        {
            upgrade.Init(WorkerEntry.State.Free);
        }

        PermanentBtn.interactable = _canAssignPermanent;

        U1Part.sprite = _updateIndex > 0 ? UpgradeActive : UpgradeInactive;
        U2Part.sprite = _updateIndex > 1 ? UpgradeActive : UpgradeInactive;
        EndPart.sprite = _updateIndex > 1 ? UpgradeActiveEnd : UpgradeInactiveEnd;

        UpdateInfoTxt.text = string.Empty;
        CurrentWorkInfoTxt.text = "Current status of used upgrade";
        OtherInfoTxt.text = "I dont knwo what should be here.";
    }

    public void UpdateSliderActivation(int updateIndex)
    {
        if(updateIndex == 0)
        {
            U1Slider.gameObject.SetActive(false);
            U2Slider.gameObject.SetActive(false);
        }
        else
        {
            U1Slider.gameObject.SetActive(updateIndex > 0);
            U2Slider.gameObject.SetActive(updateIndex > 1);
            _sliderMax = updateIndex == 1 ? 7 : 9;
        }      
    }

    public void OnU1SliderChanged()
    {
        _u1Val = (int)U1Slider.value;
        
        U2Slider.minValue = _u1Val;
    }

    public void OnU2SliderChanged()
    {
        _u2Val = (int)U2Slider.value;
    }
}
