using System.Collections;
using UnityEngine;

public class PlayerHealthCanvasHandler : MonoBehaviour
{
    [SerializeField] float fadeDuration = 1.5f;

    Canvas playerHealthCanvas;

    void Start()
    {
        playerHealthCanvas = GetComponent<Canvas>();
        playerHealthCanvas.enabled = false;
    }

    public void FadeCanvas()
    {
        GetComponent<Canvas>().enabled = true;

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1f;
        StartCoroutine(FadeAway(canvasGroup, canvasGroup.alpha, 0f));        
    }

    //IEnumerator instaead of Update method because I don't want loop to be done by a frame by frame basis
    IEnumerator FadeAway(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < fadeDuration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / fadeDuration);

            yield return null;  //returns null because I don't need this method to wait to execute
        }

        playerHealthCanvas.enabled = false;
    }
}
