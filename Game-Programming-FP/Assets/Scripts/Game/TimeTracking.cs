using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTracking : MonoBehaviour
{
    public static TimeTracking instance;
    public float totalTimePlayed;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalTimePlayed = PlayerPrefs.GetFloat("totalTimePlayed", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        totalTimePlayed += Time.deltaTime;

        PlayerPrefs.SetFloat("totalTimePlayed", totalTimePlayed);
    }

    public string GetTime()
    {
        int hours = Mathf.FloorToInt(totalTimePlayed / 3600);
        int minutes = Mathf.FloorToInt((totalTimePlayed % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalTimePlayed % 60);

        return string.Format("Time spent playing game:\n{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}
