using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
 
 
namespace GameStateManagementSample.Models.GUI
{
    public abstract class GUIObserver 
    {

        protected Entity player;
        public GUIObserver(Entity player)
        {
            this.player = player;
        }


        public abstract void Update();

        public abstract void Draw(SpriteBatch spriteBatch , SpriteFont spriteFont);
    }
}
