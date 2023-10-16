using Guns;
using System;
using System.Collections;
using UnityEngine;

namespace UI.Menus
{
    /// <summary>
    /// The behavior for the ammo inventory canvas
    /// </summary>
    public class AmmoInventoryCanvas : MonoBehaviour
    {
        [SerializeField]
        private Canvas m_canvas;

        [SerializeField]
        private CanvasGroup m_canvasGroup;

        [SerializeField]
        private float m_fadeDuration;

        [SerializeField]
        private GunsController m_gunsController;

        public event Action OnShowed;

        private void Awake()
        {
            if (!m_canvas)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its canvas.");
            else
                m_canvas.enabled = false;

            if (!m_canvasGroup)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its CanvasGroup component.");

            if (!m_gunsController)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the {nameof(GunsController)}.");
            else
                m_gunsController.OnShowAmmo += FadeCanvas;
        }

        private void OnDestroy() => m_gunsController.OnShowAmmo -= FadeCanvas;

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