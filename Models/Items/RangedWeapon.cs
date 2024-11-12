using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reflection.Metadata;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.World;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameStateManagementSample.Models.Items
{
    public class RangedWeapon : Weapon
    {

        #region attributes, fields and properties
        private int projectileSpeed;
        public int ProjectileSpeed
        {
            get
            {
                return this.projectileSpeed;
            }
            set
            {
                this.projectileSpeed = value;
            }
        }
        private Texture2D projectileTexture;
        public Texture2D ProjectileTexture
        {
            get
            {
                return this.projectileTexture;
            }
            set
            {
                this.projectileTexture = value;
            }
        }
        #endregion

        public RangedWeapon (String itemName, Texture2D itemTexture, Entity itemOwner, float weaponDamage, float attackSpeed, float weaponRange, int projectileSpeed, Texture2D projectileTexture) : base (itemName, itemTexture, itemOwner, weaponDamage, attackSpeed, weaponRange) {
            this.projectileSpeed = projectileSpeed;
            this.projectileTexture = projectileTexture; 
        }

        public override void use()
        {
            throw new System.NotImplementedException();
        }

        public override void attack(Entity owner, Level level)
        {
            // Setze für jeden Angriff ein Projektil in die Welt, welches sich bei jedem Update um eine von der projectileSpeed abhängigen (oder alternativ festen) Distanz bewegt, undzwar in Richtung des Mousecursors beim Abschuss.
            // Anders als bei Space Invaders, wo die Projektile nur geradewegs nach oben gehen, muss hier daher jedes Projektil die Information mitbekommen, von wo genau aus es wo genau hingeht und wie schnell es sich dabei bewegt.
            level.Projectiles.Add(
                new Projectile(
                    this.ItemName,
                    projectileTexture,
                    this.ItemOwner,
                    this.ItemOwner.Position,
                    new Vector2(Mouse.GetState().X, Mouse.GetState().Y),
                    projectileSpeed));
        }
    }
}
