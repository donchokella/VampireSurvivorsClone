using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }


    private bool gameActive;
    public float timer;

    public float waitToShowEndScreen = 1f;

    private void Start()
    {
        gameActive = true;
    }

    private void Update()
    {
        if(gameActive)
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }
    }

    public void EndLevel()
    {
        gameActive = false;

        StartCoroutine("EndLevelCo");
        
    }

    IEnumerator EndLevelCo()
    {
        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60);

        UIController.instance.endTimeText.text = minutes.ToString() + " mins " + seconds.ToString("00") + " secs";

        yield return new WaitForSeconds(waitToShowEndScreen);

        UIController.instance.levelEndScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(UIController.instance.endLevelFirstButton);

    }

}
