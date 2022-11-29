using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingCounter
{
    private static int bankAmount;
    private static int archersAmount;
    private static int swordsmenAmount;
    private static int siegeAmount;
    private static int wallAmount;

    public static int BankAmount
    {
        get { return bankAmount; }
        set { bankAmount = value; }
    }
    public static int ArchersAmount
    {
        get { return archersAmount; }
        set { archersAmount = value; }
    }
    public static int SwordsmenAmount
    {
        get { return swordsmenAmount; }
        set { swordsmenAmount = value; }
    }
    public static int SiegeAmount
    {
        get { return siegeAmount; }
        set { siegeAmount = value; }
    }
    public static int WallAmount
    {
        get { return wallAmount; }
        set { wallAmount = value; }
    }
}
