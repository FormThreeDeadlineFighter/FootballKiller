using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class SceneManger : MonoBehaviour
{
    public void MainGround()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.Play(1, "shoot", false);
        SceneManager.LoadScene("MainScene"); // 替換成主介面        
        Debug.Log("替換成主介面");
    }
    public void ReturnStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 替換成關卡圖        
    }
    public void Stage(Button clickedButton)
    {
        Time.timeScale = 1f;
        string index = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text;
        SceneManager.LoadScene($"{index}");
        Debug.Log("替換成關卡 " + $"{index}");
    }
    public void NextStage()
    {
        Time.timeScale = 1f;
        string currentSceneName = SceneManager.GetActiveScene().name;  // 例如 Level 0
        // 以 ' ' 分割：得到 ["Level", "0"]
        string[] parts = currentSceneName.Split(' ');

        if (int.TryParse(parts[1], out int levelNumber))
        {
            levelNumber++;
            string nextSceneName = parts[0] + " " + levelNumber;
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("轉換失敗：不是有效的數字 -> " + parts[1]);
        }
    }
    public void ExitGame()
    {
        Application.Quit();      
    }
}