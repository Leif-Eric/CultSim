using UnityEngine.UI;

public class UpgradeEntry : WorkerEntry
{
    public int UpgradeIndex;

    private RoomUiView _roomUI;
    private Button _upgradeBtn;

    public int UpgradeWorkerId;

    private void Awake()
    {
        _upgradeBtn = GetComponent<Button>();
    }

    public void Setup(RoomUiView roomUi)
    {
        _roomUI = roomUi;
    }

    public void Clicked()
    {
        _roomUI.UpgradeEntryClicked(UpgradeWorkerId);
    }

    public override void ChangeState(State target)
    {
        base.ChangeState(target);

        _upgradeBtn.interactable = target != State.Locked;
    }
}
