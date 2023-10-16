using Managers;
using UnityEngine;

namespace Guns
{
    /// <summary>
    /// The behavior for switching between guns
    /// </summary>
    public class GunSwitcher : MonoBehaviour
    {
        private const int Weapon1 = 0;
        private const int Weapon2 = Weapon1 + 1;
        private const int Weapon3 = Weapon2 + 1;
        private const int Weapon4 = Weapon3 + 1;

        [SerializeField]
        private GunsController m_gunsController;

        private void Awake()
        {
            if (!m_gunsController)
                Debug.LogError($"{gameObject.name} doesn't have a reference to {nameof(GunsController)}.");
        }

        private void Update()
        {
            if (GameManager.IsGamePaused)
                return;

            ScrollWheelSwitching();
            KeyInputSwitching();
        }

        private void ScrollWheelSwitching()
        {
            if (InputManager.Scroll() < 0f)
                m_gunsController.ScrollNextGun(true);
            else if (InputManager.Scroll() > 0f)
                m_gunsController.ScrollNextGun(false);
        }

        private void KeyInputSwitching()
        {
            if (InputManager.Weapon1())
                m_gunsController.SetNextGun(Weapon1);

            if (InputManager.Weapon2())
                m_gunsController.SetNextGun(Weapon2);

            if (InputManager.Weapon3())
                m_gunsController.SetNextGun(Weapon3);

            if (InputManager.Weapon4())
                m_gunsController.SetNextGun(Weapon4);
        }
    }
}