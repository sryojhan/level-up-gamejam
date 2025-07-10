using System.Collections.Generic;
using UnityEngine;

public class PersistentData 
{
    public static int connection_id = -1;

    public static HashSet<int> collectedKeys = new();

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
