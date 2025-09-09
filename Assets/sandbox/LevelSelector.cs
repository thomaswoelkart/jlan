using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static SaveStats;

public class LevelSelector : MonoBehaviour
{
    public Sprite star;

    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public int numberOfLevels = 10;
    public Vector2 iconSpacing;
    public GameObject levelLoader;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int amountPerPage;
    private int currentLevelCount;
    private int levelFortschritt = 1;
    private LevelSterne[] sterne;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        //GameObject.Find("Music").GetComponent<Music>().StopMusic();
        //GameObject.Find("BattleMusic").GetComponent<Music>().PlayMusic();
        sterne = SaveScript.Sterne;
        levelFortschritt = SaveScript.Level;
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInACol = Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));
        amountPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);
        LoadPanels(totalPages);
    }
    void LoadPanels(int numberOfPanels)
    {
        GameObject panelClone = Instantiate(levelHolder) as GameObject;
        PageSwiper swiper = levelHolder.AddComponent<PageSwiper>();
        swiper.totalPages = numberOfPanels;

        for (int i = 1; i <= numberOfPanels; i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i - 1), 0);
            SetUpGrid(panel);
            int numberOfIcons = i == numberOfPanels ? numberOfLevels - currentLevelCount : amountPerPage;
            LoadIcons(numberOfIcons, panel);
        }
        Destroy(panelClone);
    }
    void SetUpGrid(GameObject panel)
    {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }
    void LoadIcons(int numberOfIcons, GameObject parentObject)
    {
        for (int i = 1; i <= numberOfIcons; i++)
        {
            currentLevelCount++;
            GameObject icon = Instantiate(levelIcon) as GameObject;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = "Level " + i;
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Level " + currentLevelCount);
            int levelID = currentLevelCount;
            var stern = sterne[currentLevelCount - 1];
            //Debug.Log(SaveScript.Sterne[i]);
            int k;
            

            if (stern != SaveStats.LevelSterne.NotStarted)
            {
                if (stern == SaveStats.LevelSterne.One) k = 1;
                else if (stern == SaveStats.LevelSterne.Two) k = 2;
                else k = 3;
                //Debug.Log("hey");
                for (int j = 1; j <= k; j++)
                    icon.transform.GetChild(j).GetComponent<Image>().sprite = star;
            }
            else
            {
                for (int j = 1; j <= 3; j++)
                    icon.transform.GetChild(j).gameObject.SetActive(false);
            }
            icon.GetComponentInChildren<Button>().onClick.AddListener(() => levelLoader.GetComponent<LevelLoader>().LoadNextScene(sceneName: "Level" + levelID));
            if (currentLevelCount > levelFortschritt)
                icon.GetComponent<Button>().interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}