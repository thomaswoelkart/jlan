using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SaveStats;
using static System.Net.Mime.MediaTypeNames;

public class ButtonInitializer : MonoBehaviour
{
    bool first;
    bool first2;
    int coins;
    GameObject Code;
    GameObject CodeError;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //Debug.Log(GameObject.Find("BattleMusic").GetComponent<Music>());
        //GameObject.Find("BattleMusic").GetComponent<Music>().StopMusic();
        //GameObject.Find("Music").GetComponent<Music>().PlayMusic();
        first = true;
        first2 = true;
        int coins = SaveScript.CoinsShop;
        var skins = SaveScript.Player; // { Jlan, Teshi, Terukazu, Akira, Supaman }
        var swords = SaveScript.Swords;

        var buttons = transform.GetChild(3);

        //-----------------------------Player-----------------------------

        for (int i = 0; i < skins.Length; i++)
        {
            var button = buttons.transform.GetChild(i + 1);
            switch (skins[i].ToString())
            {// Selected, Select, NotOwned
                case "Selected":
                    Selected(button);
                    first = false;
                    break;
                case "Select":
                    Select(button);
                    break;
                case "NotOwned":
                    ;
                    break;
            }
        }
        //-----------------------------Swords-----------------------------

        for (int i = 0; i < swords.Length; i++)
        {
            var button = buttons.transform.GetChild(i + 10);
            switch (swords[i].ToString())
            {// Selected, Select, NotOwned
                case "Selected":
                    SelectedSwords(button);
                    first2 = false;
                    break;
                case "Select":
                    Select(button);
                    break;
                case "NotOwned":
                    ;
                    break;
            }
        }
        coins = SaveScript.CoinsShop;
        GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coins + "";

        GameObject.Find("HealText").GetComponent<TextMeshProUGUI>().text = SaveScript.HealShop + "";
        GameObject.Find("JumpText").GetComponent<TextMeshProUGUI>().text = SaveScript.JumpShop + "";
        GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>().text = SaveScript.SpeedShop + "";
        Code = GameObject.Find("CodeText");
        CodeError = GameObject.Find("CodeError");
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            string code = Code.GetComponent<TextMeshProUGUI>().text + "                                ";
            CodeError.GetComponent<TextMeshProUGUI>().text = "successful";
            CodeError.GetComponent<TextMeshProUGUI>().color = Color.green;

            if (code.Substring(0,11).ToLower() == "penepiccolo")
            {
                SaveScript.Coins = SaveScript.CoinsShop + 1000000;
                SaveScript.Heal = SaveScript.HealShop + 100;
                SaveScript.Jump = SaveScript.JumpShop + 100;
                SaveScript.Speed = SaveScript.SpeedShop + 100;

                Start();
            }
            else if (code.Substring(0, 14).ToLower() == "penelevel3star")
            {
                //SaveScript.Sterne = new LevelSterne [10] { LevelSterne.Three, LevelSterne.Three, LevelSterne.Three, LevelSterne.Three, LevelSterne.Three, LevelSterne.Three, LevelSterne.Three, LevelSterne.Three, LevelSterne.Three, LevelSterne.Three};
                for (int i = 0; i < 10; i++)
                {
                    SaveScript.SetStar(i, LevelSterne.Three);
                }
            }
            else if (code.Substring(0, 9).ToLower() == "penelevel")
            {
                SaveScript.Level = 999;
            }
            else
            {
                CodeError.GetComponent<TextMeshProUGUI>().text = "failed";
                CodeError.GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
    }

    public void Selected(Component button)
    {
        if (getPlayerStats(button.tag) == Shop.NotOwned)
        {
            if (getPlayerCosts(button.tag) <= coins)
            {
                SaveScript.Coins = coins - getPlayerCosts(button.tag);
            }
            else
                return;
        }
        button.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = "selected";
        button.GetComponent<Button>().interactable = false;
        button.transform.GetChild(3).GetComponentInChildren<RectTransform>().offsetMin = new Vector2(2.5f, 7.300018f);
        button.transform.GetChild(3).GetComponentInChildren<RectTransform>().offsetMax = new Vector2(-2.5f, -96.69998f);
        button.transform.GetChild(4).gameObject.SetActive(false);
        if (!first)
        {
            //print("latest Button" + (button.transform.parent.GetChild(SaveScript.SelectedPlayer + 1).tag));
            Select(button.transform.parent.GetChild(SaveScript.SelectedPlayer + 1));
        }

        //print("Tag: " + button.tag);
        setPlayerSkin(button.tag);
        print(SaveScript.SelectedPlayer);
        coins = SaveScript.CoinsShop;
        GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coins + "";
    }

    public void Select(Component button)
    {
        button.GetComponent<Button>().interactable = true;
        button.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = "select";
        button.transform.GetChild(3).GetComponentInChildren<RectTransform>().offsetMin = new Vector2(2.5f, 7.300018f);
        button.transform.GetChild(3).GetComponentInChildren<RectTransform>().offsetMax = new Vector2(-2.5f, -96.69998f);
        button.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void BuyGadgets(Component button)
    {

        if (10 <= coins)
        {
            SaveScript.Coins = coins - 10;
            AddGadget(button.tag);
        }
        coins = SaveScript.CoinsShop;

        GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coins + "";

        GameObject.Find("HealText").GetComponent<TextMeshProUGUI>().text = SaveScript.HealShop + "";
        GameObject.Find("JumpText").GetComponent<TextMeshProUGUI>().text = SaveScript.JumpShop + "";
        GameObject.Find("SpeedText").GetComponent<TextMeshProUGUI>().text = SaveScript.SpeedShop + "";
    }

    public void AddGadget(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Heal":
                SaveScript.Heal = SaveScript.HealShop + 1;
                break;
            case "Jump":
                SaveScript.Jump = SaveScript.JumpShop + 1;
                break;
            case "Speed":
                SaveScript.Speed = SaveScript.SpeedShop + 1;
                break;
        }
    }

    //-----------------------------Player-----------------------------
    public void setPlayerSkin(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Jlan":
                SaveScript.SelectedPlayer = 0;
                break;
            case "Teshi":
                SaveScript.SelectedPlayer = 1;
                break;
            case "Terukazu":
                SaveScript.SelectedPlayer = 2;
                break;
            case "Akira":
                SaveScript.SelectedPlayer = 3;
                break;
            case "Supaman":
                SaveScript.SelectedPlayer = 4;
                break;
            default:
                SaveScript.SelectedPlayer = 0;
                break;
        }
    }
    public int getPlayerIndex(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Jlan":
                return 0;
            case "Teshi":
                return 1;
            case "Terukazu":
                return 2;
            case "Akira":
                return 3;
            case "Supaman":
                return 4;
            default:
                return 0;
        }
    }

    public int getPlayerCosts(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Jlan":
                return 0;
            case "Teshi":
                return 1000;
            case "Terukazu":
                return 1500;
            case "Akira":
                return 2000;
            case "Supaman":
                return 2500;
            default:
                return 0;
        }
    }
    public Shop getPlayerStats(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Jlan":
                return SaveScript.Player[0];
            case "Teshi":
                return SaveScript.Player[1];
            case "Terukazu":
                return SaveScript.Player[2];
            case "Akira":
                return SaveScript.Player[3];
            case "Supaman":
                return SaveScript.Player[4];
            default:
                return SaveScript.Player[0];
        }
    }

    //-----------------------------Swords-----------------------------

    public void SelectedSwords(Component button)
    {
        if (getSwordsStats(button.tag) == Shop.NotOwned)
        {
            if (getSwordsCosts(button.tag) <= coins)
            {
                SaveScript.Coins = coins - getSwordsCosts(button.tag);
            }
            else
                return;
        }
        button.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = "selected";
        button.GetComponent<Button>().interactable = false;
        button.transform.GetChild(3).GetComponentInChildren<RectTransform>().offsetMin = new Vector2(2.5f, 7.300018f);
        button.transform.GetChild(3).GetComponentInChildren<RectTransform>().offsetMax = new Vector2(-2.5f, -96.69998f);
        button.transform.GetChild(4).gameObject.SetActive(false);
        if (!first2)
        {
            //print("latest Button" + (button.transform.parent.GetChild(SaveScript.SelectedSwords + 1).tag));
            Select(button.transform.parent.GetChild(SaveScript.SelectedSwords + 10));
        }

        //print("Tag: " + button.tag);
        setSwordsSkin(button.tag);
        //print(SaveScript.SelectedSwords);
        coins = SaveScript.CoinsShop;
        GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = coins + "";
    }
    public void setSwordsSkin(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Katana":
                SaveScript.SelectedSwords = 0;
                break;
            case "Iaito":
                SaveScript.SelectedSwords = 1;
                break;
            case "Tanto":
                SaveScript.SelectedSwords = 2;
                break;
            default:
                SaveScript.SelectedSwords = 0;
                break;
        }
    }
    public int getSwordsIndex(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Katana":
                return 0;
            case "Iaito":
                return 1;
            case "Tanto":
                return 2;
            default:
                return 0;
        }
    }
    public int getSwordsCosts(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Katana":
                return 0;
            case "Iaito":
                return 500;
            case "Tanto":
                return 1000;
            default:
                return 0;
        }
    }
    public Shop getSwordsStats(string name)
    {
        switch (name)
        {// Selected, Select, NotOwned
            case "Katana":
                return SaveScript.Swords[0];
            case "Iaito":
                return SaveScript.Swords[1];
            case "Tanto":
                return SaveScript.Swords[2];
            default:
                return SaveScript.Swords[0];
        }
    }
}
