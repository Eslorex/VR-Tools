using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    // Singleton instance
    public static CoroutineRunner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
