using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class MenuScript : MonoBehaviour
{
    /*
    public int LevelAnzahl;
    private MainMenu menu;

    Button[] LevelButton;

    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("EventSystem").AddComponent<MainMenu>();
        LevelButton = new Button[LevelAnzahl];
        for (int i = 0; i < LevelAnzahl; i++)
        {
            int level = i + 1;
            LevelButton[i] = GameObject.Find("LevelButton" + level).GetComponent<Button>();
        }

        foreach (Button level in LevelButton)
        {
            level.enabled = false;
        }
        for(int i = 0; i < SaveScript.Level; i++)
        {
            LevelButton[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPrint()
    {
        Debug.Log("Button pressed");
    }

    public void ExitButton()
    {
        menu.ShowLevels(false);
    }

    public void ToLevel(string Level)
    {
        SceneManager.LoadScene(sceneName: Level);
    }
    */
}
