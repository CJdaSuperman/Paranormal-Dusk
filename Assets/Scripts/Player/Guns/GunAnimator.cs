using UnityEngine;

namespace Guns
{
    /// <summary>
    /// Controls the animations for guns
    /// </summary>
    public class GunAnimator
    {
        private const string ReloadingName = "Reloading";
        private const string AimDownName   = "Aim Down";

        private static readonly int Reloading = Animator.StringToHash(ReloadingName);
        private static readonly int AimDown   = Animator.StringToHash(AimDownName);

        private Animator m_animator;

        public GunAnimator(Animator animator)
        {
            m_animator = animator;
        }

        public void SetReloading(bool state) => m_animator.SetBool(Reloading, state);

        public void SetADS(bool state) => m_animator.SetBool(AimDown, state);
    }
}