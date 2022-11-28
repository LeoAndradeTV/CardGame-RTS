using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Data", menuName = "Card Data")]
public class CardData : ScriptableObject
{
    public CardType CardType;
    public new string name;
    public string description;
    public int price;
}
