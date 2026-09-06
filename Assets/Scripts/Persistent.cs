using UnityEngine;

public class Persistent : MonoBehaviour
{
    public static Persistent Instance;

    public GameManager GameManager;
    public GameCanvas GameCanvas;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
}
