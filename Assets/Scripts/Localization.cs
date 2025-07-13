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


        CreateEntry("interact", "Press <interact> to interact", "Pulsa <interact> para interactuar", "Prem <interact> per interactuar");
        CreateEntry("read", "Press <interact> to read", "Pulsa <interact> para leer", "Prem <interact> per llegir");
        CreateEntry("inspect", "Press <interact> to inspect", "Pulsa <interact> para inspeccionar", "Prem <interact> per inspeccionar");
        CreateEntry("talk", "Press <interact> to talk", "Pulsa <interact> para hablar", "Prem <interact> per parlar");
        CreateEntry("pick_key", "Press <interact> to pick the key", "Pulsa <interact> para coger la llave", "Prem <interact> per agafar la clau");
        CreateEntry("pick_coin", "Press <interact> to pick the coin", "Pulsa <interact> para coger la moneda", "Prem <interact> per agafar la moneda");
        CreateEntry("pick_laundry", "Press <interact> to pick up the laundry", "Pulsa <interact> para recoger la colada", "Prem <interact> per recollir la bugada");
        CreateEntry("open_door", "Press <interact> to open the door", "Pulsa <interact> para abrir la puerta", "Prem <interact> per obrir la porta");
        CreateEntry("open_chest", "Press <interact> to open the chest", "Pulsa <interact> para abrir el cofre", "Prem <interact> per obrir el cofre");



        CreateEntry("enter_washing_machine_room_confirm",
    "Are you sure you want to go into the washing machine room? It is not possible to go back.",
    "�Est�s seguro de que quieres entrar en la sala de la lavadora? No es posible volver atr�s.",
    "Est�s segur que vols entrar a la sala de la rentadora? No �s possible tornar enrere.");



        CreateEntry("door_cannot_open",
    "Hmmm... It doesn't seem it can be opened right now.",
    "Hmm... Parece que no se puede abrir ahora mismo.",
    "Hmm... Sembla que no es pot obrir ara mateix.");


        CreateEntry("be_back_soon",
    "I'll be back in 20 minutes. When I'm back you'd better have finished doing the laundry.",
    "Volver� en 20 minutos. Cuando vuelva, ser� mejor que hayas terminado de hacer la colada.",
    "Tornar� en 20 minuts. Quan torni, m�s val que hagis acabat de fer la bugada.");


        CreateEntry("ok_question",
    "Ok?",
    "�Vale?",
    "D�acord?");



        //Items

        CreateEntry("heart_item_obtained",
    "You have obtained an extra heart",
    "Has obtenido un coraz�n extra",
    "Has obtingut un cor extra");

        CreateEntry("heal_item_obtained",
    "You feel rejuvenated! You have recovered a lost heart",
    "�Te sientes rejuvenecido! Has recuperado un coraz�n perdido",
    "Et sents rejovenit! Has recuperat un cor perdut");


        CreateEntry("damage_item_obtained",
    "Wow! You found a stone. So cool! You can place it inside the pillow to deal more damage. I guess?",
    "��pico! Has encontrado una piedra. �Qu� guay! Puedes meterla dentro de la almohada para hacer m�s da�o. �Supongo?",
    "Uau! Has trobat una pedra. Que guai! Pots posar-la dins del coix� per fer m�s mal. Suposo?");


        CreateEntry("speed_item_obtained",
    "Are those new running shoes? Man... You're going to be faster than ever!",
    "�Son zapatillas nuevas para correr? �Vas a ser m�s r�pido que nunca!",
    "S�n sabatilles noves per c�rrer? Ser�s m�s r�pid que mai!");



        CreateEntry("sock_found",
    "You found some stiff socks. Hmmm... yeah... I guess you can use them to throw at enemies.",
    "Has encontrado unos calcetines duros. Hmm... s�... Supongo que puedes usarlos para lanz�rselos a los enemigos.",
    "Has trobat uns mitjons durs. Hmm... s�... Suposo que els pots fer servir per llen�ar-los als enemics.");



        //Closet
        CreateEntry("closet_occupied",
            "Occupied!",
            "�Ocupado!",
            "Ocupat!");

        CreateEntry("closet_occupied_2",
            "I said it's occupied! Just a minute!",
            "�He dicho que est� ocupado! �Un minuto!",
            "He dit que est� ocupat! Un minut!");

        CreateEntry("closet_angry",
            "MAN GET OUT OF HERE",
            "�T�O, SAL DE AQU�!",
            "TIO, SURT D'AQU�!");



        //Belton

        CreateEntry("belton_bad_guy",
    "I don't like to be a bad guy, you know?",
    "No me gusta ser el malo, �sabes?",
    "No m�agrada ser el dolent, saps?");

        CreateEntry("belton_have_to_do",
    "But you have to do what you have to do",
    "Pero tienes que hacer lo que tienes que hacer",
    "Per� has de fer el que has de fer");

        CreateEntry("belton_chinese_uncle",
    "It's just like my chinese uncle used to say",
    "Es como sol�a decir mi t�o chino",
    "�s com deia el meu oncle xin�s");


        //Oppenheimer
        CreateEntry("oppenheimer_intro",
    "Hello, it's me, J. Robert Oppenheimer. I've been turned into a cow.",
    "Hola, soy J. Robert Oppenheimer. Me han convertido en una vaca.",
    "Hola, s�c J. Robert Oppenheimer. M'han convertit en una vaca.");

        CreateEntry("oppenheimer_fixing",
    "I am currently working on fixing this... peculiar... situation.",
    "Actualmente estoy trabajando para arreglar esta... peculiar... situaci�n.",
    "Actualment estic treballant per arreglar aquesta... peculiar... situaci�.");

        CreateEntry("oppenheimer_moo",
    "Moo.....",
    "Muu.....",
    "Muuu.....");


        CreateEntry("oppenheimer_leave_me_alone",
    "Leave me alone....",
    "D�jame en paz....",
    "Deixa'm en pau....");


        //Jane
        CreateEntry("jane_dialogue_1",
    "H- Hello",
    "H- Hola",
    "H- Hola");

        CreateEntry("jane_dialogue_2",
    "..................",
    "..................",
    "..................");

        CreateEntry("jane_dialogue_3",
    "It's been cool talking to you!",
    "�Ha estado guay hablar contigo!",
    "Ha estat guai parlar amb tu!");

        CreateEntry("jane_dialogue_4",
    "I don't know how I got here",
    "No s� c�mo llegu� aqu�",
    "No s� com he arribat aqu�");

        CreateEntry("jane_dialogue_5",
    "But I'm too scared of the dark to try to leave on my own",
    "Pero tengo demasiado miedo a la oscuridad para intentar irme sola",
    "Per� em fa massa por la foscor per intentar marxar sola");

        CreateEntry("jane_dialogue_6",
    "W- What? Would you really walk me out? (AAAAAAAAAAA)",
    "�Q- Qu�? �De verdad me acompa�ar�as fuera? (AAAAAAAAAAA)",
    "Q- Qu�? De deb� m�acompanyaries a fora? (AAAAAAAAAAA)");

        CreateEntry("jane_dialogue_7",
    "No- no, don't worry. I don't want to bother you. I'll just wait here. I'm sure they are looking for me anyways... They're just taking longer this time... That's all...",
    "N-no, no te preocupes. No quiero molestarte. Mejor espero aqu�. Seguro que me est�n buscando igualmente... Solo est�n tardando m�s esta vez... Eso es todo...",
    "N-no, no et preocupis. No et vull molestar. Millor espero aqu�. Segur que m�estan buscant igualment... Nom�s tarden m�s aquesta vegada... Ja est�...");

        CreateEntry("jane_dialogue_8",
    "....",
    "....",
    "....");


        //Sockrates
        CreateEntry("sockrates_dialogue_1",
    "Oh man... Oh man...",
    "Oh t�o... Oh t�o...",
    "Oh noi... Oh noi...");

        CreateEntry("sockrates_dialogue_2",
    "Do you happen to know anything about what happened in the kitchen??!",
    "�Sabes algo de lo que pas� en la cocina??!",
    "Saps alguna cosa del que va passar a la cuina??!");

        CreateEntry("sockrates_dialogue_3",
    "Sorry. I'm just a little bit nervous....",
    "Perd�n. Es que estoy un poco nervioso....",
    "Perd�. �s que estic una mica nervi�s....");

        CreateEntry("sockrates_dialogue_4",
    "I haven't seen my brother Socky in about 3 days. All since the 'Washing Machine Incident'",
    "No he visto a mi hermano Socky desde hace unos 3 d�as. Todo desde el 'Incidente de la Lavadora'",
    "No he vist el meu germ� Socky des de fa uns 3 dies. Tot des de l�'Incident de la Rentadora'");

        CreateEntry("sockrates_dialogue_5",
    "You seem like a brave adventurer, will you lend me a hand?",
    "Pareces un aventurero valiente, �me echar�s una mano?",
    "Sembles un aventurer valent, m�ajudar�s?");

        CreateEntry("sockrates_dialogue_6",
    "To defeat the Washing Machine, you will need to look for the three 'Laundry baskets' all around the house. It will be hard, but I know I can count on you. You watch out for the dirty clothes and everything will be alright",
    "Para derrotar a la lavadora, tendr�s que buscar las tres 'cestas de ropa sucia' por toda la casa. Ser� dif�cil, pero s� que puedo contar contigo. Vigila la ropa sucia y todo saldr� bien",
    "Per derrotar la rentadora, haur�s de buscar les tres 'cistelles de roba bruta' per tota la casa. Ser� dif�cil, per� s� que puc comptar amb tu. Vigila la roba bruta i tot anir� b�");

        CreateEntry("sockrates_dialogue_7",
    "Good luck",
    "Buena suerte",
    "Bona sort");


        CreateEntry("sockrates_dialogue_8",
    "Any progress on the mission so far?",
    "�Alg�n progreso en la misi�n hasta ahora?",
    "Has fet algun progr�s a la missi� fins ara?");

        CreateEntry("sockrates_dialogue_9",
    "Remember, to defeat the Washing Machine you will need to look for the three 'Laundry baskets' all around the house. It will be hard, but I know I can count on you.",
    "Recuerda, para derrotar a la lavadora tendr�s que buscar las tres 'cestas de ropa sucia' por toda la casa. Ser� dif�cil, pero s� que puedo contar contigo.",
    "Recorda, per derrotar la rentadora haur�s de buscar les tres 'cistelles de roba bruta' per tota la casa. Ser� dif�cil, per� s� que puc comptar amb tu.");


        CreateEntry("sockrates_kitchen_1",
    "Oh! Hey man",
    "�Oh! Hola, t�o",
    "Oh! Ei, noi");


        CreateEntry("sockrates_kitchen_2",
    "I've tried to help but I have not been able to locate all the 'Laundry Baskets'",
    "He intentado ayudar, pero no he podido encontrar todas las 'cestas de ropa sucia'",
    "He intentat ajudar, per� no he pogut trobar totes les 'cistelles de roba bruta'");

        CreateEntry("sockrates_kitchen_3",
    "We are next to the door to the washing machine. Legends say that when you are able to find all the baskets, the door to the washing machine will open",
    "Estamos junto a la puerta de la lavadora. Dicen las leyendas que cuando encuentres todas las cestas, la puerta de la lavadora se abrir�",
    "Som al costat de la porta de la rentadora. Les llegendes diuen que quan trobis totes les cistelles, la porta de la rentadora s�obrir�");


        CreateEntry("sockrates_kitchen_4",
    "I'm sorry, I'm not in the mood to talk right now...",
    "Lo siento, no tengo ganas de hablar ahora...",
    "Ho sento, no tinc ganes de parlar ara mateix...");

        CreateEntry("sockrates_kitchen_5",
    "To think that I've come so far but I can't continue because I still need to find the three laundry baskets....",
    "Pensar que he llegado tan lejos y no puedo continuar porque todav�a necesito encontrar las tres cestas de ropa sucia....",
    "Pensar que he arribat tan lluny i no puc continuar perqu� encara he de trobar les tres cistelles de roba bruta....");




        //MCClain

        CreateEntry("mcclain_lost_salon",
    "I once got lost in the living room after a movie marathon. I haven't seen sunlight since.",
    "Me perd� una vez en el sal�n despu�s de un marat�n de pelis. No he vuelto a ver la luz del sol desde entonces.",
    "Em vaig perdre un cop al sal� despr�s d�una marat� de pel�l�cules. No he tornat a veure la llum del sol des de llavors.");

        CreateEntry("mcclain_exit_house",
            "You can leave the house this way, but that's not what you're looking for right now.",
            "Por aqu� se sale de la casa, pero t� no est�s buscando eso ahora.",
            "Per aqu� es surt de la casa, per� tu ara no est�s buscant aix�.");

        CreateEntry("mcclain_oppenheimer_debt",
            "Don't trust Oppenheimer, he owes me 5 euros.",
            "No te f�es de Oppenheimer, me debe 5 euros.",
            "No et refi�s de l�Oppenheimer, em deu 5 euros.");

        CreateEntry("mcclain_introduction",
            "I'm McClain, nice to meet you.",
            "Soy McClain, un placer.",
            "S�c McClain, un plaer.");



        CreateEntry("controls", "attack <attack>   shoot <shoot>", "atacar <attack>   disparar <shoot>", "atacar <attack>   disparar <shoot>");

    }

    public static string GetText(string id)
    {
        if (!instance.englishDic.ContainsKey(id))
        {
            //Debug.LogError("This key has not been initialised: " + id);
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
