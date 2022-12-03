using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions
{
    public static Action OnDrawCardsClicked;
    public static Action OnShuffleCardsClicked;
    public static Action<Card> OnCardPlayedClicked;
    public static Action<Card, int> OnCardBought;
    public static Action<bool> ChangeCardInteractable;
}
