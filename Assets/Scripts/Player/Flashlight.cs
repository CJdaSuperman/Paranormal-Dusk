using UnityEngine;

namespace Player
{
    /// <summary>
    /// The behavior for the flashlight
    /// </summary>
    public class Flashlight : MonoBehaviour
    {
        [SerializeField]
        private Light m_light;

        [SerializeField]
        private float m_lightDecay;

        [SerializeField]
        private float m_angleDecay;

        [SerializeField]
        private float m_minAngle;

        private void Awake()
        {
            if (!m_light)
                Debug.LogError($"{gameObject.name} doesn't have a reference to the Light component.");
        }

        private void Update()
        {
            DecreaseLightAngle();
            m_light.intensity -= m_lightDecay * Time.deltaTime;
        }

        public void Restore(float restoredAngle, float restoredIntensity)
        {
            m_light.spotAngle = restoredAngle;
            m_light.intensity = restoredIntensity;
        }

        private void DecreaseLightAngle()
        {
            if (m_light.spotAngle <= m_minAngle)
                return;

            m_light.spotAngle -= m_angleDecay * Time.deltaTime;
        }
    }
}