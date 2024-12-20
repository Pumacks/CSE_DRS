using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Models.Map
{

    class DoorTile : Tile
    {
        private DoorTile oppositeDoor;

        public DoorTile(Vector2 pos, Texture2D texture, bool collision) : base(pos, texture, collision)
        {

        }
        
        public DoorTile(Tile other){
            setPos(other.getPos());
            setTexture(other.getTexture());
            setCollison(other.getCollision());
        }

        public void setOtherSideDoor(DoorTile door)
        {
            oppositeDoor = door;
        }

        public DoorTile getOtherSideDoor(){
            return oppositeDoor;
        }
    }

}
