using GameStateManagement;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.Items;
using GameStateManagementSample.Models.Map;
using GameStateManagementSample.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using GameStateManagementSample.Screens;

namespace GameStateManagementSample.Models.GameEngine
{
    public class Engine
    {

        #region Fields
        private ContentManager content;
        private SpriteFont gameFont;

        Camera.Camera camera;
        Texture2D golem;
        Player hero;


        private Room room;
        private MapGenerator map;
        public Camera.Camera CameraProperty
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

        Texture2D MarkerTexture;

        private Vector2 playerPosition = new Vector2(100, 100);
        private Vector2 enemyPosition = new Vector2(100, 100);

        private Random random = new Random();
 

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



        public Engine()
        {
        }


        public void LoadContent(ScreenManager ScreenManager)
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");


            camera = new Camera.Camera();
            room = new Room();
            map = new MapGenerator();


            _texture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            _texture.SetData(new Color[] { Color.DarkSlateGray });

            gameFont = content.Load<SpriteFont>("gamefont");




            golem = content.Load<Texture2D>("Player/WalkRight/Golem_03_Walking_000");



            hero = new Player(100, 5, new Vector2(5000, 5000), golem, gameFont, new List<Item>());

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
                Projectiles.Add(new Projectile(null, ArrowTexture, null, new Vector2(arrowPlacementIndex * 10, 200), new Vector2(1000, 500), 500));
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


            map.LoadMapTextures(content);
            map.GenerateMap();
            hero.LoadContent(content);


            Thread.Sleep(1000);



        }


        public void UnloadContent()
        {
            content.Unload();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
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
                texture: BowTexture,
                position: Vector2.Transform(hero.Position, camera.Transform), // Hier Vector2.Transform(hero.Position,camera.Transform) anstatt new Vector2(ScreenManager.GraphicsDevice.Viewport.Width/2,ScreenManager.GraphicsDevice.Viewport.Height/2)
                sourceRectangle: null,
                color: Color.White,
                rotation: (float)Math.PI / 2 + (float)Math.Atan2(Mouse.GetState().Y - Vector2.Transform(hero.Position, camera.Transform).Y, Mouse.GetState().X - Vector2.Transform(hero.Position, camera.Transform).X),
                // rotation: hero.ActiveWeapon.calculateWeaponRotation(),
                // rotation: calculateWeaponRotation(hero.Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y)),
                origin: new Vector2(BowTexture.Width / 2, BowTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
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

        }


        public void Update(GameTime gameTime)
        {
            hero.SetGameTime(gameTime);
            camera.Follow(hero);

            // Updating the positions of the projectiles (arrows) in the world
            if (Projectiles != null)
            {
                int projectileUpdateIndex;
                for (projectileUpdateIndex = 0; projectileUpdateIndex < Projectiles.Count; projectileUpdateIndex++)
                {
                    Projectiles[projectileUpdateIndex].CurrentProjectilePosition += Projectiles[projectileUpdateIndex].SpeedVector / 60;
                }
            }

            // Updating the Weapon-Classes necessary awareness of enemies and projectiles in the world.
            hero.ActiveWeapon.Enemies = Enemies;
            this.Projectiles = hero.ActiveWeapon.Projectiles;
        }

        public void HandleInput(InputState input, PlayerIndex? controllingPlayer, ScreenManager screenManager)
        {

            if (hero.HealthPoints <= 0)
            {
                hero.PlayerDeathAnimation();

                screenManager.AddScreen(new GameOverScreen(true), controllingPlayer);
            }
            else
            {
                hero.Move();
            }
        }

    }
}
