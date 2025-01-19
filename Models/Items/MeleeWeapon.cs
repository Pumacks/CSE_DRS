using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameStateManagementSample.Models.Items
{
    public class MeleeWeapon : Weapon
    {

        #region attributes and properties
        #endregion

        public MeleeWeapon(String itemName, Texture2D itemTexture, Entity itemOwner, float weaponDamage, float attackSpeed, float weaponRange, List<Enemy> enemies, Engine engine) : base(itemName, itemTexture, itemOwner, weaponDamage, attackSpeed, weaponRange, enemies, engine)
        {

        }

        public override void use()
        {
            throw new System.NotImplementedException();
        }

        // Hier Room zurückändern zu Level, sobald die Map verfeinert wurde.
        public override void attack(Entity owner) // Momentan Auswahl zwischen eingeschränktem Kegelbereichs-Angriff und uneingeschränkten 360° Angriff auf alle Feinde in Waffenreichweite des Spielers
        {
            //Check for attack-cooldown (happens in Weapon class)


            // 360° attack
            // Enemies.ForEach(targetEnemy => // remove WeaponDamage amount of HealthPoints from all Enemies in WeaponRange.
            // {
            //     if (distance(owner.Position, targetEnemy.Position) <= this.WeaponRange)
            //     {
            //         targetEnemy.HealthPoints -= (int)this.WeaponDamage;
            //     }
            // });

            Random random = new Random();
            if (owner is Player)
            {


                if (random.Next(0, 2) == 0)
                    gameEngine.swordSwing1.Play();
                else
                    gameEngine.swordSwing2.Play();


                Vector2 vectorWeaponToCursor = vectorToTarget();
                float lengthWeaponToCursor = (float)Math.Sqrt(vectorWeaponToCursor.X * vectorWeaponToCursor.X + vectorWeaponToCursor.Y * vectorWeaponToCursor.Y);
                Vector2 unitVectorWeaponToCursor = new Vector2(vectorWeaponToCursor.X / lengthWeaponToCursor, vectorWeaponToCursor.Y / lengthWeaponToCursor);

                gameEngine.Enemies.ForEach(targetEnemy => // remove WeaponDamage amount of HealthPoints from all Enemies in WeaponRange that are within a designated area.
                {
                    if (distance(owner.Position, targetEnemy.Position) <= this.WeaponRange)
                    {
                        Vector2 vectorWeaponToEnemy = new Vector2(targetEnemy.Position.X - this.ItemOwner.Position.X, targetEnemy.Position.Y - this.ItemOwner.Position.Y);
                        float lengthWeaponToEnemy = (float)Math.Sqrt(vectorWeaponToEnemy.X * vectorWeaponToEnemy.X + vectorWeaponToEnemy.Y * vectorWeaponToEnemy.Y);
                        Vector2 unitVectorWeaponToEnemy = new Vector2(vectorWeaponToEnemy.X / lengthWeaponToEnemy, vectorWeaponToEnemy.Y / lengthWeaponToEnemy);

                        double scalarProduct = unitVectorWeaponToCursor.X * unitVectorWeaponToEnemy.X + unitVectorWeaponToCursor.Y * unitVectorWeaponToEnemy.Y;

                        double angle = Math.Acos(scalarProduct) * (180.0 / Math.PI);

                        if (angle <= 52)
                        {
                            targetEnemy.TakeDamage((int)WeaponDamage);
                            Player.totalScore += (int)weaponDamage;
                        }
                    }
                });
            }
            else
            {
                if (owner is EnemyWarrior)
                {
                    
                if (random.Next(0, 2) == 0)
                    gameEngine.enemySwordHit1.Play();
                else
                    gameEngine.enemySwordHit2.Play();

                } else if (owner is EnemySpearman) {
                    gameEngine.enemySpearHit1.Play();
                }
                

                if (distance(owner.Position, gameEngine.heroPlayer.Position) <= this.WeaponRange) {
                    gameEngine.heroPlayer.TakeDamage((int)WeaponDamage);
                }
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
