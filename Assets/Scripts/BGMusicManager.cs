using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
    public static BGMusicManager instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioSource[] bgMusics;
    private int currentMusicIndex = 0;

    public AudioSource gameOverMusic, victoryMusic;

    private void Start()
    {
        PlayNextMusic();
    }


    private void PlayNextMusic()
    {
        int nextMusicIndex = currentMusicIndex + 1;

        if (nextMusicIndex >= bgMusics.Length)
        {
            nextMusicIndex = 0;
        }

        bgMusics[currentMusicIndex].Stop();
        bgMusics[nextMusicIndex].Play();
        bgMusics[nextMusicIndex].loop = false;

        currentMusicIndex = nextMusicIndex;
        Invoke("PlayNextMusic", bgMusics[currentMusicIndex].clip.length);

    }

    public void PlayGameOverMusic()
    {
        foreach (var music in bgMusics) { music.Stop(); }

        gameOverMusic.Play();
    }

    public void PlayVictoryMusic()
    {
        foreach (var music in bgMusics) { music.Stop(); }

        victoryMusic.Play();
    }
}