using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;


namespace GameStateManagementSample.Models.Entities
{
    public class Player : Entity
    {
        private double atackTimer = 0;
        private bool isAtacking = false;
        public Player() { }
        public Player(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, List<Item> items)
        : base(healthPoints, movementSpeed, playerPosition, texture, items)
        {
        }

        public override void Move()
        {
            Vector2 movement = Vector2.Zero;

            #region Atacking Timer
            if (isAtacking)
                atackTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (atackTimer >= 0.4)
            {
                atackTimer = 0;
                isAtacking = false;
            }
            #endregion



            #region Keyboard input
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

                // Attack with activeWeapon
                this.ActiveWeapon.weaponAttack(this);



                if (!isAtacking)
                {
                    isAtacking = true;
                    Atack();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                movement.X -= MovementSpeed;
                flipTexture = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                movement.X += MovementSpeed;
                flipTexture = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                movement.Y -= MovementSpeed; ;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                movement.Y += MovementSpeed; ;
            }
            #endregion



            if (isAtacking)
                Texture = animationManager.AttackAnimation();
            else if (movement != Vector2.Zero)
                Texture = animationManager.WalkAnimation();



            boundingBox.X = (int)position.X - Texture.Width /2 ;
            boundingBox.Y = (int)position.Y - Texture.Height / 2;
            boundingBox.Width = Texture.Width;
            boundingBox.Height = Texture.Height;



            position += movement;
        }
        public override void Atack()
        {
            Trace.WriteLine("Atack: " + ActiveWeapon + atackTimer);
            // ActiveWeapon.weaponAttack(this, )
        }

        // public 

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture: Texture,
                            position: position,
                            sourceRectangle: null,
                            color: Color.White,
                            rotation: 0f,
                            origin: new Vector2(Texture.Width / 2, Texture.Height / 2),
                            scale: 1f,
                            effects: flipTexture ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                            layerDepth: 0f);
        }

        public override void LoadContent(ContentManager content)
        {
            animationManager.loadTextures(content);
        }
    }
}
