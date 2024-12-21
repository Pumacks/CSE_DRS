using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameStateManagementSample.Models.Map
{

    public class DoorTile : Tile
    {
        private DoorTile oppositeDoor;
        private Vector2 teleportPosition;
        private bool isLastDoor;

        public bool IsLastDoor
        {
            get { return isLastDoor; }
            set { isLastDoor = value; }
        }

        public Vector2 TeleportPosition
        {
            get { return teleportPosition; }
            set { teleportPosition = value; }
        }


        public DoorTile(Vector2 pos, Texture2D texture, bool collision) : base(pos, texture, collision)
        {

        }

        public DoorTile(Tile other)
        {
            setPos(other.getPos());
            setTexture(other.getTexture());
            setCollison(other.getCollision());
            BoundingBox = new Rectangle((int)getPos().X, (int)getPos().Y, getTexture().Width, getTexture().Height);
        }

        public void setOtherSideDoor(DoorTile door)
        {
            oppositeDoor = door;
        }

        public DoorTile getOtherSideDoor()
        {
            return oppositeDoor;
        }
    }

}
