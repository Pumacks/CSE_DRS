using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStateManagementSample.Models.Items;

namespace GameStateManagementSample.Models.Helpers
{
    public class CollisionDetector
    {


        public static DoorTile HasDoorTileCollision(Room room, Entity entity, Vector2 movement)
        {
            Tile[,] tiles = room.GetTiles();
            int x = (int)entity.Position.X + (int)movement.X - entity.Texture.Width / 2;
            int y = (int)entity.Position.Y + (int)movement.Y - entity.Texture.Height / 2;
            Rectangle entityBoundingBoxAfterMovment = new Rectangle(x, y, entity.Texture.Width, entity.Texture.Height);
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] is DoorTile doorTile && IsIntersecting(tiles[i, j].BoundingBox, entityBoundingBoxAfterMovment))
                    {
                        return doorTile;
                    }
                }
            }
            return null;
        }

        public static bool HasStructureCollision(Room room, Entity entity, Vector2 movment)
        {
            Tile[,] tiles = room.GetTiles();
            int x = (int)entity.Position.X + (int)movment.X - entity.Texture.Width / 2;
            int y = (int)entity.Position.Y + (int)movment.Y - entity.Texture.Height / 2;

            Rectangle entityBoundingBoxAfterMovement = new Rectangle(x, y, entity.Texture.Width, entity.Texture.Height);

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].Collision && IsIntersecting(tiles[i, j].BoundingBox, entityBoundingBoxAfterMovement))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Item HasItemCollision(List<Item> items, Entity entity)
        {
            foreach (Item item in items)
            {
                if (IsIntersecting(item.BoundingBox, entity.BoundingBox))
                {
                    return item;
                }
            }
            return null;
        }

        public static bool IsIntersecting(Rectangle objA, Rectangle objB)
        {
            return objA.Intersects(objB);
        }
    }
}
