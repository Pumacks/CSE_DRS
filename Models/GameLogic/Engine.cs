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
using System.Diagnostics;
using System.Threading;
using GameStateManagementSample.Models.Helpers;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Xna.Framework.Audio;
using System.Runtime.Serialization;


enum PlayerGameStatus
{
    ALIVE,
    DEAD,
    WON
}

namespace GameStateManagementSample.Models.GameLogic
{
    public class Engine
    {

        #region Fields

        private ContentManager content;
        private SpriteFont gameFont;

        Models.Camera camera;
        Texture2D initTexture;
        Player hero;
        int stage = 1;


        private List<Item> worldConsumables;

        private PlayerGameStatus playerGameStatus = PlayerGameStatus.ALIVE;
        private bool deathAnimationFinished = false;

        //private Room room;
        private MapGenerator map;

        public Models.Camera CameraProperty
        {
            get { return camera; }
            set { camera = value; }
        }


        // Dummy Texture
        Texture2D ArrowTexture;
        Texture2D SwordTexture;
        Texture2D InventoryTexture;
        Texture2D SelectedInventorySlotTexture;
        Texture2D ActiveWeaponInventorySlotTexture;
        Texture2D BowTexture;

        // Texture2D MarkerTexture;
        Texture2D HealthPotion;
        Texture2D SpeedPotion;



        public SoundEffect bowEquip1;
        public SoundEffect bowEquip2;
        public SoundEffect bowShoot1;
        public SoundEffect bowShoot2;
        public SoundEffect bowShoot3;
        public SoundEffect swordEquip1;
        public SoundEffect swordSwing1;
        public SoundEffect swordSwing2;
        public SoundEffect swordHit1;
        public SoundEffect swordHit2;


        private Random random = new Random();




        #region Fields and properties required for keeping track of enemies, player and projectile

        private List<Enemy> enemies;

        public List<Enemy> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }

        private Ai ai;


        private List<Projectile> projectiles;

        public List<Projectile> Projectiles
        {
            get { return projectiles; }
            set { projectiles = value; }
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

            camera = new Models.Camera();
            map = new MapGenerator();
            worldConsumables = new List<Item>();
            Enemies = new List<Enemy>();


            gameFont = content.Load<SpriteFont>("gamefont");
            initTexture = content.Load<Texture2D>("Player/idle_frames/idle0");

            hero = new Player(100, 5, new Vector2(5300, 5300), initTexture, gameFont, new List<Item>(), this);
            hero.Camera = camera;


            // Loading the Texture for Arrows
            ArrowTexture = content.Load<Texture2D>("ArrowSmall7x68px");
            BowTexture = content.Load<Texture2D>("Bow1-130x25px");
            // Creating a List-Object for enemies
            Enemies = new List<Enemy>();
            // Creating a List-Object for projectiles
            Projectiles = new List<Projectile>();
            //The following is just for testing Textures and rotations.
            //for (int arrowPlacementIndex = 0; arrowPlacementIndex < 100; arrowPlacementIndex++)
            //{
            //    Projectiles.Add(new Projectile(null, ArrowTexture, null, new Vector2(arrowPlacementIndex * 10, 200), new Vector2(1000, 500), 500, 250));
            //}
            //ArrowTexture = content.Load<Texture2D>("ArrowSmall7x68px");
            ArrowTexture = content.Load<Texture2D>("Items/Projectile/Arrow");
            BowTexture = content.Load<Texture2D>("Bow1-130x25px");
            SwordTexture = content.Load<Texture2D>("sword1_130x27px");
            InventoryTexture = content.Load<Texture2D>("966x138 Inventory Slot Bar v2.1");
            SelectedInventorySlotTexture = content.Load<Texture2D>("138x138 Inventory Slot v2.1 Selected v3.2");
            ActiveWeaponInventorySlotTexture = content.Load<Texture2D>("138x138 Inventory Slot Coloured v3.5");
            //MarkerTexture = content.Load<Texture2D>("Marker");
            HealthPotion = content.Load<Texture2D>("Items/Potions/HealthPotion");
            SpeedPotion = content.Load<Texture2D>("Items/Potions/SpeedPotion");

            // Loading Sound Effects
            bowEquip1 = content.Load<SoundEffect>("649332__sonofxaudio__bow_draw_fast01");
            bowEquip2 = content.Load<SoundEffect>("649337__sonofxaudio__bow_draw_fast02");
            bowShoot1 = content.Load<SoundEffect>("649335__sonofxaudio__arrow_loose01");
            bowShoot2 = content.Load<SoundEffect>("649334__sonofxaudio__arrow_loose02");
            bowShoot3 = content.Load<SoundEffect>("649333__sonofxaudio__arrow_loose03");
            swordEquip1 = content.Load<SoundEffect>("draw-sword1-44724");
            swordSwing1 = content.Load<SoundEffect>("sword-swing-whoosh-sound-effect-1-241824");
            swordSwing2 = content.Load<SoundEffect>("sword-swing-whoosh-sound-effect-2-241823");
            // swordHit1 = content.Load<SoundEffect>("");
            // swordHit2 = content.Load<SoundEffect>("");


            //Giving our Test-Hero a Weapon (bow) at the start (without a texture), so he can shoot arrows!
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
                Projectiles,
                this
            );
            //Giving our Test-Hero's inventory a Weapon (sword) at the start, so he can choose between sword and bow!
            hero.Inventory[0] = new MeleeWeapon(
                "Sword of the Gods",
                SwordTexture,
                hero,
                40,
                400,
                250,
                Enemies,
                this
            );

            ai = new Ai();

            map.LoadMapTextures(content);
            map.GenerateMap(content, ref enemies, hero.Camera);
            hero.LoadContent(content);
            enemies.ForEach(enem => enem.Camera = camera);
            enemies.ForEach(enemy1 => enemy1.LoadContent(content));

            Thread.Sleep(1000);
        }


        public void UnloadContent()
        {
            content.Unload();
        }


        public void Draw(SpriteBatch spriteBatch, ScreenManager ScreenManager, GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform);
            map.DrawMap(spriteBatch);
            spriteBatch.End();


            // sortMode: SpriteSortMode.Immediate, blendState: BlendState.Opaque
            spriteBatch.Begin(transformMatrix: camera.Transform);
            foreach (var consumable in worldConsumables)
            {
                consumable.DrawItem(spriteBatch);
            }

            spriteBatch.End();


            hero.Draw(spriteBatch);
            foreach (var enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }



            // Bows, Projectiles, GameTime
            //spriteBatch.Begin();
            //spriteBatch.Draw(
            //    texture: this.hero.ActiveWeapon.ItemTexture,
            //    position: Vector2.Transform(hero.Position,
            //        camera.Transform), // Hier Vector2.Transform(hero.Position,camera.Transform) anstatt new Vector2(ScreenManager.GraphicsDevice.Viewport.Width/2,ScreenManager.GraphicsDevice.Viewport.Height/2)
            //    sourceRectangle: null,
            //    color: Color.White,
            //    rotation: (float)Math.PI / 2 +
            //              (float)Math.Atan2(Mouse.GetState().Y - Vector2.Transform(hero.Position, camera.Transform).Y,
            //                  Mouse.GetState().X - Vector2.Transform(hero.Position, camera.Transform).X),
            //    // rotation: hero.ActiveWeapon.calculateWeaponRotation(),
            //    // rotation: calculateWeaponRotation(hero.Position, new Vector2(Mouse.GetState().X, Mouse.GetState().Y)),
            //    origin: this.hero.ActiveWeapon is MeleeWeapon
            //        ? new Vector2(this.hero.ActiveWeapon.ItemTexture.Width / 2,
            //            this.hero.ActiveWeapon.ItemTexture.Height * 0.75f)
            //        : new Vector2(this.hero.ActiveWeapon.ItemTexture.Width / 2,
            //            this.hero.ActiveWeapon.ItemTexture.Height / 2),
            //    scale: 1f,
            //    effects: SpriteEffects.None,
            //    layerDepth: 0f);
            //spriteBatch.End();



            if (Projectiles != null)
            {
                spriteBatch.Begin();
                int projectileIndex;
                for (projectileIndex = 0; projectileIndex < Projectiles.Count; projectileIndex++)
                {
                    spriteBatch.Draw(
                        texture: ArrowTexture,
                        position: Vector2.Transform(Projectiles[projectileIndex].CurrentProjectilePosition,
                            camera.Transform), // Hier Vector2.Transform(Projectiles[projectileIndex].CurrentProjectilePosition,camera.Transform) anstatt Projectiles[projectileIndex].CurrentProjectilePosition
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


            // to draw the inventory
            spriteBatch.Begin();
            spriteBatch.Draw(
                texture: InventoryTexture,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.5f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 138),
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
                position: new Vector2(
                    ScreenManager.GraphicsDevice.Viewport.Width * 0.5f + this.hero.SelectedInventorySlot * 138,
                    ScreenManager.GraphicsDevice.Viewport.Height - 138),
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
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.5f - 276,
                    ScreenManager.GraphicsDevice.Viewport.Height - 138),
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
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.5f - 690,
                    ScreenManager.GraphicsDevice.Viewport.Height - 138),
                sourceRectangle: null,
                color: Color.White,
                rotation: 0.785398f,
                origin: new Vector2(this.hero.ActiveWeapon.ItemTexture.Width / 2,
                    this.hero.ActiveWeapon.ItemTexture.Height / 2),
                scale: 1f,
                effects: SpriteEffects.None,
                layerDepth: 0f);
            spriteBatch.End();

            // to draw each item in the inventory into their intended slots
            for (int inventorySlotCounter = 0;
                 inventorySlotCounter < this.hero.Inventory.Length;
                 inventorySlotCounter++)
            {
                if (this.hero.Inventory[inventorySlotCounter] != null)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(
                        texture: hero.Inventory[inventorySlotCounter].ItemTexture,
                        position: new Vector2(
                            ScreenManager.GraphicsDevice.Viewport.Width * 0.5f - 414 + inventorySlotCounter * 138,
                            ScreenManager.GraphicsDevice.Viewport.Height - 138),
                        sourceRectangle: null,
                        color: Color.White,
                        rotation: 0.785398f,
                        origin: new Vector2(this.hero.Inventory[inventorySlotCounter].ItemTexture.Width / 2,
                            this.hero.Inventory[inventorySlotCounter].ItemTexture.Height / 2),
                        scale: 1f,
                        effects: SpriteEffects.None,
                        layerDepth: 0f);
                    spriteBatch.End();
                }
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Total ms: " + gameTime.TotalGameTime.TotalMilliseconds.ToString(),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 250),
                Color.Red
            );
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Hero Position: " + hero.Position.ToString(),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 300),
                Color.Red
            );
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Hero Transformed Position: " + Vector2.Transform(hero.Position, camera.Transform).ToString(),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 350),
                Color.Red
            );
            spriteBatch.End();



            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                // text: "M.Cursor Transformed Inverted: " + Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Matrix.Invert(hero.Camera.Transform)),
                text: "Mouse aiming at: " + Vector2.Transform(new Vector2(Mouse.GetState().X, Mouse.GetState().Y),
                    Matrix.Invert(hero.Camera.Transform)),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 400),
                Color.Red
            );
            spriteBatch.End();



            /*
            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "selectedInventorySlot: " + this.hero.SelectedInventorySlot,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 450),
                Color.Red
            );
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Mouse Wheel Value: " + Mouse.GetState().ScrollWheelValue,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 500),
                Color.Red
            );
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Mouse Wheel PREVIOUS Value: " + Mouse.GetState().ScrollWheelValue,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 550),
                Color.Red
            );
            spriteBatch.End();
            */

        }


        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            camera.Follow(hero);
            UpdateEnemies(gameTime);
            CollectItems();


            if (hero.HealthPoints <= 0)
            {
                playerGameStatus = PlayerGameStatus.DEAD;
                deathAnimationFinished = hero.PlayDeathAnimation();
            }


            // Updating the values of all projectiles (arrows) in the world, whether they collide, when they disappear, etc.
            if (Projectiles != null)
            {
                Projectile theProjectile;
                int projectileUpdateIndex;
                for (projectileUpdateIndex = 0; projectileUpdateIndex < Projectiles.Count; projectileUpdateIndex++)
                {
                    theProjectile = Projectiles[projectileUpdateIndex];
                    if (Projectiles[projectileUpdateIndex].DistanceCovered >= Projectiles[projectileUpdateIndex].ProjectileRange)
                    {
                        Projectiles.RemoveAt(projectileUpdateIndex);
                    }
                    else
                    {
                        // Updating each arrow's position
                        if (!theProjectile.IsStuck) theProjectile.CurrentProjectilePosition += Projectiles[projectileUpdateIndex].SpeedVector / 60;
                        // Updating each arrow's covered distance
                        if (!theProjectile.IsStuck) Projectiles[projectileUpdateIndex].DistanceCovered += Projectiles[projectileUpdateIndex].Velocity / 60;

                        // Updating each arrow's hitbox
                        if (!theProjectile.IsStuck) Projectiles[projectileUpdateIndex].ProjectileHitBox = new Rectangle(
                            (int)theProjectile.CurrentProjectilePosition.X - theProjectile.ProjectileTexture.Width / 2,
                            (int)theProjectile.CurrentProjectilePosition.Y - theProjectile.ProjectileTexture.Height / 2,
                            theProjectile.ProjectileTexture.Width,
                            theProjectile.ProjectileTexture.Width
                        );

                        // For each Projectile, checks collision between arrow and enemy hitbox with every enemy (yes, Projectiles * Enemies calculations, O(n²), bad but not extremely bad)
                        Enemies.ForEach(targetEnemy => // remove WeaponDamage amount of HealthPoints from the first Enemy whose hitbox intersects the arrow's hitbox.
                        {
                            if (!theProjectile.IsStuck) // <---------- notice this
                                if (theProjectile.ProjectileHitBox.Intersects(targetEnemy.BoundingBox))
                                {
                                    targetEnemy.TakeDamage(theProjectile.ProjectileDamage);
                                    Projectiles.Remove(theProjectile); // Projectiles.RemoveAt(projectileUpdateIndex); <--- ist fehleranfällig für IndexOutOfBoundsException
                                }
                        });

                        // Checking whether the projectiles collide with any tiles in the map. Sadly this check is currently not possible for only the current room of the player, but only for all rooms.
                        if (!theProjectile.IsStuck)
                            for (int roomNumber = 0; roomNumber < map.Rooms.Length; roomNumber++)
                            {
                                Room currentRoom = map.Rooms[roomNumber];
                                for (int tileCounterX = 0; tileCounterX < currentRoom.GetTiles().GetLength(0); tileCounterX++)
                                {
                                    for (int tileCounterY = 0; tileCounterY < currentRoom.GetTiles().GetLength(1); tileCounterY++)
                                    {
                                        Tile currentTile = currentRoom.GetTiles()[tileCounterX, tileCounterY];
                                        if (currentTile.Collision)
                                            if (theProjectile.ProjectileHitBox.Intersects(currentTile.BoundingBox))
                                            {
                                                theProjectile.IsStuck = true;
                                            }

                                        // At this point, this is O(n³), but that's okay for our purpose, especially since it's all small numbers. And it's still polynomial.
                                    }
                                }
                            }


                    }
                }
            }

            // Updating the rotation value of weapons for displaying them correctly
            hero.ActiveWeapon.WeaponRotationFloatValue = (float)Math.PI / 2 + (float)Math.Atan2(Mouse.GetState().Y - Vector2.Transform(hero.Position, camera.Transform).Y, Mouse.GetState().X - Vector2.Transform(hero.Position, camera.Transform).X);



            // Updating the Weapon-Classes necessary awareness of enemies and projectiles in the world.
            // THIS CAN (and probably should) BE DELETED (if you read this while merging and are unsure, just delete these two lines and two comments)
            // hero.ActiveWeapon.Enemies = Enemies;
            // Projectiles = hero.ActiveWeapon.Projectiles;

            // CollisionDetector.HasArrowCollision(hero, enemies, Projectiles);
        }

        public void HandleInput(KeyboardState keyboardState, PlayerIndex? controllingPlayer,
            ScreenManager screenManager)
        {
            // --- TO BE DELETED ----
            if (Keyboard.GetState().IsKeyDown(Keys.K))
                hero.TakeDamage(1);

            List<Vector2> vecs = new List<Vector2>();
            for (int i = 0; i < Enemies.Count; i++)
            {
                vecs.Add(Vector2.Zero);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Vector2 v = vecs[i];
                    v.Y += Enemies[i].MovementSpeed;
                    vecs[i] = v;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Vector2 v = vecs[i];
                    v.Y -= Enemies[i].MovementSpeed;
                    vecs[i] = v;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Vector2 v = vecs[i];
                    v.X -= Enemies[i].MovementSpeed;
                    vecs[i] = v;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Vector2 v = vecs[i];
                    v.X += Enemies[i].MovementSpeed;
                    vecs[i] = v;
                }
            }
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Move(vecs[i]);
            }
            // --- END TO BE DELETED ----

            if (playerGameStatus == PlayerGameStatus.ALIVE)
            {

                // Movement of the player with collision detection
                Vector2 north = Vector2.Zero;
                Vector2 south = Vector2.Zero;
                Vector2 west = Vector2.Zero;
                Vector2 east = Vector2.Zero;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    west.X -= hero.MovementSpeed;

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    east.X += hero.MovementSpeed;

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                    north.Y -= hero.MovementSpeed;

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                    south.Y += hero.MovementSpeed;


                bool collisionNorth = false;
                bool collisionSouth = false;
                bool collisionWest = false;
                bool collisionEast = false;
                foreach (var room in map.Rooms)
                {
                    #region DoorCollision

                    DoorTile door = CollisionDetector.HasDoorTileCollision(room, hero, north, ref map);
                    if (door != null)
                    {
                        if (!door.IsLastDoor)
                            hero.Position = door.getOtherSideDoor().TeleportPosition;
                        else
                        {
                            hero.Position = door.TeleportPosition;
                            stage++;

                            // The enemies and projectiles need to get cleared between stages because we're entering new rooms where the old enemies and projectiles don't exist, and this happens at similar coordinates.
                            clearEnemiesAndProjectiles();
                            //Enemies = new List<Enemy>();
                            map.SetStage(stage);
                            map.GenerateMap(content, ref enemies, hero.Camera);
                            foreach (var e in enemies)
                            {
                                e.LoadContent(content);
                                e.Camera = camera;
                            }
                            ClearItemsOnStageChange();
                        }

                        break;
                    }

                    door = CollisionDetector.HasDoorTileCollision(room, hero, south, ref map);
                    if (door != null)
                    {
                        if (!door.IsLastDoor)
                            hero.Position = door.getOtherSideDoor().TeleportPosition;
                        else
                        {
                            hero.Position = door.TeleportPosition;
                            stage++;
                            clearEnemiesAndProjectiles();
                            Enemies = new List<Enemy>();
                            map.SetStage(stage);
                            map.GenerateMap(content, ref enemies, hero.Camera);
                            foreach (var e in enemies)
                            {
                                e.LoadContent(content);
                                e.Camera = camera;
                            }
                            ClearItemsOnStageChange();
                        }

                        break;
                    }

                    door = CollisionDetector.HasDoorTileCollision(room, hero, west, ref map);
                    if (door != null)
                    {
                        if (!door.IsLastDoor)
                            hero.Position = door.getOtherSideDoor().TeleportPosition;
                        else
                        {
                            hero.Position = door.TeleportPosition;
                            stage++;
                            clearEnemiesAndProjectiles();
                            Enemies = new List<Enemy>();
                            map.SetStage(stage);
                            map.GenerateMap(content, ref enemies, hero.Camera);
                            foreach (var e in enemies)
                            {
                                e.LoadContent(content);
                                e.Camera = camera;
                            }
                            ClearItemsOnStageChange();
                        }

                        break;
                    }

                    door = CollisionDetector.HasDoorTileCollision(room, hero, east, ref map);
                    if (door != null)
                    {
                        if (!door.IsLastDoor)
                            hero.Position = door.getOtherSideDoor().TeleportPosition;
                        else
                        {
                            hero.Position = door.TeleportPosition;
                            stage++;
                            clearEnemiesAndProjectiles();
                            Enemies = new List<Enemy>();
                            map.SetStage(stage);
                            map.GenerateMap(content, ref enemies, hero.Camera);
                            foreach (var e in enemies)
                            {
                                e.LoadContent(content);
                                e.Camera = camera;
                            }
                            ClearItemsOnStageChange();
                        }

                        break;
                    }

                    #endregion

                    #region StructureCollision

                    if (CollisionDetector.HasStructureCollision(room, hero, north))
                        collisionNorth = true;
                    if (CollisionDetector.HasStructureCollision(room, hero, south))
                        collisionSouth = true;
                    if (CollisionDetector.HasStructureCollision(room, hero, west))
                        collisionWest = true;
                    if (CollisionDetector.HasStructureCollision(room, hero, east))
                        collisionEast = true;

                    #endregion
                }


                #region EnemyCollision

                if (CollisionDetector.HasEnemyCollision(hero, enemies, north))
                    collisionNorth = true;
                if (CollisionDetector.HasEnemyCollision(hero, enemies, south))
                    collisionSouth = true;
                if (CollisionDetector.HasEnemyCollision(hero, enemies, west))
                    collisionWest = true;
                if (CollisionDetector.HasEnemyCollision(hero, enemies, east))
                    collisionEast = true;

                #endregion

                Vector2 movement = Vector2.Zero;
                if (!collisionNorth)
                    movement += north;

                if (!collisionSouth)
                    movement += south;

                if (!collisionWest)
                    movement += west;

                if (!collisionEast)
                    movement += east;


                hero.Move(movement);
            }
            else if (playerGameStatus == PlayerGameStatus.DEAD)
            {

                if (deathAnimationFinished)
                {
                    screenManager.AddScreen(new GameOverScreen(playerGameStatus), controllingPlayer);
                }
            }
            else if (playerGameStatus == PlayerGameStatus.WON)
            {
                screenManager.AddScreen(new GameOverScreen(playerGameStatus), controllingPlayer);
            }
        }

        public void clearEnemiesAndProjectiles()
        {
            enemies.Clear();
            Projectiles.Clear();
        }

        // public void playSoundBowEquip1()
        // {
        //     bowEquip1.Play();
        // }

        private void ClearItemsOnStageChange()
        {
            worldConsumables.Clear();
            //enemies.Clear();
        }


        public void UpdateEnemies(GameTime gameTime)
        {
            List<Enemy> deadEnemy = new List<Enemy>();

            foreach (var e in Enemies)
            {
                e.UpdateDistanceToHero(hero.Position);
                //e.Idling(map.Rooms[0]);
                e.FollowPlayer(map.Rooms[0]);
                e.Camera = camera;
                e.Update(gameTime);
                if (e.HealthPoints <= 0)
                    deadEnemy.Add(e);

            }

            foreach (Enemy e in deadEnemy)
            {
                bool finishedAnim = e.PlayDeathAnimation();
                if (finishedAnim)
                {
                    Enemies.Remove(e);
                    worldConsumables.Add(new HealthPotion("HP", HealthPotion, null, e.Position, 20));
                    worldConsumables.Add(new SpeedPotion("Speed Potion", SpeedPotion, null, e.Position, 2f, 10));
                }
            }
        }

        public void CollectItems()
        {
            // Collect pots
            Item itemToCollect = CollisionDetector.HasItemCollision(worldConsumables, hero);
            if (itemToCollect != null)
            {
                for (int i = 0; i < hero.Inventory.Length; i++)
                {
                    if (hero.Inventory[i] == null)
                    {
                        itemToCollect.ItemOwner = hero;
                        hero.Inventory[i] = itemToCollect;
                        worldConsumables.Remove(itemToCollect);
                        break;
                    }
                }
            }
        }

    }
}