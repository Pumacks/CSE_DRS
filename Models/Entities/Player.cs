using GameStateManagementSample.Models.GUI;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;


namespace GameStateManagementSample.Models.Entities
{
    public class Player : Entity
    {
        private double atackTimer = 0;
        private bool isAtacking = false;


        private Vector2 healthPositionGUI = new Vector2(20, 20);

        List<GUIObserver> GUIObservers = new();

        public Player() { }
        public Player(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
        : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items)
        {
            GUIObservers.Add(new HealthGUI(this));
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
                    TakeDamage(25);
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
            else if (movement == Vector2.Zero)
                Texture = animationManager.IdleAnimation();



            boundingBox.X = (int)position.X - Texture.Width / 2;
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
            spriteBatch.End();
            spriteBatch.Begin();

            foreach (GUIObserver observer in GUIObservers)
            {
                observer.Draw(spriteBatch, spriteFont, healthPositionGUI);
            }

        }

        public override void LoadContent(ContentManager content)
        {
            animationManager.loadTextures(content);
        }

        private void NotifyObservers()
        {
            foreach (GUIObserver observer in GUIObservers)
            {
                observer.Update();
            }

        }
        public override void TakeDamage(int damage)
        {
            HealthPoints = MathHelper.Max(0, HealthPoints - damage);
            NotifyObservers();
        }


        public void PlayerDeathAnimation()
        {
            Texture = animationManager.DeathAnimation();
        }
    }
}
