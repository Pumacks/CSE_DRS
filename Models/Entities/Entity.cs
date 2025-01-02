using System.Collections;
using GameStateManagementSample.Models.GUI;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace GameStateManagementSample.Models.Entities
{
    enum AnimState
    {
        Idle,
        Walk,
        Attack,
        Death
    }

    public abstract class Entity
    {
        #region atributes

        private Weapon activeWeapon;
        protected Item[] inventory;
        private Texture2D texture;
        private int healthPoints;
        protected Vector2 position;
        protected Vector2 lastPosition;
        protected Rectangle boundingBox;
        protected bool flipTexture;
        protected GameTime gameTime;

        private AnimState animState = AnimState.Idle;

        protected List<GUIObserver> GUIObservers = new();
        //Testing Purposes, required to give a Camera to an Entity so that the
        private Camera.Camera camera;
        public Camera.Camera CameraProperty { get { return camera; } set { camera = value; } }


        protected SpriteFont spriteFont;

        // Weapon + Healthpots
        List<Item> items = new List<Item>();
        #endregion

        #region Properties
        public int HealthPoints
        {
            get { return healthPoints; }
            set
            {
                healthPoints = value;
                if (healthPoints <= 0)
                {
                    healthPoints = 0;
                }
            }
        }
        public float MovementSpeed { get; set; }
        public Texture2D Texture { get { return texture; } set { texture = value; } }

        public Weapon ActiveWeapon { get { return activeWeapon; } set { activeWeapon = value; } }

        public Item[] Inventory { get { return this.inventory; } set { inventory = value; } }
        public GameTime GameTime { get { return gameTime; } set { gameTime = value; } }
        public Rectangle BoundingBox { get { return boundingBox; } }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                lastPosition = position;
                position = value;
                boundingBox = new Rectangle((int)position.X - Texture.Width / 2, (int)position.Y - Texture.Height / 2,
                    texture.Width, texture.Height);
            }
        }

        public Vector2 LastPostion
        {
            get { return lastPosition; }
            set { lastPosition = value; }
        }

        #endregion

        protected AnimationManager animManager;
        public Entity() { }

        public Entity(int healthPoints, float movmentSpeed, Vector2 playerPosition, Texture2D texture, SpriteFont spriteFont, List<Item> items)
        {
            this.HealthPoints = healthPoints;
            this.MovementSpeed = movmentSpeed;
            this.position = playerPosition;
            this.texture = texture;
            this.items = items;
            this.spriteFont = spriteFont;
            this.inventory = new Item[7];
            animManager = new AnimationManager(movmentSpeed);
            this.boundingBox = new Rectangle((int)position.X - texture.Width / 2,
                (int)position.Y - texture.Height / 2,
                texture.Width,
                texture.Height);
            GUIObservers.Add(new FloatingHealthNumbers(this));
        }


        public abstract void Move(Vector2 movment);
        public abstract void Atack();

        public abstract void LoadContent(ContentManager content);

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: CameraProperty.Transform);
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


            foreach (GUIObserver observer in GUIObservers)
            {
                observer.Draw(spriteBatch, spriteFont);
            }
        }


        public void TakeDamage(int damage)
        {
            HealthPoints -= damage;
            NotifyObservers();
        }
        protected void NotifyObservers()
        {
            foreach (GUIObserver observer in GUIObservers)
            {
                observer.Update();
            }
        }

        public bool PlayDeathAnimation()
        {
            Texture = animManager.DeathAnimation();
            return animManager.DeathAnimationFinished();
        }



        public void Update(GameTime gametime)
        {
            GameTime = gametime;

            // Filip texture based on direction
            if (lastPosition.X < Position.X)
                flipTexture = false;
            else if (lastPosition.X > Position.X)
                flipTexture = true;
            //-------


            if (activeWeapon != null)
            {
                if (animManager.AttackAnimationFinished() && activeWeapon.ItemOwner.GameTime.TotalGameTime.TotalMilliseconds - activeWeapon.LastAttackGameTimeInMilliseconds >= activeWeapon.AttackSpeed)
                {
                    activeWeapon.IsAtacking = false;
                }
            }




            if (HealthPoints <= 0)
                animState = AnimState.Death;
            else if (activeWeapon is { IsAtacking: true })
                animState = AnimState.Attack;
            else if (Position == LastPostion)
                animState = AnimState.Idle;
            else if (Position != LastPostion)
                animState = AnimState.Walk;






            switch (animState)
            {
                case AnimState.Idle:
                    Texture = animManager.IdleAnimation();
                    break;

                case AnimState.Walk:
                    Texture = animManager.WalkAnimation();
                    break;

                case AnimState.Attack:
                    Texture = animManager.AttackAnimation();
                    break;

                case AnimState.Death:
                    Texture = animManager.DeathAnimation();
                    break;
            }
        }



    }
}
