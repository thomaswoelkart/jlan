using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseResume : MonoBehaviour
{
    public GameObject PauseMenu;
    GameObject LevelLoader;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        LevelLoader = GameObject.Find("LevelLoader");
        GameObject.Find("Music").GetComponent<Music>().StopMusic();
        GameObject.Find("BattleMusic").GetComponent<Music>().PlayMusic();
        //PauseMenu = GameObject.Find("PauseMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PauseGame()
    {
        Pause();
        PauseMenu.SetActive(true);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Resume();
        PauseMenu.SetActive(false);
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void ToLevelMenu()
    {
        Resume();
        LevelLoader.GetComponent<LevelLoader>().LoadNextScene("LevelMenu");
        GameObject.Find("BattleMusic").GetComponent<Music>().StopMusic();
        GameObject.Find("Music").GetComponent<Music>().PlayMusic();
        //SceneManager.LoadScene(sceneName: "LevelMenu");
    }
    public void Restart()
    {
        Resume();
        string active = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(sceneName: active);
    }
    public void NextLevel()
    {
        Resume();
        string active = "Level" + (int.Parse(SceneManager.GetActiveScene().name.Substring(5)) + 1);

        SceneManager.LoadScene(sceneName: active);
    }
}
