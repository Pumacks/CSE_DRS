using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Reflection.Metadata;
using GameStateManagementSample.Models.Camera;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using System.Collections.Generic;


namespace GameStateManagementSample.Models.Items
{
    public class RangedWeapon : Weapon
    {

        #region attributes, fields and properties
        private int projectileSpeed; // Pixels per second
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

        public RangedWeapon (String itemName, Texture2D itemTexture, Entity itemOwner, float weaponDamage, float attackSpeed, float weaponRange, List<Enemy> enemies, int projectileSpeed, Texture2D projectileTexture, List<Projectile> projectileList) : base (itemName, itemTexture, itemOwner, weaponDamage, attackSpeed, weaponRange, enemies) {
            this.projectileSpeed = projectileSpeed;
            this.projectileTexture = projectileTexture;
            this.Projectiles = projectileList;
        }

        public override void use()
        {
            throw new System.NotImplementedException();
        }

        public override void attack(Entity owner)
        {

            Vector2 vector2 = Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(owner.CameraProperty.Transform));

            Projectiles.Add(
            new Projectile(
                this.ItemName,
                projectileTexture,
                this.ItemOwner,
                this.ItemOwner.Position,
                vector2,
                projectileSpeed,
                this.weaponRange, 
                10));
        }

        public override void DrawItem(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
