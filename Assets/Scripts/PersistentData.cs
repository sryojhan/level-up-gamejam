using System.Collections.Generic;
using UnityEngine;

public class PersistentData 
{
    public static int connection_id = -1;
    public static int lastSceneConnection_id = -1;


    //Player stats
    public static int maxHealth = -1;
    public static int currentHealth = -1;
    public static int currentHealthOnSceneLoad = -1;


    public static int maxSockCount = -1;
    public static int currentSockCount = -1;



    //Laundry
    public static bool[] collectedLaundry = new []{ false, false, false };


    //Keys 
    public static HashSet<int> collectedKeys = new();


    // Entities

    private static readonly Dictionary<string, int> objectPersistentState = new();

    public static void Set(string uid, int value)
    {
        objectPersistentState[uid] = value;
    }

    public static int Get(string uid)
    {
        if (!objectPersistentState.ContainsKey(uid)) return 0;

        return objectPersistentState[uid];
    }

}
