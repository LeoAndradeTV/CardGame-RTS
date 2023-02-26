using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Table : MonoBehaviour
{
    public static Table Instance;

    public Button drawButton;
    public Button shuffleButton;

    public Transform[] cardLocations = new Transform[5];
    public bool[] locationIsFilled = new bool[5];

    public int GoldAmount
    {
        get { return goldAmount; }
        set
        {
            goldAmount = value;
            goldText.text = goldAmount.ToString();
        }
    }
    private int goldAmount;
    public TMP_Text goldText;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        drawButton.interactable = false;
    }

    public void DrawCard()
    {
        Actions.OnDrawCardsClicked?.Invoke();
    }

    public void ShuffleDiscardPile()
    {
        Actions.OnShuffleCardsClicked?.Invoke();
    }
}
