using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    void Start()
    {
        var gameManager = FindFirstObjectByType<EarthquakeGameManager>();
        if (gameManager != null)
            gameManager.StartIntroduction();
    }
}