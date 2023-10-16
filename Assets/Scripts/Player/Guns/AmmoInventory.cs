using Enums;
using System.Collections.Generic;

namespace Guns
{
    /// <summary>
    /// The ammo inventory
    /// </summary>
    public class AmmoInventory
    {
        private Dictionary<AmmoType, int> m_ammoAmounts;

        public AmmoInventory(Gun[] guns)
        {
            m_ammoAmounts = new Dictionary<AmmoType, int>();

            foreach (Gun gun in guns)
            {
                AmmoType ammoType = gun.TypeAmmo;

                if (!m_ammoAmounts.ContainsKey(ammoType))
                    m_ammoAmounts.Add(ammoType, 0);
            }
        }

        public void IncreaseAmmoAmount(AmmoType ammoType, int addition) => m_ammoAmounts[ammoType] += addition;

        public void DecreaseAmmoAmount(AmmoType type, int subtraction) => m_ammoAmounts[type] -= subtraction;

        public int GetAmmoAmount(AmmoType type) => m_ammoAmounts[type];
    }
}