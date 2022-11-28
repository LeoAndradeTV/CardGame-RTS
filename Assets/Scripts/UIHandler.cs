using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    public bool allMenusAreClosed = true;
    public int harvests;

    [SerializeField] private GameObject cardSelectionMenu;
    [SerializeField] private GameObject materialSelectionMenu;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private Button playButton;
    [SerializeField] private Button discardButton;
    [SerializeField] private Button backButton;
    public TMP_Text woodCounterText; 
    public TMP_Text rockCounterText; 
    public TMP_Text stringCounterText; 
    public TMP_Text ironCounterText;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        HideAllMenus();
    }

    public IEnumerator HideAllMenusCoroutine()
    {  
        yield return new WaitForEndOfFrame();
        allMenusAreClosed = true;
        backButton.gameObject.SetActive(!allMenusAreClosed);
        cardSelectionMenu.SetActive(!allMenusAreClosed);
        playerHUD.SetActive(allMenusAreClosed);
    }
    public void HideAllMenus()
    {
        StartCoroutine(HideAllMenusCoroutine());
    }
    private IEnumerator ShowCardMenu()
    { 
        yield return new WaitForEndOfFrame();
        allMenusAreClosed = false;
        backButton.gameObject.SetActive(!allMenusAreClosed);
        cardSelectionMenu.SetActive(!allMenusAreClosed);
        playerHUD.SetActive(allMenusAreClosed);
    }
    private IEnumerator ShowMaterialMenu()
    {
        yield return new WaitForEndOfFrame();
        materialSelectionMenu.SetActive(true);
        allMenusAreClosed = false;
    }
    public void OpenMaterialMenu()
    {
        StartCoroutine(ShowMaterialMenu());
    }
    public void ShowAndSetCardPlayMenu(Card card)
    {
        playButton.onClick.RemoveAllListeners();
        discardButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(delegate { PlayButtonClicked(card); });
        discardButton.onClick.AddListener(delegate { DiscardButtonClicked(card); });
        StartCoroutine(ShowCardMenu());
    }
    public void PlayButtonClicked(Card card)
    {
        card.Play();
        PlayerCards.instance.DiscardCard(card);
    }
    public void DiscardButtonClicked(Card card)
    {
        PlayerCards.instance.DiscardCard(card);
        HideAllMenus();
    }
    public void HarvestWood()
    {
        MaterialCounter.WoodCounter += 1;
        CheckIfDoneHarvesting();
    }
    public void HarvestRock()
    {
        MaterialCounter.RockCounter += 1;
        CheckIfDoneHarvesting();
    }
    public void HarvestString()
    {
        MaterialCounter.StringCounter += 1;
        CheckIfDoneHarvesting();
    }
    public void HarvestIron()
    {
        MaterialCounter.IronCounter += 1;
        CheckIfDoneHarvesting();
    }
    private void CheckIfDoneHarvesting()
    {
        harvests--;
        if (harvests == 0) 
        { 
            materialSelectionMenu.SetActive(false);
            allMenusAreClosed = true;
        }
    }
}
