using System;
using UnityEngine;

public class UID : MonoBehaviour
{
    public string uid = "";

    private void Reset()
    {
        if (string.IsNullOrEmpty(uid))
        {
            RegenerateUID();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(!Application.isPlaying)
            RegenerateUID();
    }


#endif

    [EasyButtons.Button]
    public void RegenerateUID()
    {
        uid = Guid.NewGuid().ToString();
    }
}
