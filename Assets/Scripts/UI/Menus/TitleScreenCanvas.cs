using Managers;
using System.Collections;
using UnityEngine;

namespace UI.Menus
{
    /// <summary>
    /// The behavior for the title screen
    /// </summary>

    public class TitleScreenCanvas : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup m_canvasGroup;

        [SerializeField]
        private float m_fadeInDuration;

        private void Awake()
        {
            if (!m_canvasGroup)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the canvas group.");
        }

        private void Start() => StartCoroutine(FadeIn());

        private IEnumerator FadeIn()
        {
            float elapsedTime = 0f;

            GameManager.EnableCursor(true);

            while (elapsedTime < m_fadeInDuration)
            {
                m_canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / m_fadeInDuration);

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            m_canvasGroup.alpha = 1f; // Ensure the alpha is fully 1
        }
    }
}