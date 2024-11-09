using System.Diagnostics.CodeAnalysis;

namespace GameStateManagementSample.Models.Items
{
    public class MeleeWeapon : Weapon
    {

        #region attributes and properties
        #endregion

        public override void use()
        {
            throw new System.NotImplementedException();
        }

        public abstract void attack();

        public void attackArea() {
            
        }
    }
}
