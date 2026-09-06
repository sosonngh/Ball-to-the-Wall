using TMPro;
using UnityEngine;

public class ScoreGainedAnimation : MonoBehaviour
{
    [SerializeField] private float animationLengthInSeconds;
    [SerializeField] private float distanceUp;
    private float timeLeft;

    private RectTransform rectTransform;
    private TMP_Text tmpText;

    private void Start()
    {
        timeLeft = animationLengthInSeconds;
        rectTransform = GetComponent<RectTransform>();
        tmpText = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        // destroy itself at the end
        if (timeLeft <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        // move up and fade
        rectTransform.anchoredPosition += Vector2.up * (distanceUp / animationLengthInSeconds) * Time.deltaTime;
        float newOpacity = tmpText.color.a - (1 / animationLengthInSeconds) * Time.deltaTime;
        tmpText.color = new Color(tmpText.color.r, tmpText.color.g, tmpText.color.b, newOpacity);

        timeLeft -= Time.deltaTime;
    }
}
