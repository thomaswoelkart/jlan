using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource _audioSource;
    private GameObject[] other;
    private bool NotFirst = false;
    public string tagName;
    private void Awake()
    {
        other = GameObject.FindGameObjectsWithTag(tagName);

        foreach (GameObject oneOther in other)
        {
            if (oneOther.scene.buildIndex == -1)
            {
                NotFirst = true;
            }
        }

        if (NotFirst == true)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        StartCoroutine(AudioHelper.FadeIn(_audioSource, 2));
    }

    public void StopMusic()
    {
        StartCoroutine(AudioHelper.FadeOut(_audioSource, 2));
    }
}