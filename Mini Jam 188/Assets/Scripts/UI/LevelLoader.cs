using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [Header("Menu Scene")]
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject mainMenu;

    [Header("Slider")]
    [SerializeField] Slider loadingSlider;

    [Header("Loading Text")]
    [SerializeField] TextMeshProUGUI loadingText;  // Text component for percentage display

    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;

            // Update the percentage text
            loadingText.text = (progressValue * 100f).ToString("F0") + "%";

            yield return null;
        }
    }
}
