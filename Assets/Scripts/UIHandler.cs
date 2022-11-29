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
    [SerializeField] private GameObject cardPurchaseMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button discardButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button goToTableButton;
    [SerializeField] private Button goToBoardButton;
    [SerializeField] private Vector3 cameraOnBoard;
    [SerializeField] private Vector3 cameraOnTable;
    [SerializeField] private Camera camera;

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
        cardPurchaseMenu.SetActive(!allMenusAreClosed);
        playerHUD.SetActive(allMenusAreClosed);
    }
    public void HideAllMenus()
    {
        StartCoroutine(HideAllMenusCoroutine());
    }
    private IEnumerator ShowCardMenuCoroutine()
    {
        yield return new WaitForEndOfFrame();
        allMenusAreClosed = false;
        backButton.gameObject.SetActive(!allMenusAreClosed);
        cardSelectionMenu.SetActive(!allMenusAreClosed);
        playerHUD.SetActive(allMenusAreClosed);
    }
    private IEnumerator ShowBuyMenuCoroutine()
    {
        yield return new WaitForEndOfFrame();
        allMenusAreClosed = false;
        cardPurchaseMenu.SetActive(!allMenusAreClosed);
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
        StartCoroutine(ShowCardMenuCoroutine());
    }

    public void ShowBuyMenu(Card card)
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(delegate { BuyButtonClicked(card, Table.Instance.GoldAmount); });
        StartCoroutine(ShowBuyMenuCoroutine());

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
    private void BuyButtonBehaviour(Card card, int goldAmount)
    {
        if (goldAmount < card.price)
        {
            Debug.Log("Not enough gold");
            return;
        }
        card.currentData.CardStatus = CardStatus.Bought;
        PlayerCards.instance.AddCardToDiscardFromBank(card);
        CardBank.instance.cardsOnCardBank.Remove(card);
        CardBank.instance.locationIsFilled[card.indexInHand] = false;
        CardBank.instance.PlaceCardToBuy();
        Destroy(card.gameObject);
        HideAllMenus();

    }
    private void BuyButtonClicked(Card card, int goldAmount)
    {
        Actions.OnCardBought?.Invoke(card, goldAmount);
    }
    private IEnumerator LerpCamera(Vector3 startPos, Vector3 endPos)
    {
        float difference = 0;
        while (difference < 1)
        {
            camera.transform.position = Vector3.Lerp(startPos, endPos, difference);
            difference += Time.deltaTime * 2;
            yield return null;
        }
        camera.transform.position = endPos;
    }
    public void ChangeToTableView()
    {
        StopCoroutine(LerpCamera(camera.transform.position, cameraOnBoard));
        StartCoroutine(LerpCamera(camera.transform.position, cameraOnTable));
    }
    public void ChangeToBoardView()
    {
        StopCoroutine(LerpCamera(camera.transform.position, cameraOnTable));
        StartCoroutine(LerpCamera(camera.transform.position, cameraOnBoard));
    }
    private void OnEnable()
    {
        Actions.OnCardBought += BuyButtonBehaviour;
    }
    private void OnDisable()
    {
        Actions.OnCardBought -= BuyButtonBehaviour;
    }
}
