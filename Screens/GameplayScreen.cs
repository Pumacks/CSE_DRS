#region File Description

//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements


using GameStateManagementSample.Models.GameLogic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using Color = Microsoft.Xna.Framework.Color;

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
        private float pauseAlpha;

        private Engine gameEngine = new Engine();

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


            gameEngine.LoadContent(ScreenManager);



            // Creating a List-Object for enemies
            // Enemies = new List<Enemy>();
            //The following is just for testing Textures and rotations.
            // Projectiles = new List<Projectile>();



            // hero = new Player(100, 5, new Vector2(5000, 5000), golem, gameFont, new List<Item>());

            //  hero.Camera = camera;






            // Loading the Texture for Arrows


            // Placing a few arrows in the world to demonstrate the way they fly:
            // for (int arrowPlacementIndex = 0; arrowPlacementIndex < 100; arrowPlacementIndex++)
            // {
            //     Projectiles.Add(new Projectile(null, ArrowTexture, null, new Vector2(arrowPlacementIndex*10,200), new Vector2(1000, 500), 500));
            // }








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
            gameEngine.UnloadContent();
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

                gameEngine.Update(gameTime);

                // Apply some random jitter to make the enemy move around.

                /*
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
                */


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
                gameEngine.HandleInput(keyboardState, ControllingPlayer, ScreenManager);
            }
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // stage 1 RGB: 149, 153, 28
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0); // Student's Note: Background of the world during gameplay


            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;




            gameEngine.Draw(spriteBatch, ScreenManager, gameTime);






            // Bows, Projectiles, GameTime







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