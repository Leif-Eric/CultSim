using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Messages;

public class RessourceInfoComponent : MonoBehaviour
{
    public TextMeshProUGUI WorkerTxt, FaithTxt, MenaceTxt, Moneytxt;
    public Sprite Watch0, Watch1, Watch2;
    public Image WachtImage;

    private ResourceManager _ressourceManager;
    public void Awake()
    {
        GameController.MessageBus.Subscribe<RessourcesUpdatedMessage>(OnRessourcesUpdated);
    }

    private void Start()
    {
        _ressourceManager = ResourceManager.Instance;
        UpdateInfos();
    }

    private void OnRessourcesUpdated(RessourcesUpdatedMessage msg)
    {
        UpdateInfos();
    }

    private void UpdateInfos()
    {
        WorkerTxt.text = _ressourceManager.roundedWorkers.ToString() + "/" + _ressourceManager.freeWorkers.ToString();
        Moneytxt.text = _ressourceManager.roundedMoney.ToString();
        FaithTxt.text = _ressourceManager.faith + "/" + _ressourceManager.uFaith;
        MenaceTxt.text = _ressourceManager.watchscore.ToString();

        Sprite s = null;
        switch(_ressourceManager.GetWatchLevel())
        {
            case 0:
                s = Watch0;
                break;
            case 1:
                s = Watch1;
                break;
            case 2:
                s = Watch2;
                break;
        }

        WachtImage.sprite = s;
    }
}
