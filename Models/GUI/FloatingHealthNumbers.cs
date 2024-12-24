using System;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;


namespace GameStateManagementSample.Models.GUI
{
    internal class FloatingHealthNumbers : GUIObserver
    {
        private Color color;
        private int healthPoints;
        private int healthDifference;
        private double maxTime = 200;
        bool healthChanged = false;
        private Vector2 offset;

        public FloatingHealthNumbers(Entity entity) : base(entity)
        {
            healthPoints = entity.HealthPoints;
            offset = new Vector2(0, entity.Texture.Width / 4);
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
                    color = Color.Green;
                    healthDifference = healthPoints - oldHealthPoints;
                }
                else
                {
                    color = Color.Red;
                    healthDifference = oldHealthPoints - healthPoints;
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
                }

                String str = color == Color.Green ? "+" : "-";
                spriteBatch.Begin(transformMatrix: player.CameraProperty.Transform);
                spriteBatch.DrawString(spriteFont, str + healthDifference.ToString(), player.Position - spriteFont.MeasureString(healthDifference.ToString()) - offset, color);

                spriteBatch.End();
            }
        }
    }
}
