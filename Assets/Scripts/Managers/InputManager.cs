using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages the input for the game
    /// </summary>
    public static class InputManager
    {
        private const string ScrollWheel = "Mouse ScrollWheel";

        public static bool Shoot() => Input.GetMouseButton(0);
        
        public static bool AimDown() => Input.GetMouseButton(1);
        
        public static bool ReleaseAimDown() => Input.GetMouseButtonUp(1);

        public static bool Reload() => Input.GetKeyDown(KeyCode.R);

        public static bool Interact() => Input.GetKeyDown(KeyCode.E);

        public static float Scroll() => Input.GetAxis(ScrollWheel);

        public static bool Weapon1() => Input.GetKeyDown(KeyCode.Alpha1);

        public static bool Weapon2() => Input.GetKeyDown(KeyCode.Alpha2);

        public static bool Weapon3() => Input.GetKeyDown(KeyCode.Alpha3);

        public static bool Weapon4() => Input.GetKeyDown(KeyCode.Alpha4);

        public static bool PauseGame() => Input.GetKeyDown(KeyCode.Escape);
        
        public static bool ShowAmmo() => Input.GetKeyDown(KeyCode.Tab);

        public static bool ShowHealth() => Input.GetKeyDown(KeyCode.Q);
    }
}