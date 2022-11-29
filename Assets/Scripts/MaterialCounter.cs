using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MaterialCounter
{
    public static int WoodCounter
    {
        get { return woodCounter; }
        set 
        { 
            woodCounter = value;
            UIHandler.instance.woodCounterText.text = $"Wood: {woodCounter}";
        }
    
   }
    public static int RockCounter
    {
        get { return rockCounter; }
        set
        {
            rockCounter = value;
            UIHandler.instance.rockCounterText.text = $"Rock: {rockCounter}";
        }

    }
    public static int StringCounter
    {
        get { return stringCounter; }
        set
        {
            stringCounter = value;
            UIHandler.instance.stringCounterText.text = $"String: {stringCounter}";
        }

    }
    public static int IronCounter
    {
        get { return ironCounter; }
        set
        {
            ironCounter = value;
            UIHandler.instance.ironCounterText.text = $"Iron: {ironCounter}";
        }

    }

    private static int woodCounter;
    private static int rockCounter;
    private static int stringCounter;
    private static int ironCounter;
}
