using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttackMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject bgImage;
    [SerializeField] private GameObject prompt;
    [SerializeField] private GameObject unitSelectionArea;
    [SerializeField] private Slider archerSlider;
    [SerializeField] private Slider swordsmenSlider;
    [SerializeField] private Slider siegesSlider;
    [SerializeField] private TMP_Text archersCount;
    [SerializeField] private TMP_Text swordsmenCount;
    [SerializeField] private TMP_Text siegesCount;
    [SerializeField] private Button attackButton;
    [SerializeField] private AttackUnitsSpawner attackUnitsSpawner;

    private int archersCommitted;
    private int swordsmenCommitted;
    private int siegesCommitted;

    // Start is called before the first frame update
    void Awake()
    {
        archerSlider.maxValue = BuildingCounter.ArchersAmount * 3f;
        swordsmenSlider.maxValue = BuildingCounter.SwordsmenAmount * 5f;
        siegesSlider.maxValue = BuildingCounter.SiegeAmount;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUnitCount();
    }

    public void OnAttackClicked()
    {
        archersCommitted = Mathf.RoundToInt(archerSlider.value);
        swordsmenCommitted = Mathf.RoundToInt(swordsmenSlider.value);
        siegesCommitted = Mathf.RoundToInt(siegesSlider.value);
        UIHandler.instance.HideAllMenus();
        UIHandler.instance.ChangeToTableView();
        attackUnitsSpawner.SpawnUnits(archersCommitted, swordsmenCommitted, siegesCommitted);
    }
    private void UpdateUnitCount()
    {
        archersCount.text = archerSlider.value.ToString();
        swordsmenCount.text = swordsmenSlider.value.ToString();
        siegesCount.text = siegesSlider.value.ToString();
    }
}
