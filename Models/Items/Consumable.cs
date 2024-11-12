using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics.CodeAnalysis;

namespace GameStateManagementSample.Models.Items
{
    public class Consumable : Item
    {

        #region attributes, fields and properties
        private int healingAmount;

        public Consumable(string itemName, Texture2D itemTexture, Entity itemOwner) : base(itemName, itemTexture, itemOwner)
        {
        }

        public int HealingAmount
        {
            get
            {
                return this.healingAmount;
            }
            set
            {
                this.healingAmount = value;
            }
        }
        /*
        Möglicherweise noch andere Effekte wie temporär erhöhter ausgeteilter Schaden, oder reduzierter erhaltener Schaden?
        Nach Implementierung besagter Mechaniken.
        */
        #endregion

        public override void use()
        {
        //useConsumable(user);
        }

   

        public void useConsumable(Entity user) {
            user.HealthPoints = user.HealthPoints + this.HealingAmount;
        }
    }
}
