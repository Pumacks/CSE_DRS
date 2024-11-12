using GameStateManagement;
using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;

namespace GameStateManagementSample.Models.Camera
{
    public class Camera
    {
        public Matrix Transform { get; set; }

        public void Follow(Entity target)
        {
            var position = Matrix.CreateTranslation(
              -target.Position.X - (target.BoundingBox.Width / 2),
              -target.Position.Y - (target.BoundingBox.Height / 2),
              0);

            var offset = Matrix.CreateTranslation(
                GameStateManagementGame.ScreenWidth / 2,
                GameStateManagementGame.ScreenHeight / 2,
                0);

            Transform = position * offset;
        }
    }
}
