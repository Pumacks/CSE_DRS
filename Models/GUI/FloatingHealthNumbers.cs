using System;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;


namespace GameStateManagementSample.Models.GUI
{
    enum Health
    {
        Damage,
        Healing
    }
    internal class FloatingHealthNumbers : GUIObserver
    {
        private Color color;
        private int healthPoints;
        private int healthDifference;
        bool healthChanged = false;

        private Health healthStatus;

        private double maxTime = 200;
        private Vector2 offset;

        public FloatingHealthNumbers(Entity entity) : base(entity)
        {
            healthPoints = entity.HealthPoints;
            offset = new Vector2(-30, (entity.Texture.Width / 4) -5);
        }

        public override void Update()
        {
            int oldHealthPoints = healthPoints;
            healthPoints = player.HealthPoints;

            if (oldHealthPoints != healthPoints)
            {
                healthChanged = true;
                maxTime = 2000;
                if (oldHealthPoints < healthPoints)
                {
                    if (healthStatus == Health.Damage)
                        healthDifference = 0;

                    healthStatus = Health.Healing;
                    color = Color.Green;
                    healthDifference += healthPoints - oldHealthPoints;
                }
                else
                {
                    if (healthStatus == Health.Healing)
                        healthDifference = 0;

                    healthStatus = Health.Damage;
                    color = Color.Red;
                    healthDifference -= oldHealthPoints - healthPoints;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (healthChanged)
            {
                maxTime -= player.GameTime.ElapsedGameTime.Milliseconds;
                if (maxTime <= 0)
                {
                    healthChanged = false;
                    maxTime = 2000;
                    healthDifference = 0;
                }

                String str = healthDifference > 0 ? "+" : "";
                spriteBatch.Begin(transformMatrix: player.CameraProperty.Transform);
                spriteBatch.DrawString(spriteFont, str + healthDifference.ToString(), player.Position - spriteFont.MeasureString(healthDifference.ToString()) - offset, color);

                spriteBatch.End();
            }
        }
    }
}
