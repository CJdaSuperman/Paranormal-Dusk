using System;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Manages cursor state, game state, and time scaling
    /// </summary>
    public static class GameManager
    {
        public static bool IsGamePaused { get => Time.timeScale == 0f; }

        public static event Action OnGameLost;

        public static void PauseGame(bool gamePaused)
        {
            EnableTimeScale(!gamePaused);
            EnableCursor(gamePaused);
        }

        public static void EndGame()
        {
            EnableTimeScale(false);
            EnableCursor(true);
            OnGameLost?.Invoke();
        }

        public static void EnableCursor(bool enable)
        {
            Cursor.visible = enable;
            Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Confined;
        }

        public static void EnableTimeScale(bool enable) => Time.timeScale = enable ? 1f : 0f;
    }
}