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


        CreateEntry("interact", "Press E to interact", "Pulsa la E para interactuar", "Algo en catalan");
        CreateEntry("read", "Press E to read", "Pulsa la E para leer", "Algo en catalan");
        CreateEntry("inspect", "Press E to inspect", "Pulsa la E para inspeccionar", "Algo en catalan");
        CreateEntry("talk", "Press E to talk", "Pulsa la E para hablar", "Algo en catalan");
        CreateEntry("pick_key", "Press E to pick the key", "Pulsa la E para coger la llave", "Algo en catalan");
        CreateEntry("pick_coin", "Press E to pick the coin", "Pulsa la E para coger la moneda", "Algo en catalan");
    }

    public static string GetText(string id)
    {
        if (!instance.englishDic.ContainsKey(id))
        {
            Debug.LogError("This key has not been initialised: " + id);
            return id;
        }

        return instance.currentLanguage switch
        {
            Language.English => instance.englishDic[id],
            Language.Spanish => instance.spanishDic[id],
            Language.Catalonian => instance.catalonianDic[id],
            _ => id,
        };
    }

}
