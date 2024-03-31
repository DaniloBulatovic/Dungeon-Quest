using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private float timer;
    public bool isGamePaused;

    [Header("UI")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject timerValue;

    [Header("Doors")]
    [SerializeField] private GameObject doorStart;

    [Header("Goblet")]
    [SerializeField] private GameObject Goblet;
    [SerializeField] private AudioClip victoryAudio;

    protected override void Awake()
    {
        base.Awake();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        isGamePaused = true;
    }
    private IEnumerator Timer()
    {
        while (!isGamePaused)
        {
            timer++;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void PlayGame()
    {
        isGamePaused = false;
        StartCoroutine(Timer());
        mainMenuPanel.SetActive(false);
        doorStart.GetComponent<DoorController>().InteractWithObject();
    }

    public void EndGame()
    {
        Goblet.GetComponent<AudioSource>().PlayOneShot(victoryAudio);
        isGamePaused = true;
        timerValue.GetComponent<TMP_Text>().text = GetTextFromTimer();
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private string GetTextFromTimer()
    {
        TimeSpan time = TimeSpan.FromSeconds(timer);

        string str = time.ToString(@"hh\:mm\:ss");
        return str;
    }
}
