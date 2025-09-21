using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingInvoker : MonoBehaviour
{
    private void Awake()
    {
        EnemySpawner.OnEndingReached += LoadEnding;
    }

    public void LoadEnding(bool drugsUsed)
    {
        if (!drugsUsed)
        {
            SceneManager.LoadScene("_GoodEnding");
        }
        else
        {
            SceneManager.LoadScene("_BadEnding");
        }
    }

    private void OnDestroy()
    {
        EnemySpawner.OnEndingReached -= LoadEnding;
    }
}
