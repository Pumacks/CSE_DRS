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
        private int selectedInventorySlot = 0;
        public int SelectedInventorySlot { get { return this.selectedInventorySlot; } set { selectedInventorySlot = value; } }
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState = Keyboard.GetState();
        private MouseState currentMouseState;
        private MouseState previousMouseState = Mouse.GetState();

        private bool isAtacking = false;


        private Vector2 healthPositionGUI = new Vector2(20, 20);

        List<GUIObserver> GUIObservers = new();

        public Player() { }
        public Player(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
        : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items)
        {
            GUIObservers.Add(new HealthGUI(this));
        }

         
        public override void Move(Vector2 movement)
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
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


            #region Input: Inventory

            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (currentKeyboardState.IsKeyDown(Keys.Q) && !previousKeyboardState.IsKeyDown(Keys.Q))
                {
                    if (selectedInventorySlot <= 0)
                    {
                        selectedInventorySlot = this.inventory.Length - 1;
                    }
                    else
                    {
                        selectedInventorySlot--;
                    }
                }
            }
            // previousKeyboardState = currentKeyboardState; This has to occur inside the move method at some point after the methods required for single key pressed. I put it at the very end.

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                if (currentKeyboardState.IsKeyDown(Keys.E) && !previousKeyboardState.IsKeyDown(Keys.E))
                {
                    if (selectedInventorySlot >= this.inventory.Length - 1)
                    {
                        selectedInventorySlot = 0;
                    }
                    else
                    {
                        selectedInventorySlot++;
                    }
                }
            }
            // previousKeyboardState = currentKeyboardState; This has to occur inside the move method at some point after the methods required for single key pressed. I put it at the very end.

            if (Mouse.GetState().ScrollWheelValue > previousMouseState.ScrollWheelValue)
            {
                if (selectedInventorySlot <= 0)
                {
                    selectedInventorySlot = this.inventory.Length - 1;
                }
                else
                {
                    selectedInventorySlot--;
                }
                previousMouseState = Mouse.GetState();
            }

            if (Mouse.GetState().ScrollWheelValue < previousMouseState.ScrollWheelValue)
            {
                if (selectedInventorySlot >= this.inventory.Length - 1)
                {
                    selectedInventorySlot = 0;
                }
                else
                {
                    selectedInventorySlot++;
                }
                previousMouseState = Mouse.GetState();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                if (currentKeyboardState.IsKeyDown(Keys.D1) && !previousKeyboardState.IsKeyDown(Keys.D1))
                {
                    selectedInventorySlot = 0;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                if (currentKeyboardState.IsKeyDown(Keys.D2) && !previousKeyboardState.IsKeyDown(Keys.D2))
                {
                    selectedInventorySlot = 1;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                if (currentKeyboardState.IsKeyDown(Keys.D3) && !previousKeyboardState.IsKeyDown(Keys.D3))
                {
                    selectedInventorySlot = 2;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                if (currentKeyboardState.IsKeyDown(Keys.D4) && !previousKeyboardState.IsKeyDown(Keys.D4))
                {
                    selectedInventorySlot = 3;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D5))
            {
                if (currentKeyboardState.IsKeyDown(Keys.D5) && !previousKeyboardState.IsKeyDown(Keys.D5))
                {
                    selectedInventorySlot = 4;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D6))
            {
                if (currentKeyboardState.IsKeyDown(Keys.D6) && !previousKeyboardState.IsKeyDown(Keys.D6))
                {
                    selectedInventorySlot = 5;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D7))
            {
                if (currentKeyboardState.IsKeyDown(Keys.D7) && !previousKeyboardState.IsKeyDown(Keys.D7))
                {
                    selectedInventorySlot = 6;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                if (currentKeyboardState.IsKeyDown(Keys.X) && !previousKeyboardState.IsKeyDown(Keys.X))
                {
                    if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot] is Weapon)
                    { //if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot].GetType() == typeof(Weapon) && Inventory[selectedInventorySlot] is Weapon) {
                        Weapon toBeSwitchedWeapon = ActiveWeapon;
                        ActiveWeapon = (Weapon)Inventory[selectedInventorySlot];
                        Inventory[selectedInventorySlot] = toBeSwitchedWeapon;
                    }
                }
            }

            if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                if (currentMouseState.MiddleButton == ButtonState.Pressed && !(previousMouseState.MiddleButton == ButtonState.Pressed))
                {
                    if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot] is Weapon)
                    { //if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot].GetType() == typeof(Weapon) && Inventory[selectedInventorySlot] is Weapon) {
                        Weapon toBeSwitchedWeapon = ActiveWeapon;
                        ActiveWeapon = (Weapon)Inventory[selectedInventorySlot];
                        Inventory[selectedInventorySlot] = toBeSwitchedWeapon;
                    }
                }
            }

            #endregion Input: Inventory



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

                flipTexture = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                
                flipTexture = false;
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
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
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


        public bool PlayerDeathAnimation()
        {
            Texture = animationManager.DeathAnimation();
            return animationManager.DeathAnimationFinished();
        }
    }
}
