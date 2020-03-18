using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Upgrade", order = 1)]
public class Upgrade : ScriptableObject
{
    [SerializeField]
    public enum CostType { ufaith,money}

    [HideInInspector]
    public int id;

    public string upgradeName;

    public UnityEvent method;
    public float[] attributes;

    public CostType costType;
    public float cost;

    public string description;

    


    public void BuyUpgrade()
    {
        method.Invoke();
    }
    






}

