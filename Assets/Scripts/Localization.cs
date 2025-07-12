using System.Collections.Generic;
using UnityEngine;

public class Localization : Singleton<Localization>
{
    protected override bool DestroyOnLoad => false;

    public enum Language
    {
        English, Spanish, Catalonian
    }

    public Language currentLanguage;


    Dictionary<string, string> englishDic = new();
    Dictionary<string, string> spanishDic = new();
    Dictionary<string, string> catalonianDic = new();


    private void Awake()
    {
        if (DestroyIfInitialised(this)) return;

        EnsureInitialised();
        PopulateDictionaries();
    }

    private void PopulateDictionaries()
    {
        void CreateEntry(string id, string english, string spanish, string catalonian)
        {
            englishDic.Add(id, english);
            spanishDic.Add(id, spanish);
            catalonianDic.Add(id, catalonian);
        }


        CreateEntry("debug_0", "Hello im socky", "Hola soy don calcetin", "AJDSAJDJASKDJASDALSD");
        CreateEntry("debug_1", "And I don't know what else to say", "No se que mas decirte la verdad", "AJDSAJDJASKDJASDALSD");
        CreateEntry("debug_2", ".....", ".......", "AJDSAJDJASKDJASDALSD");
        CreateEntry("debug_3", "go ahead and kill the washing machine already", "A que esperas? Ves de una vez a matar a la lavadora", "AJDSAJDJASKDJASDALSD");


        CreateEntry("interact", "Press <interact> to interact", "Pulsa <interact> para interactuar", "Algo en catalan");
        CreateEntry("read", "Press <interact> to read", "Pulsa <interact> para leer", "Algo en catalan");
        CreateEntry("inspect", "Press <interact> to inspect", "Pulsa <interact> para inspeccionar", "Algo en catalan");
        CreateEntry("talk", "Press <interact> to talk", "Pulsa <interact> para hablar", "Algo en catalan");
        CreateEntry("pick_key", "Press <interact> to pick the key", "Pulsa <interact> para coger la llave", "Algo en catalan");
        CreateEntry("pick_coin", "Press <interact> to pick the coin", "Pulsa <interact> para coger la moneda", "Algo en catalan");
        CreateEntry("pick_laundry", "Press <interact> to pick up the laundry", "Pulsa <interact> para recoger la colada", "Algo en catalan");
        CreateEntry("open_door", "Press <interact> to open the door", "Pulsa <interact> para abrir la puerta", "Algo en catalan");
        CreateEntry("open_chest", "Press <interact> to open the chest", "Pulsa <interact> para abrir el cofre", "Algo en catalan");
    }

    public static string GetText(string id)
    {
        if (!instance.englishDic.ContainsKey(id))
        {
            Debug.LogError("This key has not been initialised: " + id);
            return id;
        }

        string translation = instance.currentLanguage switch
        {
            Language.English => instance.englishDic[id],
            Language.Spanish => instance.spanishDic[id],
            Language.Catalonian => instance.catalonianDic[id],
            _ => id,
        };

        return PlatformButtonTranslator.ProcessString(translation); ;
    }

}
