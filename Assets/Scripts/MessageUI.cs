using UnityEngine;
using TMPro;
using System.Collections;

public class MessageUI : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public float fadeDuration = 0.2f; 
    public float displayTime = 1.5f;  

    void Start()
    {
        if (messageText != null)
            messageText.alpha = 0; 
    }

    public void ShowMessage(string text, Color color)
    {
        StopAllCoroutines(); 
        StartCoroutine(FadeMessage(text, color));
    }

    private IEnumerator FadeMessage(string text, Color color)
    {
        messageText.text = text;
        messageText.color = color;

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            messageText.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            messageText.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }

        messageText.text = "";
    }
}
