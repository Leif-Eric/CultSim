using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMethodeHolder : MonoBehaviour
{
    public static GameObject bloodRainObjekt;
    public static Material background;
    public static Color redTint;

    public static void BloodRain()
    {
        bloodRainObjekt.SetActive(true);
        background.color = redTint;
        //Sound?
        Debug.Log("open the gates");
    }
}
