using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        Time.timeScale = 1;

        soundSlider.onValueChanged.AddListener(delegate { changeFXSound(); });
        soundSlider.value = (float)SaveScript.Sound;

        musicSlider.onValueChanged.AddListener(delegate { changeMusicSound(); });
        musicSlider.value = (float)SaveScript.Music;
    }

    private void changeMusicSound()
    {
        GameObject.Find("BattleMusic").GetComponent<AudioSource>().volume = musicSlider.value;
        GameObject.Find("Music").GetComponent<AudioSource>().volume = musicSlider.value;
        SaveScript.Music = musicSlider.value;
    }

    private void changeFXSound()
    {
        SaveScript.Sound = soundSlider.value;
    }

    public void Quit() => Application.Quit();

    public void ShowOptions(bool active)
    {
        StartCoroutine(OptionsDelay(active));
    }

    IEnumerator OptionsDelay(bool active)
    {
        yield return new WaitForSeconds(0.5f);
        mainMenu.SetActive(!active);
        optionsMenu.SetActive(active);
    }

    IEnumerator LevelsDelay(bool active)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName: active ? "LevelMenu" : "Menu");
    }

    public void ShowLevels(bool active)
    {
        StartCoroutine(LevelsDelay(active));
    }

    IEnumerator ShopDelay(bool active)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName: active ? "Shop" : "Menu");
    }

    public void ShowShop(bool active)
    {
        StartCoroutine(ShopDelay(active));
    }

}
