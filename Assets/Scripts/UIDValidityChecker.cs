using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDValidityChecker : Singleton<UIDValidityChecker>
{
#if UNITY_EDITOR


    protected override bool DestroyOnLoad => false;

    private UID[] GetAllUIDs()
    {
        return FindObjectsByType<UID>(FindObjectsSortMode.None);
    }

    [EasyButtons.Button]
    public void RegenerateUIDs()
    {
        foreach (UID uid in GetAllUIDs())
        {
            uid.RegenerateUID();
        }
    }

    public bool CheckUIDsValidity()
    {
        HashSet<string> allUIDs = new();

        foreach (UID uid in GetAllUIDs())
        {
            if (!allUIDs.Add(uid.uid))
            {
                return false;
            }
        }

        return true;
    }

    public void RegenerateIfInvalidUIDs()
    {
        if (CheckUIDsValidity()) RegenerateUIDs();
    }

    private float lastCheck = 0;

    private void OnDrawGizmos()
    {
        if (Application.isPlaying) return;

        if (Time.realtimeSinceStartup - lastCheck < 10) return;

        lastCheck = Time.realtimeSinceStartup;

        RegenerateIfInvalidUIDs();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoadedCallback;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedCallback;
    }

    private void OnSceneLoadedCallback(Scene scene, LoadSceneMode mode)
    {
        if (CheckUIDsValidity())
        {

            Debug.LogError($"Error on scene {scene.name}. Duplication of UID");
        }
    }

#endif

}
