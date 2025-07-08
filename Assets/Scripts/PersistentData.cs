using System.Collections.Generic;
using UnityEngine;

public class PersistentData 
{
    public static int connection_id = -1;


    private static readonly Dictionary<string, int> dictionaryNPC = new();

    public static void SetNPC(string uid, int value)
    {
        dictionaryNPC[uid] = value;
    }

    public static int GetNPC(string uid)
    {
        if (!dictionaryNPC.ContainsKey(uid)) return 0;

        return dictionaryNPC[uid];
    }

}
