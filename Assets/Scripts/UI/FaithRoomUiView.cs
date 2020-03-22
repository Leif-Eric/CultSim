using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Messages;

public class FaithRoomUiView : SubMenuView
{
    public TextMeshProUGUI Up1Txt, Up2Txt, Up3Txt, UFaithTxt, SacrificeTxt;
    public Button Up1Btn, Up2Btn, Up3Btn, SacrificeBtn;

    public List<WorkerEntry> WorkerEntries = new List<WorkerEntry>();

    private Panel _roomData;

    private void Awake()
    {
        GameController.MessageBus.Subscribe<RoomUpdatedMessage>(OnRoomUpdated);
    }

    public override void Open()
    {
        base.Open();
        _roomData = _roomData = UpgradeManager.Instance.GetRoomData(0);
        _roomData.UpdatePanel();

        UpdateView();
    }

    private void UpdateView()
    {
        if(_roomData == null)
        {
            return;
        }

        for (int i = 0; i < WorkerEntries.Count; i++)
        {
            if (i < _roomData.workers)
            {
                WorkerEntries[i].ChangeState(WorkerEntry.State.Occupied);
            }
            else
            {
                WorkerEntries[i].ChangeState(WorkerEntry.State.Free);
            }
        }

        Up1Txt.text = _roomData.faithUpgradeOneText;
        Up2Txt.text = _roomData.faithUpgradeTwoText;
        Up3Txt.text = _roomData.faithUpgradeThreeText;

        Up1Btn.interactable = _roomData.isFUOne;
        Up2Btn.interactable = _roomData.isFUTwo;
        Up3Btn.interactable = _roomData.isFUThree;

        SacrificeBtn.interactable = _roomData.workers > 0;

        SacrificeTxt.text = _roomData.sacrificeButtonText;
        UFaithTxt.text = Mathf.CeilToInt(ResourceManager.Instance.uFaith).ToString();
    }

    private void OnRoomUpdated(RoomUpdatedMessage msg)
    {
        if(msg.roomID == 0 && IsActive)
        {
            UpdateView();
        }
    }

    public void BuyFaithUpgrade(int numb)
    {
        _roomData.BuyFaithUpgrade(numb);
    }

    public void Sacrifice()
    {
        _roomData.SacrificeWorker();
    }
}
