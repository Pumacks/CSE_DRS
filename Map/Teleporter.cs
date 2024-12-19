using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Models.Map
{

    class Teleporter : Tile
    {
        private Teleporter otherSideTp;

        public Teleporter(Vector2 pos, Texture2D texture, bool collision) : base(pos, texture, collision)
        {
        }

        public void setOtherSideTp(Teleporter tp)
        {
            otherSideTp = tp;
        }
    }

}
