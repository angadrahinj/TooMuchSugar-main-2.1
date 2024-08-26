using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int collectedMilkBottles = 0;
    public int totalMilkBottleAmount = 15;

    public TextMeshProUGUI collectedAmountUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateMilkBottleUI();
    }

    public void CollectMilkBottle()
    {
        collectedMilkBottles += 1;
        UpdateMilkBottleUI();
    }

    public void UpdateMilkBottleUI()
    {
        collectedAmountUI.text = collectedMilkBottles + "/" + totalMilkBottleAmount;
        // Debug.Log(collectedMilkBottles + "/" + totalMilkBottleAmount);
    }
}
