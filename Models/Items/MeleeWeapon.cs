using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Models.Items
{
    public class MeleeWeapon : Weapon
    {

        #region attributes and properties
        #endregion

        public MeleeWeapon (String itemName, Texture2D itemTexture, Entity itemOwner, float weaponDamage, float attackSpeed, float weaponRange) : base (itemName, itemTexture, itemOwner, weaponDamage, attackSpeed, weaponRange) {
            
        }

        public override void use()
        {
            throw new System.NotImplementedException();
        }

        public override void attack(Entity owner, Level level) // Momentan 360° Angriff auf alle Feinde in Waffenreichweite des Spielers
        {
            level.Enemies.ForEach(targetEnemy =>
            {
                if (distance(owner.Position, targetEnemy.Position) <= this.WeaponRange)
                {
                    targetEnemy.HealthPoints -= (int)this.WeaponDamage;
                }
            });

        }
    }
}
