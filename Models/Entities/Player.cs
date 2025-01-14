using GameStateManagementSample.Models;
using GameStateManagementSample.Models.GameLogic;
using GameStateManagementSample.Models.GUI;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GameStateManagement;
using System;
using GameStateManagementSample.Models.Helpers;


namespace GameStateManagementSample.Models.Entities
{
    public class Player : Entity
    {
        private int selectedInventorySlot = 0;
        public int SelectedInventorySlot { get { return this.selectedInventorySlot; } set { selectedInventorySlot = value; } }
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState = Keyboard.GetState();
        private MouseState currentMouseState;
        private MouseState previousMouseState = Mouse.GetState();
        private Engine gameEngine;
        private bool showStats = false;
        public bool ShowStats
        {
            get
            {
                return this.showStats;
            }
            set
            {
                this.showStats = value;
            }
        }
        private bool showControls = false;
        public bool ShowControls
        {
            get
            {
                return this.showControls;
            }
            set
            {
                this.showControls = value;
            }
        }

        public static int totalScore;
        public bool HasKey { get; set; } = false;

        public Player() { }
        public Player(int healthPoints, float movementSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items, Engine engine)
        : base(healthPoints, movementSpeed, playerPosition, texture, spriteFont, items, engine)
        {
            GUIObservers.Add(new HealthGUI(this));
            GUIObservers.Add(new SpeedBuffGUI(this));
            GUIObservers.Add(new FloatingHealthNumbers(this));
            GUIObservers.Add(new KeyGUI(this));
            gameEngine = engine;
        }


        public override void Move(Vector2 movement)
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            #region Atacking Timer


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

            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                if (currentKeyboardState.IsKeyDown(Keys.F1) && !previousKeyboardState.IsKeyDown(Keys.F1))
                {
                    // showControlsAndStats = showControlsAndStats ? false : true ;
                    showStats = !showStats;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                if (currentKeyboardState.IsKeyDown(Keys.F2) && !previousKeyboardState.IsKeyDown(Keys.F2))
                {
                    showControls = !showControls;
                }
            }

            // Pick up items
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                if (currentKeyboardState.IsKeyDown(Keys.F) && !previousKeyboardState.IsKeyDown(Keys.F))
                {
                    pickUpItems();
                }
            }
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (currentMouseState.RightButton == ButtonState.Pressed && !(previousMouseState.RightButton == ButtonState.Pressed))
                {
                    pickUpItems();
                }
            }

            // Pick up items
            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                if (currentKeyboardState.IsKeyDown(Keys.G) && !previousKeyboardState.IsKeyDown(Keys.G))
                {
                    dropItem();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                if (currentKeyboardState.IsKeyDown(Keys.X) && !previousKeyboardState.IsKeyDown(Keys.X))
                {
                    if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot] is Weapon)
                    { //if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot].GetType() == typeof(Weapon) && Inventory[selectedInventorySlot] is Weapon) {
                        if (Inventory[selectedInventorySlot] is RangedWeapon)
                        {
                            // Engine.playSoundBowEquip1();
                            // GameplayScreen.GameEngine.bowEquip1.Play();
                            gameEngine.bowEquip1.Play();
                        }
                        if (Inventory[selectedInventorySlot] is MeleeWeapon)
                        {
                            // Engine.playSoundBowEquip1();
                            // GameplayScreen.GameEngine.bowEquip1.Play();
                            gameEngine.swordEquip1.Play();
                        }
                        Weapon toBeSwitchedWeapon = ActiveWeapon;
                        ActiveWeapon = (Weapon)Inventory[selectedInventorySlot];
                        Inventory[selectedInventorySlot] = toBeSwitchedWeapon;
                    }

                    else if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot] is Item)
                    {
                        Inventory[selectedInventorySlot].use();
                        Inventory[selectedInventorySlot] = null;
                        NotifyObservers();
                    }
                }
            }

            if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                if (currentMouseState.MiddleButton == ButtonState.Pressed && !(previousMouseState.MiddleButton == ButtonState.Pressed))
                {
                    if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot] is Weapon)
                    { //if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot].GetType() == typeof(Weapon) && Inventory[selectedInventorySlot] is Weapon) {
                        if (Inventory[selectedInventorySlot] is RangedWeapon)
                        {
                            // Engine.playSoundBowEquip1();
                            // GameplayScreen.GameEngine.bowEquip1.Play();
                            gameEngine.bowEquip1.Play();
                        }
                        if (Inventory[selectedInventorySlot] is MeleeWeapon)
                        {
                            // Engine.playSoundBowEquip1();
                            // GameplayScreen.GameEngine.bowEquip1.Play();
                            gameEngine.swordEquip1.Play();
                        }
                        Weapon toBeSwitchedWeapon = ActiveWeapon;
                        ActiveWeapon = (Weapon)Inventory[selectedInventorySlot];
                        Inventory[selectedInventorySlot] = toBeSwitchedWeapon;
                    }

                    else if (Inventory[selectedInventorySlot] != null && Inventory[selectedInventorySlot] is Item)
                    {
                        Inventory[selectedInventorySlot].use();
                        Inventory[selectedInventorySlot] = null;
                        NotifyObservers();
                    }
                }
            }

            #endregion Input: Inventory



            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                // Attack with activeWeapon
                Atack();
            }



            #endregion


            Position += movement;
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;
        }

        private void dropItem()
        {
            if (inventory[selectedInventorySlot] != null) {
                inventory[selectedInventorySlot].Position = this.Position;
                gameEngine.WorldConsumables.Add(inventory[selectedInventorySlot]);
                inventory[selectedInventorySlot] = null;
            }
        }

        private void pickUpItems()
        {
            // foreach (Item itemToPickUp in gameEngine.WorldConsumables) {

            // }
            Item itemToPickUp = CollisionDetector.HasItemCollision(gameEngine.WorldConsumables, this);
            if (itemToPickUp != null)
            {
                for (int i = 0; i < this.Inventory.Length; i++)
                {
                    if (this.Inventory[i] == null)
                    {
                        itemToPickUp.ItemOwner = this;
                        this.Inventory[i] = itemToPickUp;
                        gameEngine.WorldConsumables.Remove(itemToPickUp);
                        break;
                    }
                }
            }
        }

        public override void Atack()
        {
            ActiveWeapon.weaponAttack(this);
        }

        public void UseKey()
        {
            HasKey = false;
            NotifyObservers();
        }

        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i <= 7; i++)
                animManager.walk.addFrame(content.Load<Texture2D>("Player/walk_frames/walk" + i));
            for (int i = 0; i <= 4; i++)
                animManager.attack.addFrame(content.Load<Texture2D>("Player/attack_frames/attack" + i));
            for (int i = 8; i <= 13; i++)
                animManager.shot.addFrame(content.Load<Texture2D>("Player/shot_frames/shot" + i));
            for (int i = 0; i <= 8; i++)
                animManager.idle.addFrame(content.Load<Texture2D>("Player/idle_frames/idle" + i));
            for (int i = 0; i <= 4; i++)
                animManager.death.addFrame(content.Load<Texture2D>("Player/death_frames/death" + i));


            animManager.walk.ChangeAnimationDuration(3);
            animManager.attack.ChangeAnimationDuration(3);
            animManager.idle.ChangeAnimationDuration(2);
            animManager.death.ChangeAnimationDuration(8);
            animManager.shot.ChangeAnimationDuration(2);
            Texture = animManager.IdleAnimation();


            foreach (var observer in GUIObservers)
            {
                if (observer is HealthGUI healthGUI)
                    healthGUI.Texture = content.Load<Texture2D>("Player/HPTexture");

                if (observer is KeyGUI keyGUI)
                    keyGUI.KeyTexture = content.Load<Texture2D>("Items/Key");

                if (observer is SpeedBuffGUI speedBuffGUI)
                    speedBuffGUI.Texture = content.Load<Texture2D>("Player/RunTexture");
            }
        }

        public override void FlipTexture()
        {
            // Filip texture based on mouse position
            Vector2 mousePos = Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y),
                Matrix.Invert(Camera.Transform));

            if (mousePos.X > Position.X)
                flipTexture = false;
            else if (mousePos.X < Position.X)
                flipTexture = true;
            //-------
        }
    }
}
