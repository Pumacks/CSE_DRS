using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GameStateManagement;
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

        public MeleeWeapon(String itemName, Texture2D itemTexture, Entity itemOwner, float weaponDamage, float attackSpeed, float weaponRange, List<Enemy> enemies) : base(itemName, itemTexture, itemOwner, weaponDamage, attackSpeed, weaponRange, enemies)
        {

        }

        public override void use()
        {
            throw new System.NotImplementedException();
        }

        // Hier Room zurückändern zu Level, sobald die Map verfeinert wurde.
        public override void attack(Entity owner) // Momentan 360° Angriff auf alle Feinde in Waffenreichweite des Spielers
        {
            //Check for attack-cooldown


            //Attack
            Enemies.ForEach(targetEnemy => // remove WeaponDamage amount of HealthPoints from all Enemies in WeaponRange.
            {
                if (distance(owner.Position, targetEnemy.Position) <= this.WeaponRange)
                {
                    targetEnemy.TakeDamage((int)this.WeaponDamage);
                }
            });

        }

        public override void DrawItem(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }



    }
}
