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

    [Header("Menus")]
    [SerializeField] private GameObject cardSelectionMenu;
    [SerializeField] private GameObject materialSelectionMenu;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject cardPurchaseMenu;
    [SerializeField] private GameObject buildingMenu;
    [SerializeField] private GameObject attackMenu;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button discardButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button goToTableButton;
    [SerializeField] private Button goToBoardButton;
    [SerializeField] private Button buildButton;
    [SerializeField] private Button woodButton;
    [SerializeField] private Button rockButton;
    [SerializeField] private Button ironButton;
    [SerializeField] private Button stringButton;

    [Header("Camera Properties")]
    [SerializeField] private Vector3 cameraOnBoard;
    [SerializeField] private Vector3 cameraOnTable;
    [SerializeField] private Quaternion cameraOnBoardRotation;
    [SerializeField] private Quaternion cameraOnTableRotation;
    [SerializeField] private new Camera camera;
    public Vector3 lastCameraPositionOnTable;

    [Header("Text Properties")]
    public TMP_Text woodCounterText;
    public TMP_Text rockCounterText;
    public TMP_Text stringCounterText;
    public TMP_Text ironCounterText;

    [Header("Images")]
    [SerializeField] private Transform cardInPlayMenuSpawn;
    [SerializeField] private Transform cardInBuyMenuSpawn;



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

    public void HideAllMenus()
    {
        StartCoroutine(HideAllMenusCoroutine());
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
    private IEnumerator ShowCardMenuCoroutine()
    {
        yield return new WaitForEndOfFrame();
        allMenusAreClosed = false;
        backButton.gameObject.SetActive(!allMenusAreClosed);
        cardSelectionMenu.SetActive(!allMenusAreClosed);
        playerHUD.SetActive(allMenusAreClosed);
    }
    public void ShowBuyMenu(Card card)
    {
        if (cardInBuyMenuSpawn.gameObject.transform.childCount > 0)
        {
            Destroy(cardInBuyMenuSpawn.gameObject.transform.GetChild(0).gameObject);
        }
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(delegate { BuyButtonClicked(card, PlayerStats.Instance.GoldAmount); });
        StartCoroutine(ShowBuyMenuCoroutine());
        var cardInMenu = Instantiate(card, cardInBuyMenuSpawn);
        SetUpCardInMenu(cardInMenu);

    }
    private IEnumerator ShowBuyMenuCoroutine()
    {
        yield return new WaitForEndOfFrame();
        allMenusAreClosed = false;
        cardPurchaseMenu.SetActive(!allMenusAreClosed);
        playerHUD.SetActive(allMenusAreClosed);
    }
    private IEnumerator ShowAndSetMaterialMenu()
    {
        yield return new WaitForEndOfFrame();
        woodButton.onClick.AddListener(GameManager.instance.HarvestWood);
        rockButton.onClick.AddListener(GameManager.instance.HarvestRock);
        ironButton.onClick.AddListener(GameManager.instance.HarvestIron);
        stringButton.onClick.AddListener(GameManager.instance.HarvestString);
        materialSelectionMenu.SetActive(true);
        allMenusAreClosed = false;
    }
    public void ShowAttackMenu()
    {
        StartCoroutine(ShowAttackMenuCoroutine());
    }
    private IEnumerator ShowAttackMenuCoroutine()
    {
        yield return new WaitForEndOfFrame();
        attackMenu.SetActive(true);
        allMenusAreClosed = false;
    }
    public void OpenMaterialMenu(int materialPerHarvest)
    {
        StartCoroutine(ShowAndSetMaterialMenu());
        GameManager.instance.materialsPerHarvest = materialPerHarvest;
    }
    public void ShowAndSetCardPlayMenu(Card card)
    {
        //Destroy any previous instantiated card
        if (cardInPlayMenuSpawn.gameObject.transform.childCount > 0)
        {
            Destroy(cardInPlayMenuSpawn.gameObject.transform.GetChild(0).gameObject);
        }

        playButton.onClick.RemoveAllListeners();
        discardButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => PlayButtonClicked(card));
        discardButton.onClick.AddListener(() => DiscardButtonClicked(card));
        StartCoroutine(ShowCardMenuCoroutine());
        var cardInMenu = Instantiate(card, cardInPlayMenuSpawn);
        SetUpCardInMenu(cardInMenu);
    }

    private static void SetUpCardInMenu(Card cardInMenu)
    {
        cardInMenu.transform.localScale *= 5;
        cardInMenu.transform.localRotation = Quaternion.Euler(Vector3.zero);
        cardInMenu.transform.localPosition = Vector3.zero;
    }

    public void PlayButtonClicked(Card card)
    {
        Actions.OnCardPlayedClicked?.Invoke(card);
        card.Play();
        
    }
    public void DiscardButtonClicked(Card card)
    {
        Actions.OnCardDiscardClicked?.Invoke(card); 
        HideAllMenus();
    }
    public void CheckIfDoneHarvesting()
    {
        harvests--;
        if (harvests == 0)
        {
            materialSelectionMenu.SetActive(false);
            allMenusAreClosed = true;
            woodButton.onClick.RemoveAllListeners();
            rockButton.onClick.RemoveAllListeners();
            ironButton.onClick.RemoveAllListeners();
            stringButton.onClick.RemoveAllListeners();
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
        HideAllMenus();

    }
    private void BuyButtonClicked(Card card, int goldAmount)
    {
        Actions.OnCardBought?.Invoke(card, goldAmount);
    }
    public void SetBuildButton(bool set)
    {
        buildButton.gameObject.SetActive(set);
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
    
    private void OnEnable()
    {
        Actions.OnCardBought += BuyButtonBehaviour;
    }
    private void OnDisable()
    {
        Actions.OnCardBought -= BuyButtonBehaviour;
    }
}
