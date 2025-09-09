using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using static SaveStats;

public class SaveScript : MonoBehaviour
{
    public static int Coins
    {
        get
        {
            int coins = Load().coins;
            return (coins < 999999) ? coins : 999999;
        }
        set
        {
            Save(new SaveStats(null, null, null, null, null, value, null, null, null, null, Load()));
        }
    }
    public static int CoinsShop
    {
        get
        {
            return Load().coins;
        }
    }
    public static int Level
    {
        get
        {
            return Load().level;
        }
        set
        {
            Save(new SaveStats(null, null, null, null, null, null, value, null, null, null, Load()));
        }
    }
    public static Shop [] Player
    {
        get
        {
            return Load().player;
        }
        set
        {
            Save(new SaveStats(null, null, null, value, null, null, null, null, null, null, Load()));
        }
    }
    public static int SelectedPlayer
    {
        get
        {
            return Load().player.ToList().IndexOf(Shop.Selected);
        }
        set
        {
            var shop = Load().player.ToList();
            shop[SelectedPlayer] = Shop.Select;
            shop[value] = Shop.Selected;
            Save(new SaveStats(null, null, null, shop.ToArray(), null, null, null, null, null, null, Load()));
        }
    }
    public static Shop [] Swords
    {
        get
        {
            return Load().schwert;
        }
        set
        {
            Save(new SaveStats(null, null, null, null, value, null, null, null, null, null, Load()));
        }
    }
    public static int SelectedSwords
    {
        get
        {
            return Load().schwert.ToList().IndexOf(Shop.Selected);
        }
        set
        {
            var shop = Load().schwert.ToList();

            shop[SelectedSwords] = Shop.Select;
            shop[value] = Shop.Selected;

            Save(new SaveStats(null, null, null, null, shop.ToArray(), null, null, null, null, null, Load()));
        }
    }
    public static LevelSterne[] Sterne
    {
        get
        {
            return Load().sterne;
        }
        set
        {
            Save(new SaveStats(null, null, null, null, null, null, null, value, null, null, Load()));
        }
    }
    public static void SetStar(int i, LevelSterne s)
    {
        LevelSterne[] arr = Sterne;
        arr[i] = s;
        Sterne = arr;
    }
    public static int Heal
    {
        get
        {
            int heal = Load().heal;
            return (heal < 999) ? heal : 999;
        }
        set
        {
            Save(new SaveStats(value, null, null, null, null, null, null, null, null, null, Load()));
        }
    }
    public static int Jump
    {
        get
        {
            int jump = Load().jump;
            return (jump < 999) ? jump : 999;
        }
        set
        {
            Save(new SaveStats(null, value, null, null, null, null, null, null, null, null, Load()));
        }
    }
    public static int Speed
    {
        get
        {
            int speed = Load().speed;
            return (speed < 999) ? speed : 999;
        }
        set
        {
            Save(new SaveStats(null, null, value, null, null, null, null, null, null, null, Load()));
        }
    }
    public static int HealShop
    {
        get
        {
            return Load().heal;
        }
    }
    public static int JumpShop
    {
        get
        {
            return Load().jump;
        }
    }
    public static int SpeedShop
    {
        get
        {
            return Load().speed;
        }
    }
    public static double Sound
    {
        get
        {
            return Load().Sound;
        }
        set
        {
            Save(new SaveStats(null, null, null, null, null, null, null, null, value, null, Load()));
        }
    }
    public static double Music
    {
        get
        {
            return Load().Music;
        }
        set
        {
            Save(new SaveStats(null, null, null, null, null, null, null, null, null, value, Load()));
        }
    }
    public static void Save(SaveStats p)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + "Save.jlan";

        FileStream stream = new FileStream(path, System.IO.FileMode.Create);

        formatter.Serialize(stream, p);
        stream.Close();
    }
    public static SaveStats Load()
    {
        string path = Application.persistentDataPath + System.IO.Path.DirectorySeparatorChar + "Save.jlan";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, System.IO.FileMode.Open);
            try
            {
                SaveStats ss = formatter.Deserialize(stream) as SaveStats;
                stream.Close();
                return ss;
            }
            catch
            {
                stream.Close();
                Save(new SaveStats());
                return Load();
            }
        }
        else
        {
            Save(new SaveStats());
            return Load();
        }
    }
}
