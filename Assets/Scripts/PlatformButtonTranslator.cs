using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlatformButtonTranslator : Singleton<PlatformButtonTranslator>
{
    public enum ControlScheme
    {
        Keyboard, PlayStation, Generic
    }

    ControlScheme currentControlScheme = ControlScheme.Generic;


    public Dictionary<string, string> keyboardDictionary = new();
    public Dictionary<string, string> playstationDictionary = new();
    public Dictionary<string, string> xboxDictionary = new();


    PlayerInput playerInput;


    private void Awake()
    {
        if (DestroyIfInitialised(this)) return;

        EnsureInitialised();
        PopulateDictionaries();

        playerInput = GetComponent<PlayerInput>();
    }



    private void PopulateDictionaries()
    {
        void Add(string key, string keyboard, string playstation, string xbox)
        {
            keyboardDictionary.Add(key, keyboard);
            playstationDictionary.Add(key, playstation);
            xboxDictionary.Add(key, xbox);
        }

        Add("interact", "e", "play_square", "xbox_x");
    }

    public static string TranslateButton(string baseString)
    {
        return instance.currentControlScheme switch
        {
            ControlScheme.Keyboard => instance.keyboardDictionary[baseString],
            ControlScheme.PlayStation => instance.playstationDictionary[baseString],
            ControlScheme.Generic => instance.xboxDictionary[baseString],
            _ => instance.xboxDictionary[baseString]
        };
    }

    public static string ProcessString(string input)
    {
        if (!input.Contains('<')) return input;

        int beginning = input.IndexOf('<');
        int end = input.IndexOf('>');

        if(beginning >= end)
        {
            Debug.LogError("Error with the parsing of the string: " + input);
            return input;
        }

        string textWithBrackets = input[beginning..(end + 1)];
        string bracketsContent = input[(beginning + 1)..(end)];

        string keyButton = TranslateButton(bracketsContent);

        string output = input.Replace(textWithBrackets, keyButton);

        return output;
    }

    string lastControlScheme = "";
    private void Update()
    {
        if (playerInput.currentControlScheme != lastControlScheme) OnControlsChange(playerInput.currentControlScheme);
    }

    private void OnControlsChange(string scheme)
    {
        currentControlScheme = scheme switch
        {
            "Playstation" => ControlScheme.PlayStation,
            "GenericGamepad" => ControlScheme.Generic,
            _ => ControlScheme.Keyboard,
        };

        lastControlScheme = scheme;
    }

    public void OnButtonPressed(InputControl control)
    {
        var device = control.device;

        print(device.ToString());
        print(device.displayName);
        print(device.description);

        if (device is Gamepad)
        {
            string layout = device.description.product?.ToLower();

            if (layout.Contains("dualshock") || layout.Contains("dualsense") || layout.Contains("playstation"))
                currentControlScheme = ControlScheme.PlayStation;
            else if (layout.Contains("xbox") || layout.Contains("xinput"))
                currentControlScheme = ControlScheme.Generic;
            else
                currentControlScheme = ControlScheme.Generic;
        }
        else if (device is Keyboard || device is Mouse)
        {
            currentControlScheme = ControlScheme.Keyboard;
        }

    }

}
