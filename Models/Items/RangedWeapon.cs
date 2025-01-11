using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Reflection.Metadata;
using GameStateManagementSample.Models;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using System.Collections.Generic;
using GameStateManagementSample.Models.GameLogic;


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

        public RangedWeapon(String itemName, Texture2D itemTexture, Entity itemOwner, float weaponDamage, float attackSpeed, float weaponRange, List<Enemy> enemies, int projectileSpeed, Texture2D projectileTexture, List<Projectile> projectileList, Engine engine) : base(itemName, itemTexture, itemOwner, weaponDamage, attackSpeed, weaponRange, enemies, engine)
        {
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

            Random random = new Random();
            int randomInt = random.Next(0, 3);
            if (randomInt == 0)
                gameEngine.bowShoot1.Play();
            else if (randomInt == 1)
                gameEngine.bowShoot2.Play();
            else
                gameEngine.bowShoot3.Play();
            // if (random.Next(0,3) == 0)
            // gameEngine.bowShoot1.Play();
            // else if (random.Next(0,2) == 0)
            // gameEngine.bowShoot2.Play();
            // else
            // gameEngine.bowShoot3.Play();

            Vector2 cursorPosition = Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(owner.Camera.Transform));
            Vector2 heroPosition = gameEngine.heroPlayer.Position;

            if (owner is Player)
            {
                Projectiles.Add(
                new Projectile(
                    this.ItemName,
                    projectileTexture,
                    this.ItemOwner,
                    this.ItemOwner.Position,
                    cursorPosition,
                    projectileSpeed,
                    this.weaponRange,
                    this.weaponDamage));
            }
            else {
                Projectiles.Add(
                new Projectile(
                    this.ItemName,
                    projectileTexture,
                    owner,
                    // this.ItemOwner.Position,
                    owner.Position,
                    heroPosition,
                    // new Vector2(5000,5000),
                    projectileSpeed,
                    this.weaponRange,
                    this.weaponDamage));
            }

        }

        public override void DrawItem(SpriteBatch spriteBatch)
        {
            {
                spriteBatch.Draw(texture: ItemTexture,
                    position: Position,
                    sourceRectangle: null,
                    color: Color.White,
                    rotation: 1.570796f,
                    origin: new Vector2(ItemTexture.Width / 2, ItemTexture.Height / 2),
                    scale: 1f,
                    effects: SpriteEffects.None,
                    layerDepth: 0f);
            }
        }
    }
}
