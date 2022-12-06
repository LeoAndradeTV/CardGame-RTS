using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //FOR TESTING ONLY
        PlayerStats.Instance.GoldAmount = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTurn()
    {
        PlayerStats.Instance.GoldAmount += BuildingCounter.BankAmount * 3;
        PlayerCards.instance.DrawCards();
    }
}
