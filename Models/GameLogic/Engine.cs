using GameStateManagement;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.Helpers;
using GameStateManagementSample.Models.Items;
using GameStateManagementSample.Models.Map;
using GameStateManagementSample.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;


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
        public List<Item> WorldConsumables
        {
            get { return worldConsumables; }
            set { worldConsumables = value; }
        }

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
        Texture2D KeyTexture;
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

        public Player heroPlayer;
        public Engine enginereference;
        public Texture2D arrowTextureRef;

        private Random random = new Random();




        #region Fields and properties required for keeping track of enemies, player and projectile

        private List<Enemy> enemies;

        public List<Enemy> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }



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
            map = new MapGenerator(this);
            worldConsumables = new List<Item>();
            Enemies = new List<Enemy>();


            gameFont = content.Load<SpriteFont>("gamefont");
            initTexture = content.Load<Texture2D>("Player/idle_frames/idle0");

            hero = new Player(100, 5, new Vector2(5300, 5300), initTexture, gameFont, new List<Item>(), this);
            heroPlayer = hero;
            enginereference = this;
            hero.Camera = camera;


            // Loading the Texture for Arrows
            ArrowTexture = content.Load<Texture2D>("ArrowSmall7x68px");
            arrowTextureRef = ArrowTexture;
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




            RangedWeapon enemyBow = new RangedWeapon(
                "Bow of the Dungeon",
                BowTexture,
                null,
                15,
                2000,
                1000,
                Enemies,
                600,
                ArrowTexture,
                Projectiles,
                this
            );
            MeleeWeapon enemySword = new MeleeWeapon(
                "Sword of the Dungeon",
                SwordTexture,
                null,
                10,
                1500,
                150,
                Enemies,
                this
            );
            MeleeWeapon enemySpear = new MeleeWeapon(
                "Sword of the Dungeon",
                SwordTexture,
                null,
                15,
                2000,
                250,
                Enemies,
                this
            );

            //Enemy enemyWarrior = new EnemyWarrior(100, 1, new Vector2(5500, 5500), initTexture, gameFont, new List<Item>(), this);
            //enemySword.ItemOwner = enemyWarrior;
            //enemyWarrior.Camera = camera;

            //Enemy enemyArcher = new EnemyArcher(100, 1, new Vector2(5500, 5800), initTexture, gameFont, new List<Item>(), this);
            //enemyBow.ItemOwner = enemyArcher;
            //enemyArcher.Camera = camera;

            //Enemy enemySpearman = new EnemySpearman(100, 1, new Vector2(5800, 5800), initTexture, gameFont, new List<Item>(), this);
            //enemySpear.ItemOwner = enemySpearman;
            //enemySpearman.Camera = camera;


            //enemies.Add(enemyWarrior);
            //enemies.Add(enemyArcher);
            //enemies.Add(enemySpearman);

            // for (int i = 0; i < 100; i++)
            // {
            //     Enemy e = new EnemyWarrior(100, 1, new Vector2(5500 + 10 * i, 5500 + 10 * i), initTexture, gameFont, new List<Item>());
            //     e.Camera = camera;
            //     enemies.Add(e);
            // }

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
            KeyTexture = content.Load<Texture2D>("Items/Key");
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
                17,
                900, //cooldown
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
                17,
                700, //cooldown
                300,
                Enemies,
                this
            );



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
                text: "Press_F2_to_show_Controls",
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height * 0.01f + 150),
                Color.Blue
            );
            spriteBatch.End();

            if (hero.ShowControls)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "W,A,S,D_for_movement",
                    position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                        ScreenManager.GraphicsDevice.Viewport.Height * 0.01f + 200),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "Left_Mouse_Button_to_attack",
                    position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                        ScreenManager.GraphicsDevice.Viewport.Height * 0.01f + 250),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "Right_Mouse_Button_or_F_to_pickup_items",
                    position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                        ScreenManager.GraphicsDevice.Viewport.Height * 0.01f + 300),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "Middle_Mouse_Button_or_X_to_use_item",
                    position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                        ScreenManager.GraphicsDevice.Viewport.Height * 0.01f + 350),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "1,2,3,4,5,6,7_for_quick_inventory_access",
                    position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                        ScreenManager.GraphicsDevice.Viewport.Height * 0.01f + 400),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "Mousewheel_for_scrolling_through_inventory",
                    position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                        ScreenManager.GraphicsDevice.Viewport.Height * 0.01f + 450),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "G_to_drop_selected_item",
                    position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                        ScreenManager.GraphicsDevice.Viewport.Height * 0.01f + 500),
                    Color.Green
                );
                spriteBatch.End();
            }

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont: gameFont,
                text: "Press_F1_to_toggle_Stats",
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 250),
                Color.Blue
            );
            spriteBatch.End();

            if (hero.ShowStats)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "DPS:" + hero.ActiveWeapon.WeaponDamage * (1000 / hero.ActiveWeapon.AttackSpeed),
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 450),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "Damage:" + hero.ActiveWeapon.WeaponDamage,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 400),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "Cooldown:" + hero.ActiveWeapon.AttackSpeed,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 350),
                    Color.Green
                );
                spriteBatch.DrawString(
                    spriteFont: gameFont,
                    text: "Range:" + hero.ActiveWeapon.WeaponRange,
                position: new Vector2(ScreenManager.GraphicsDevice.Viewport.Width * 0.01f,
                    ScreenManager.GraphicsDevice.Viewport.Height - 300),
                    Color.Green
                );
                spriteBatch.End();
            }

        }


        public void Update(GameTime gameTime)
        {
            hero.Update(gameTime);
            camera.Follow(hero);
            UpdateEnemies(gameTime);
            // CollectItems();


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



                        if (theProjectile.ItemOwner is Player) // Player's arrows can only damage enemies.
                        {
                            // For each Projectile, checks collision between arrow and enemy hitbox with every enemy (yes, Projectiles * Enemies calculations, O(n²), bad but not extremely bad)
                            Enemies.ForEach(targetEnemy => // remove WeaponDamage amount of HealthPoints from the first Enemy whose hitbox intersects the arrow's hitbox.
                            {
                                if (!theProjectile.IsStuck) // <---------- notice this
                                    if (theProjectile.ProjectileHitBox.Intersects(targetEnemy.BoundingBox))
                                    {
                                        Player.totalScore += (int)theProjectile.ProjectileDamage;
                                        targetEnemy.TakeDamage(theProjectile.ProjectileDamage);
                                        Projectiles.Remove(theProjectile); // Projectiles.RemoveAt(projectileUpdateIndex); <--- ist fehleranfällig für IndexOutOfBoundsException
                                    }
                            });
                        }
                        else // Enemies' arrows can only damage the player.
                        {
                            if (!theProjectile.IsStuck) // <---------- notice this
                                if (theProjectile.ProjectileHitBox.Intersects(hero.BoundingBox))
                                {
                                    hero.TakeDamage(theProjectile.ProjectileDamage);
                                    Projectiles.Remove(theProjectile); // Projectiles.RemoveAt(projectileUpdateIndex); <--- ist fehleranfällig für IndexOutOfBoundsException
                                }
                        }

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

                    #region DoorCollision

                    DoorTile door = CollisionDetector.HasDoorTileCollision(room, hero, north);
                    if (door != null)
                    {
                        EnterDoor(door);
                        break;
                    }

                    door = CollisionDetector.HasDoorTileCollision(room, hero, south);
                    if (door != null)
                    {
                        EnterDoor(door);
                        break;
                    }

                    door = CollisionDetector.HasDoorTileCollision(room, hero, west);
                    if (door != null)
                    {
                        EnterDoor(door);
                        break;
                    }

                    door = CollisionDetector.HasDoorTileCollision(room, hero, east);
                    if (door != null)
                    {
                        EnterDoor(door);
                        break;
                    }

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

        private void ClearItemsOnStageChange()
        {
            worldConsumables.Clear();
            Projectiles.Clear();
        }


        public void UpdateEnemies(GameTime gameTime)
        {
            List<Enemy> deadEnemy = new List<Enemy>();

            foreach (var e in Enemies)
            {
                e.setGameTime(gameTime);
                e.UpdateDistanceToHero(hero.Position);
                //e.Idling(map.Rooms[0]);
                e.FollowPlayer(e.EnemyRoom);
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
                    if (Enemies.Count <= 0)
                        worldConsumables.Add(new Key("key", KeyTexture, hero, e.Position));

                    // 40% Drop Chance for Health Potion
                    if (random.Next(0, 100) < 40)
                        worldConsumables.Add(new HealthPotion("HP", HealthPotion, null, e.Position, 20));

                    // 40% Drop Chance for Speed Potion
                    if (random.Next(0, 100) < 40)
                        worldConsumables.Add(new SpeedPotion("Speed Potion", SpeedPotion, null, e.Position, 2f, 10));

                    // Roll for main loot, 10% drop chance
                    int mainDropRoll = random.Next(0, 100);
                    if (mainDropRoll > 89)
                    {
                        double damageMultiplier = random.NextDouble() * 0.08f + 0.97f;
                        double speedMultiplier = random.NextDouble() * 0.08f + 0.95f;
                        double rangeMultiplier = random.NextDouble() * 0.1f + 0.96f;
                        if (hero.ActiveWeapon is MeleeWeapon)
                        {
                            MeleeWeapon newWeapon = new MeleeWeapon(
                                "Ancient Sword",
                                SwordTexture,
                                hero,
                                (float)(hero.ActiveWeapon.WeaponDamage * damageMultiplier + 1),
                                (float)(hero.ActiveWeapon.AttackSpeed * speedMultiplier - 1),
                                (float)(hero.ActiveWeapon.WeaponRange * rangeMultiplier + 1),
                                Enemies,
                                this
                            );
                            newWeapon.Position = e.Position;
                            worldConsumables.Add(newWeapon);
                        }
                        else if (hero.ActiveWeapon is RangedWeapon)
                        {
                            RangedWeapon newWeapon = new RangedWeapon(
                                "Ancient Bow",
                                BowTexture,
                                hero,
                                (float)(hero.ActiveWeapon.WeaponDamage * damageMultiplier + 1),
                                (float)(hero.ActiveWeapon.AttackSpeed * speedMultiplier - 1),
                                (float)(hero.ActiveWeapon.WeaponRange * rangeMultiplier + 1),
                                Enemies,
                                // (int)(hero.ActiveWeapon.ProjectileSpeed * 1.05f),
                                1500,
                                ArrowTexture,
                                Projectiles,
                                this
                            );
                            newWeapon.Position = e.Position;
                            worldConsumables.Add(newWeapon);
                        }
                    }
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

        public void EnterDoor(DoorTile door)
        {
            if (!door.IsLastDoor)
                hero.Position = door.getOtherSideDoor().TeleportPosition;
            else
            {
                if (hero.HasKey)
                {
                    stage++;
                    if (stage > 3)
                        playerGameStatus = PlayerGameStatus.WON;
                    else
                    {
                        hero.Position = door.TeleportPosition;
                        clearEnemiesAndProjectiles();
                        map.SetStage(stage);
                        map.GenerateMap(content, ref enemies, hero.Camera);
                        foreach (var e in enemies)
                        {
                            e.LoadContent(content);
                            e.Camera = camera;
                        }
                        ClearItemsOnStageChange();
                        hero.UseKey();
                    }
                }
            }
        }
    }
}