using EasyButtons;
using System;
using UnityEngine;

public class UID : MonoBehaviour
{
    public string uid = "";

    private void Reset()
    {
        if (string.IsNullOrEmpty(uid))
        {
            uid = Guid.NewGuid().ToString();
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(uid))
        {
            uid = Guid.NewGuid().ToString();
        }
    }

#endif
}
