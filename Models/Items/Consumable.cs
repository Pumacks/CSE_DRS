using System.Diagnostics.CodeAnalysis;

namespace GameStateManagementSample.Models.Items
{
    public class Consumable : Item
    {

        #region attributes, fields and properties
        private int healingAmount;
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

        public override void use(Entity user)
        {
            useConsumable(user);
        }

        public void useConsumable(Entity user) {
            user.HealthPoints = user.HealthPoints + this.HealingAmount;
        }
    }
}
