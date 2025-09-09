using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveStats
{
    public enum LevelSterne { NotStarted, One, Two, Three }

    public enum Shop { Selected, Select, NotOwned }

    public int heal = 0;
    public int jump = 0;
    public int speed = 0;

    public Shop [] player = new Shop[5] { Shop.Selected, Shop.NotOwned, Shop.NotOwned, Shop.NotOwned, Shop.NotOwned };
    public Shop[] schwert = new Shop[3] { Shop.Selected, Shop.NotOwned, Shop.NotOwned };

    public int coins = 0;
    public int level = 1;

    public double Sound = 0.5;
    public double Music = 0.5;

    public LevelSterne[] sterne = new LevelSterne[10];

    public SaveStats()
    {

    }
    
    public SaveStats(int heal, int jump, int speed, Shop[] player, Shop[] schwert, int coins, int level, LevelSterne[] sterne, double sound, double music)
    {
        this.heal=heal;
        this.jump=jump;
        this.speed=speed;
        this.player=player;
        this.schwert=schwert;
        this.coins=coins;
        this.level=level;
        this.sterne=sterne;
        this.Sound=sound;
        this.Music=music;
    }
    public SaveStats(int? heal, int? jump, int? speed, Shop[] player, Shop[] schwert, int? coins, int? level, LevelSterne[] sterne, double? sound, double? music, SaveStats load)
    {
        this.heal= heal ?? load.heal;
        this.jump=jump ?? load.jump;
        this.speed=speed ?? load.speed;
        this.player=player ?? load.player;
        this.schwert=schwert ?? load.schwert;
        this.coins=coins ?? load.coins;
        this.level=level ?? load.level;
        this.sterne=sterne ?? load.sterne;
        this.Sound=sound ?? load.Sound;
        this.Music=music ?? load.Music;
    }
}
