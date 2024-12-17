#region File Description

//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using GameStateManagementSample.Models;
using GameStateManagementSample.Models.Camera;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.Helpers;
using GameStateManagementSample.Models.Items;
using GameStateManagementSample.Models.Map;
using GameStateManagementSample.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

#endregion Using Statements

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    internal class GameplayScreen : GameScreen
    {
        #region Fields

        private ContentManager content;
        private SpriteFont gameFont;

        Texture2D golem;
        Player hero;

        Camera camera;

        public Camera CameraProperty
        {
            get
            {
                return this.camera;
            }
            set
            {
                this.camera = value;
            }
        }



        // Dummy Texture
        Texture2D _texture;
        Texture2D ArrowTexture;
        Texture2D BowTexture;
        Texture2D SwordTexture;
        Texture2D InventoryTexture;
        Texture2D SelectedInventorySlotTexture;
        Texture2D ActiveWeaponInventorySlotTexture;

        Texture2D MarkerTexture;

        private Vector2 playerPosition = new Vector2(100, 100);
        private Vector2 enemyPosition = new Vector2(100, 100);

        private Random random = new Random();
        private float pauseAlpha;



        #region Fields and properties required for keeping track of enemies, player and projectile
        private List<Enemy> enemies;
        public List<Enemy> Enemies
        {
            get
            {
                return this.enemies;
            }
            set
            {
                this.enemies = value;
            }
        }
        /*
        private Entity player;
        public Entity Player
        {
            get
            {
                return this.player;
            }
            set
            {
                this.player = value;
            }
        }
        */
        private List<Projectile> projectiles;
        public List<Projectile> Projectiles
        {
            get
            {
                return this.projectiles;
            }
            set
            {
                this.projectiles = value;
            }
        }
        #endregion Fields and properties required for keeping track of enemies, player and projectile





        Room room = new Room();
        MapGenerator map = new MapGenerator();


        #endregion Fields









        #region Initialization

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// LoadContent graphics content for the game.
        /// </summary>


        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");


            camera = new Camera();



            _texture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.DarkSlateGray });

            gameFont = content.Load<SpriteFont>("gamefont");




            golem = content.Load<Texture2D>("Player/WalkRight/Golem_03_Walking_000");


            // Creating a List-Object for enemies
            Enemies = new List<Enemy>();
            //The following is just for testing Textures and rotations.
            Projectiles = new List<Projectile>();



            hero = new Player(100, 5, new Vector2(5000, 5000), golem, gameFont, new List<Item>());

            hero.CameraProperty = camera;






            // Loading the Texture for Arrows
            ArrowTexture = content.Load<Texture2D>("ArrowSmall7x68px");
            BowTexture = content.Load<Texture2D>("Bow1-130x25px");
            SwordTexture = content.Load<Texture2D>("sword1_130x27px");
            InventoryTexture = content.Load<Texture2D>("966x138 Inventory Slot Bar v2.1");
            SelectedInventorySlotTexture = content.Load<Texture2D>("138x138 Inventory Slot v2.1 Selected v3.2");
            ActiveWeaponInventorySlotTexture = content.Load<Texture2D>("138x138 Inventory Slot Coloured v3.5");
            MarkerTexture = content.Load<Texture2D>("Marker");

            // Placing a few arrows in the world to demonstrate the way they fly:
            // for (int arrowPlacementIndex = 0; arrowPlacementIndex < 100; arrowPlacementIndex++)
            // {
            //     Projectiles.Add(new Projectile(null, ArrowTexture, null, new Vector2(arrowPlacementIndex*10,200), new Vector2(1000, 500), 500));
            // }


            //Giving our Test-Hero a Weapon (bow) at the start, so he can shoot arrows!
            hero.ActiveWeapon = new RangedWeapon(
                "Bow of the Gods",
                BowTexture,
                hero,
                20,
                400,
                1000,
                Enemies,
                1500,
                ArrowTexture,
                Projectiles
            );
            //Giving our Test-Hero's inventory a Weapon (sword) at the start, so he can choose between sword and bow!
            hero.Inventory[0] = new MeleeWeapon(
                "Sword of the Gods",
                SwordTexture,
                hero,
                40,
                400,
                250,
                Enemies
            );






            map.LoadMapTextures(content);
            map.GenerateMap();
            hero.LoadContent(content);

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }

        #endregion Initialization









        #region Update and Draw

        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            hero.SetGameTime(gameTime);
            camera.Follow(hero);

            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
            {
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            }
            else
            {
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);
            }

            if (IsActive)
            {
                // Apply some random jitter to make the enemy move around.


                // Apply a stabilizing force to stop the enemy moving off the screen.
                Vector2 targetPosition = new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width / 2 - gameFont.MeasureString("Insert Gameplay Here").X / 2,
                    200);

                enemyPosition = Vector2.Lerp(enemyPosition, targetPosition, 0.05f);



                // Updating the positions of the projectiles (arrows) in the world
                if (Projectiles != null)
                {
                    int projectileUpdateIndex;
                    for (projectileUpdateIndex = 0; projectileUpdateIndex < Projectiles.Count; projectileUpdateIndex++)
                    {
                        Projectiles[projectileUpdateIndex].CurrentProjectilePosition += Projectiles[projectileUpdateIndex].SpeedVector / 60;
                        Projectiles[projectileUpdateIndex].DistanceCovered += Projectiles[projectileUpdateIndex].Velocity / 60;
                        if (Projectiles[projectileUpdateIndex].DistanceCovered >= Projectiles[projectileUpdateIndex].ProjectileRange)
                        {
                            Projectiles.RemoveAt(projectileUpdateIndex);
                        }
                    }
                }




            }
        }

        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {

                if (hero.HealthPoints <= 0)
                {
                    hero.PlayerDeathAnimation();

                    ScreenManager.AddScreen(new GameOverScreen(true), ControllingPlayer);
                }
                else
                {
                    hero.Move();
                }

                //if (movement.Length() > 1)
                //{
                //    movement.Normalize();
                //}

            }
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {

            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);


            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;



            spriteBatch.Begin(transformMatrix: camera.Transform);

            // DrawGui Bounding box
            spriteBatch.Draw(_texture, new Rectangle(hero.BoundingBox.X, hero.BoundingBox.Y, hero.BoundingBox.Width, 1), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(hero.BoundingBox.X, hero.BoundingBox.Y, 1, hero.BoundingBox.Height), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(hero.BoundingBox.Right - 1, hero.BoundingBox.Y, 1, hero.BoundingBox.Height), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(hero.BoundingBox.X, hero.BoundingBox.Bottom - 1, hero.BoundingBox.Width, 1), Color.Black);

            //room.DrawRoom(spriteBatch);
            map.DrawMap(spriteBatch);
            hero.Draw(spriteBatch);
            spriteBatch.End();




            // Bows, Projectiles, GameTime




            spriteBatch.Begin();
            spriteBatch.Draw(
                texture: this.hero.ActiveWeapon.ItemTexture,
                position: Vector2.Transform(hero.Position, camera.Transform), // Hier Vector2.Transform(hero.Position,camera.Transform) anstatt new Vector2(ScreenManager.GraphicsDevice.Viewport.Width/2,ScreenManager.GraphicsDevice.Viewport.Height/2)
                sourceRectangle: null,
                color: Color.White,
                rotation: (float)Math.PI / 2 + (float)Math.Atan2(Mouse.GetState().Y - Vector2.Transform(hero.Position, camera.Transform).Y, Mouse.GetState().X - Vector2.Transform(hero.Position, camera.Transform).X),
                // rotation: hero.ActiveWeapon.calculateWeaponRotation(),
                // rotation: calculateWeaponRotation(hero.Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y)),
                origin: this.hero.ActiveWeapon is MeleeWeapon ? new Vector2(this.hero.ActiveWeapon.ItemTexture.Width / 2, this.hero.ActiveWeapon.ItemTexture.Height * 0.75f) : new Vector2(this.hero.ActiveWeapon.ItemTexture.Width / 2, this.hero.ActiveWeapon.ItemTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();

            // to draw the inventory
            spriteBatch.Begin();
            spriteBatch.Draw(
                texture: InventoryTexture,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.5f, ScreenManager.GraphicsDevice.Viewport.Height - 138),
                sourceRectangle: null,
                color: Color.White,
                rotation: 0f,
                origin: new Vector2(InventoryTexture.Width / 2, InventoryTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();

            // to draw the currently selected inventory slot
            spriteBatch.Begin();
            spriteBatch.Draw(
                texture: SelectedInventorySlotTexture,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.5f + this.hero.SelectedInventorySlot * 138, ScreenManager.GraphicsDevice.Viewport.Height - 138),
                sourceRectangle: null,
                color: Color.White,
                rotation: 0f,
                origin: new Vector2(InventoryTexture.Width / 2, InventoryTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();

            // to draw the active inventory slot
            spriteBatch.Begin();
            spriteBatch.Draw(
                texture: ActiveWeaponInventorySlotTexture,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.5f - 276, ScreenManager.GraphicsDevice.Viewport.Height - 138),
                sourceRectangle: null,
                color: Color.White,
                rotation: 0f,
                origin: new Vector2(InventoryTexture.Width / 2, InventoryTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();

            // To draw the active weapon's texture in its intended slot
            spriteBatch.Begin();
            spriteBatch.Draw(
                texture: this.hero.ActiveWeapon.ItemTexture,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.5f - 690, ScreenManager.GraphicsDevice.Viewport.Height - 138),
                sourceRectangle: null,
                color: Color.White,
                rotation: 0.785398f,
                origin: new Vector2(this.hero.ActiveWeapon.ItemTexture.Width / 2, this.hero.ActiveWeapon.ItemTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();

            // to draw each item in the inventory into their intended slots
            for (int inventorySlotCounter = 0; inventorySlotCounter < this.hero.Inventory.Length; inventorySlotCounter++)
            {
                if (this.hero.Inventory[inventorySlotCounter] != null)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(
                        texture: this.hero.Inventory[inventorySlotCounter].ItemTexture,
                        position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.5f - 414 + inventorySlotCounter * 138, ScreenManager.GraphicsDevice.Viewport.Height - 138),
                        sourceRectangle: null,
                        color: Color.White,
                        rotation: 0.785398f,
                        origin: new Vector2(this.hero.Inventory[inventorySlotCounter].ItemTexture.Width / 2, this.hero.Inventory[inventorySlotCounter].ItemTexture.Height / 2),
                        scale: 1f,
                        effects: SpriteEffects.None,
                        layerDepth: 0f);
                    spriteBatch.End();
                }
            }




            //Test to find out that the hero's position is not correctly the same as the hero's texture's position. I suppose this might be due to the camera following stuff.
            // spriteBatch.Begin();
            // spriteBatch.Draw(
            //     texture: MarkerTexture,
            //     position: Vector2.Transform(hero.Position,camera.Transform), // Hier Vector2.Transform(hero.Position, camera.Transform) anstatt hero.Position
            //     sourceRectangle: null,
            //     color: Color.White,
            //     rotation: 0f,
            //     // rotation: hero.ActiveWeapon.calculateWeaponRotation(),
            //     // rotation: calculateWeaponRotation(hero.Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y)),
            //     origin: new Vector2(MarkerTexture.Width / 2, MarkerTexture.Height),
            //     scale: 1f,
            //     effects: SpriteEffects.None,
            //     layerDepth: 0f);
            // spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Total ms: " + gameTime.TotalGameTime.TotalMilliseconds.ToString(),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f, ScreenManager.GraphicsDevice.Viewport.Height - 250),
                Color.Red
            );
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Hero Position: " + hero.Position.ToString(),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f, ScreenManager.GraphicsDevice.Viewport.Height - 300),
                Color.Red
            );
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Hero Transformed Position: " + Vector2.Transform(hero.Position, camera.Transform).ToString(),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f, ScreenManager.GraphicsDevice.Viewport.Height - 350),
                Color.Red
            );
            spriteBatch.End();



            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                // text: "M.Cursor Transformed Inverted: " + Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(hero.CameraProperty.Transform)),
                text: "Mouse aiming at: " + Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(hero.CameraProperty.Transform)),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f, ScreenManager.GraphicsDevice.Viewport.Height - 400),
                Color.Red
            );
            spriteBatch.End();



            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "selectedInventorySlot: " + this.hero.SelectedInventorySlot,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f, ScreenManager.GraphicsDevice.Viewport.Height - 450),
                Color.Red
            );
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Mouse Wheel Value: " + Mouse.GetState().ScrollWheelValue,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f, ScreenManager.GraphicsDevice.Viewport.Height - 500),
                Color.Red
            );
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Mouse Wheel PREVIOUS Value: " + Mouse.GetState().ScrollWheelValue,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f, ScreenManager.GraphicsDevice.Viewport.Height - 550),
                Color.Red
            );
            spriteBatch.End();





            if (Projectiles != null)
            {
                spriteBatch.Begin();
                int projectileIndex;
                for (projectileIndex = 0; projectileIndex < Projectiles.Count; projectileIndex++)
                {
                    spriteBatch.Draw(
                        texture: ArrowTexture,
                        position: Vector2.Transform(Projectiles[projectileIndex].CurrentProjectilePosition, camera.Transform), // Hier Vector2.Transform(Projectiles[projectileIndex].CurrentProjectilePosition,camera.Transform) anstatt Projectiles[projectileIndex].CurrentProjectilePosition
                        sourceRectangle: null,
                        color: Color.White,
                        //rotation: (float) projectileIndex*2*(float)Math.PI/100, //normally it depends on the shooting direction of the arrow. This line is currently just for testing purposes.
                        rotation: Projectiles[projectileIndex].ProjectileRotationFloatValue,
                        origin: new Vector2(ArrowTexture.Width / 2, ArrowTexture.Height / 2),
                        scale: 1f,
                        effects: SpriteEffects.None,
                        layerDepth: 0f
                        );
                }
                spriteBatch.End();
            }





            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        #endregion Update and Draw
    }
}