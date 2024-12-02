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
using GameStateManagementSample.Models.Room;
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
        Player hero2;
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
        GUI gui;
        // Dummy Texture
        Texture2D _texture;
        Texture2D ArrowTexture;
        Texture2D BowTexture;

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
        /// 


        Room room = new Room("../../../Map/rooms/Room1.txt");
        // Texture2D grass;








        



        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");


            camera = new Camera();
             
           
           
           
            _texture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.DarkSlateGray });

            gameFont = content.Load<SpriteFont>("gamefont");

            //GameStateManagementGame.ShipTexture = Content.Load<Texture2D>("laser");

            golem = content.Load<Texture2D>("Player/WalkRight/Golem_03_Walking_000");

            gui = new GUI(ScreenManager.SpriteBatch, content,gameFont);


            hero = new Player(100, 5, new Vector2(500, 400), golem, new List<Item>());
            hero2 = new Player(100, 5, new Vector2(200, 200), golem, new List<Item>());
            hero.CameraProperty = camera;






            // Loading the Texture for Arrows
            ArrowTexture = content.Load<Texture2D>("ArrowSmall7x68px");
            BowTexture = content.Load<Texture2D>("Bow1-130x25px");
            MarkerTexture = content.Load<Texture2D>("Marker");
            // Creating a List-Object for enemies
            Enemies = new List<Enemy>();
            //The following is just for testing Textures and rotations.
            Projectiles = new List<Projectile>();
            for (int arrowPlacementIndex = 0; arrowPlacementIndex < 100; arrowPlacementIndex++)
            {
                Projectiles.Add(new Projectile(null, ArrowTexture, null, new Vector2(arrowPlacementIndex*10,200), new Vector2(1000, 500), 500));
            }
            //Giving our Test-Hero a Weapon (bow) at the start (without a texture), so he can shoot arrows!
            hero.ActiveWeapon = new RangedWeapon(
                "Bow of the Gods",
                BowTexture,
                hero,
                20,
                16,
                1000,
                2000,
                ArrowTexture,
                Projectiles
            );

           





            room.loadTextures(content);

            hero.LoadContent(content);
           // hero2.LoadContent(content);

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
                    Projectiles[projectileUpdateIndex].CurrentProjectilePosition += Projectiles[projectileUpdateIndex].SpeedVector/60;
                }
            }

            // Updating the Weapon-Classes necessary awareness of enemies and projectiles in the world.
            hero.ActiveWeapon.Enemies = Enemies;
            this.Projectiles = hero.ActiveWeapon.Projectiles;



                
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
                hero.Move();
                

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

        
            if (CollisionDetector.IsIntersecting(hero.BoundingBox, hero2.BoundingBox))
                ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Navy, 0, 0);
            else
            {
                ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);
            }
                
            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;


          
 
            spriteBatch.Begin(transformMatrix: camera.Transform);
           
            // DrawGui Bounding box
            spriteBatch.Draw(_texture, new Rectangle(hero.BoundingBox.X, hero.BoundingBox.Y, hero.BoundingBox.Width, 1), Color.Black); 
            spriteBatch.Draw(_texture, new Rectangle(hero.BoundingBox.X, hero.BoundingBox.Y, 1, hero.BoundingBox.Height), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(hero.BoundingBox.Right - 1, hero.BoundingBox.Y, 1, hero.BoundingBox.Height), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(hero.BoundingBox.X, hero.BoundingBox.Bottom - 1, hero.BoundingBox.Width, 1), Color.Black);

            spriteBatch.Draw(_texture, new Rectangle(hero2.BoundingBox.X, hero2.BoundingBox.Y, hero2.BoundingBox.Width, 1), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(hero2.BoundingBox.X, hero2.BoundingBox.Y, 1, hero2.BoundingBox.Height), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(hero2.BoundingBox.Right - 1, hero2.BoundingBox.Y, 1, hero2.BoundingBox.Height), Color.Black);
            spriteBatch.Draw(_texture, new Rectangle(hero2.BoundingBox.X, hero2.BoundingBox.Bottom - 1, hero2.BoundingBox.Width, 1), Color.Black);

            room.Draw(spriteBatch);

            hero.Draw(spriteBatch);
           // hero2.Draw(spriteBatch);


            

            spriteBatch.End();



            spriteBatch.Begin();

            gui.DrawGui(hero);

            spriteBatch.End();




            // Bows, Projectiles, GameTime
            
            
            
            
            spriteBatch.Begin();
            spriteBatch.Draw(
                texture: BowTexture,
                position: Vector2.Transform(hero.Position,camera.Transform), // Hier Vector2.Transform(hero.Position,camera.Transform) anstatt new Vector2(ScreenManager.GraphicsDevice.Viewport.Width/2,ScreenManager.GraphicsDevice.Viewport.Height/2)
                sourceRectangle: null,
                color: Color.White,
                rotation: (float)Math.PI/2+(float)Math.Atan2(Mouse.GetState().Y - Vector2.Transform(hero.Position,camera.Transform).Y, Mouse.GetState().X - Vector2.Transform(hero.Position,camera.Transform).X),
                // rotation: hero.ActiveWeapon.calculateWeaponRotation(),
                // rotation: calculateWeaponRotation(hero.Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y)),
                origin: new Vector2(BowTexture.Width / 2, BowTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();


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


           


            if (Projectiles != null)
            {
                spriteBatch.Begin();
                int projectileIndex;
                for (projectileIndex = 0; projectileIndex < Projectiles.Count; projectileIndex++)
                {
                    spriteBatch.Draw(
                        texture: ArrowTexture,
                        position: Vector2.Transform(Projectiles[projectileIndex].CurrentProjectilePosition,camera.Transform), // Hier Vector2.Transform(Projectiles[projectileIndex].CurrentProjectilePosition,camera.Transform) anstatt Projectiles[projectileIndex].CurrentProjectilePosition
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