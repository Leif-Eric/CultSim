using UnityEngine;
using TMPro;

public class RessourceInfoComponent : MonoBehaviour
{
    public TextMeshProUGUI WorkerTxt, FaithTxt, MenaceTxt, Moneytxt;

    public void UpdateInfos(int worker, int faith, int menace, int money)
    {
        WorkerTxt.text = worker.ToString();
        FaithTxt.text = faith.ToString();
        MenaceTxt.text = menace.ToString();
        Moneytxt.text = money.ToString();
    }
}
