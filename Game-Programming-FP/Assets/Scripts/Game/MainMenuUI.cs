using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainPanel;
    public Slider sensitivitySlider;
    public TMP_Text sensitivityValueText;
    public TMP_Text timeTrackingText;

    void Start()
    {
        float savedSensitivity = PlayerPrefs.GetFloat("sensitivity", 100f);
        sensitivitySlider.value = savedSensitivity;
        UpdateSensitivityText(savedSensitivity);

        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    void Update()
    {
        timeTrackingText.text = GameObject.FindGameObjectWithTag("TimeTracker").GetComponent<TimeTracking>().GetTime();
    }

    public void OnSensitivityChange()
    {
        float sensitivity = sensitivitySlider.value;
        PlayerPrefs.SetFloat("sensitivity", sensitivity);
        UpdateSensitivityText(sensitivity);
    }

    private void UpdateSensitivityText(float sensitivity)
    {
        sensitivityValueText.text = sensitivity.ToString();
    }

    public void PlayGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void HideSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
