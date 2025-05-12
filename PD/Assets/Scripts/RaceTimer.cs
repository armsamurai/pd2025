using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceTimer : MonoBehaviour
{
    public Text timerText;
    public Text bestTimeText;

    float raceTime = 0f;
    bool isRacing = false;

    void Start()
    {
        ShowBestTime();
        timerText.text = "0:00.000";
    }

    void Update()
    {
        if (isRacing)
        {
            raceTime += Time.deltaTime;
            timerText.text = FormatTime(raceTime);
        }
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    public void StartRace()
    {
        if (isRacing) return;
        raceTime = 0f;
        isRacing = true;
    }

    public void FinishRace()
    {
        if (!isRacing) return;
        isRacing = false;
        print("Гонка завершена! Время: " + FormatTime(raceTime));

        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (raceTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", raceTime);
            PlayerPrefs.Save();
            print("Новый рекорд!");
        }

        ShowBestTime();
    }

    void ShowBestTime()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (bestTime < float.MaxValue)
        {
            bestTimeText.text = "Best: " + FormatTime(bestTime);
        }
        else
        {
            bestTimeText.text = "Best: --:--.---";
        }
    }
}
