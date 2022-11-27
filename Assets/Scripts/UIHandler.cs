using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;

    public bool cardMenuIsOpen;

    [SerializeField] private Image menuBackground;
    [SerializeField] private GameObject cardButtons;
    [SerializeField] private Button playButton;
    [SerializeField] private Button discardButton;
    [SerializeField] private Button backButton;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        StartCoroutine(HideAllMenus());
    }

    public IEnumerator HideAllMenus()
    {
        cardMenuIsOpen = false;
        yield return null;
        menuBackground.gameObject.SetActive(cardMenuIsOpen);
        backButton.gameObject.SetActive(cardMenuIsOpen);
        cardButtons.SetActive(cardMenuIsOpen);
    }

    private IEnumerator ShowCardMenu()
    {
        cardMenuIsOpen = true;
        yield return null;
        menuBackground.gameObject.SetActive(cardMenuIsOpen);
        backButton.gameObject.SetActive(cardMenuIsOpen);
        cardButtons.SetActive(cardMenuIsOpen);
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
        StartCoroutine(HideAllMenus());

    }

    public void DiscardButtonClicked(Card card)
    {
        PlayerCards.instance.DiscardCard(card);
        StartCoroutine(HideAllMenus());
    }

    public void BackButtonClicked()
    {
        StartCoroutine(HideAllMenus());
    }

}
