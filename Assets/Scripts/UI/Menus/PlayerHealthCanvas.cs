using Player;
using System;
using System.Collections;
using UnityEngine;

namespace UI.Menus
{
    /// <summary>
    /// The behavior for the player health canvas
    /// </summary>
    public class PlayerHealthCanvas : MonoBehaviour
    {
        [SerializeField]
        private Canvas m_canvas;

        [SerializeField]
        private CanvasGroup m_canvasGroup;

        [SerializeField]
        private float m_fadeDuration;

        [SerializeField]
        private PlayerController m_player;

        public event Action OnShowed;

        private void Awake()
        {
            if (!m_canvas)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its canvas.");
            else
                m_canvas.enabled = false;

            if (!m_canvasGroup)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its CanvasGroup component.");

            if (!m_player)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the player health.");
            else
                m_player.OnShowHealth += FadeCanvas;
        }

        private void OnDisable() => m_player.OnShowHealth -= FadeCanvas;

        private void FadeCanvas()
        {
            m_canvas.enabled = true;
            m_canvasGroup.alpha = 1f;
            OnShowed?.Invoke();
            StartCoroutine(FadeAway());
        }

        private IEnumerator FadeAway()
        {
            float elapsedTime = 0f;

            while (elapsedTime < m_fadeDuration)
            {
                m_canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / m_fadeDuration);

                elapsedTime += Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            m_canvas.enabled = false;
        }
    }
}