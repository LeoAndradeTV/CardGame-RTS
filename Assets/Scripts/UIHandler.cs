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
    public int materialsPerHarvest = 1;

    [SerializeField] private GameObject cardSelectionMenu;
    [SerializeField] private GameObject materialSelectionMenu;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject cardPurchaseMenu;
    [SerializeField] private GameObject buildingMenu;
    [SerializeField] private GameObject attackMenu;
    [SerializeField] private Button playButton;
    [SerializeField] private Button discardButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button goToTableButton;
    [SerializeField] private Button goToBoardButton;
    [SerializeField] private Button buildButton;
    [SerializeField] private Vector3 cameraOnBoard;
    [SerializeField] private Vector3 cameraOnTable;
    [SerializeField] private Quaternion cameraOnBoardRotation;
    [SerializeField] private Quaternion cameraOnTableRotation;
    [SerializeField] private new Camera camera;

    public TMP_Text woodCounterText;
    public TMP_Text rockCounterText;
    public TMP_Text stringCounterText;
    public TMP_Text ironCounterText;
    public Vector3 lastCameraPositionOnTable;

    // Start is called before the first frame update
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        SetBuildButton(false);
        HideAllMenus();
        lastCameraPositionOnTable = cameraOnTable;
    }

    public IEnumerator HideAllMenusCoroutine()
    {
        yield return new WaitForEndOfFrame();
        allMenusAreClosed = true;
        backButton.gameObject.SetActive(!allMenusAreClosed);
        cardSelectionMenu.SetActive(!allMenusAreClosed);
        cardPurchaseMenu.SetActive(!allMenusAreClosed);
        buildingMenu.SetActive(!allMenusAreClosed);
        attackMenu.SetActive(!allMenusAreClosed);
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
    private IEnumerator ShowAttackMenuCoroutine()
    {
        yield return new WaitForEndOfFrame();
        attackMenu.SetActive(true);
        allMenusAreClosed = false;
    }
    public void ShowAttackMenu()
    {
        StartCoroutine(ShowAttackMenuCoroutine());
    }
    public void OpenMaterialMenu(int materialPerHarvest)
    {
        StartCoroutine(ShowMaterialMenu());
        materialsPerHarvest = materialPerHarvest;
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
        buyButton.onClick.AddListener(delegate { BuyButtonClicked(card, PlayerStats.Instance.GoldAmount); });
        StartCoroutine(ShowBuyMenuCoroutine());

    }
    public void PlayButtonClicked(Card card)
    {
        PlayerCards.instance.DiscardCard(card);
        card.Play();    
        SetBuildButton(PlayerCards.instance.GetCardsInHand() == 0);
    }
    public void DiscardButtonClicked(Card card)
    {
        PlayerCards.instance.DiscardCard(card);
        if (PlayerCards.instance.cardsInHand.Count == 0)
        {
            buildButton.gameObject.SetActive(true);
        }
        HideAllMenus();
    }
    public void HarvestWood()
    {
        MaterialCounter.WoodCounter += materialsPerHarvest;
        CheckIfDoneHarvesting();
    }
    public void HarvestRock()
    {
        MaterialCounter.RockCounter += materialsPerHarvest;
        CheckIfDoneHarvesting();
    }
    public void HarvestString()
    {
        MaterialCounter.StringCounter += materialsPerHarvest;
        CheckIfDoneHarvesting();
    }
    public void HarvestIron()
    {
        MaterialCounter.IronCounter += materialsPerHarvest;
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

        PlayerStats.Instance.GoldAmount -= card.price;
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
    public void BuildButtonClicked()
    {
        buildingMenu.SetActive(true);
    }
    private IEnumerator LerpCamera(Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot, bool moveCamera)
    {
        float difference = 0;
        while (difference < 1)
        {
            camera.transform.position = Vector3.Lerp(startPos, endPos, difference);
            camera.transform.rotation = Quaternion.Lerp(startRot, endRot, difference);
            difference += Time.deltaTime * 2;
            yield return null;
        }
        camera.transform.position = endPos;
        camera.transform.rotation = endRot;
        CameraController.instance.canMoveCamera = moveCamera;
    }
    public void ChangeToTableView()
    {
        StartCoroutine(LerpCamera(camera.transform.position, lastCameraPositionOnTable, camera.transform.rotation, cameraOnTableRotation, true));
        Actions.ChangeCardInteractable?.Invoke(false);   // Player cards can't be selected

    }
    public void ChangeToBoardView()
    {
        lastCameraPositionOnTable = camera.transform.position;
        StartCoroutine(LerpCamera(camera.transform.position, cameraOnBoard, camera.transform.rotation, cameraOnBoardRotation, false));
        Actions.ChangeCardInteractable?.Invoke(true);    // Player cards can be selected
    }
    public void SetBuildButton(bool set)
    {
        buildButton.gameObject.SetActive(set);
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
