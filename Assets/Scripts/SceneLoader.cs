using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Назва сцени гри, яку будемо завантажувати (можна змінити в інспекторі)
    public string gameSceneName = "GameScene";

    // Метод для завантаження сцени гри за назвою gameSceneName
    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
