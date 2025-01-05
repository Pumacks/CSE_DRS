using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace GameStateManagementSample.Models.Map
{
    public class Room
    {
        enum SkyDirection
        {
            NORTH,
            EAST,
            SOUTH,
            WEST
        }

        private Tile[,] tiles;
        private bool[] blocked = new bool[4]; // wird für Map generation genutzt
        private DoorTile[] roomDoors = new DoorTile[4];
        private int stage;

        private Texture2D grass;
        private Texture2D grass2;
        private Texture2D grass3;
        private Texture2D tree;

        private Texture2D grassElement1;
        private Texture2D grassElement2;
        private Texture2D grassElement3;

        private Texture2D grasslvl2;
        private Texture2D stone;
        private Texture2D stone2;
        private Texture2D stone3;
        private Texture2D stone4;
        private Texture2D treelvl2;

        private Texture2D lvl3floor1;
        private Texture2D lvl3floor2;
        private Texture2D lvl3floor3;
        private Texture2D treelvl3;



        private Texture2D hole;
        private Texture2D doorN;
        private Texture2D doorE;
        private Texture2D doorS;
        private Texture2D doorW;

        private Texture2D stoneDoorN;
        private Texture2D stoneDoorE;
        private Texture2D stoneDoorS;
        private Texture2D stoneDoorW;

        private Texture2D doorlvl3N;
        private Texture2D doorlvl3E;
        private Texture2D doorlvl3S;
        private Texture2D doorlvl3W;

        private Texture2D pot;

        private Random random = new Random();

        public Room()
        {
            for (int i = 0; i < blocked.Length; i++)
            {
                blocked[i] = false;
            }
        }


        public void LoadTextures(ContentManager content)
        {
            //lvl1 grass
            grass = content.Load<Texture2D>("Map/grass");
            grass2 = content.Load<Texture2D>("Map/grass2");
            grass3 = content.Load<Texture2D>("Map/grass3");

            //lvl2 grass
            stone = content.Load<Texture2D>("Map/stone");
            stone2 = content.Load<Texture2D>("Map/stone2");
            stone3 = content.Load<Texture2D>("Map/stone3");
            stone4 = content.Load<Texture2D>("Map/stone4");
            grasslvl2 = content.Load<Texture2D>("Map/grasslvl2");
            treelvl2 = content.Load<Texture2D>("Map/treelvl2");

            //lvl3 grass
            lvl3floor1 = content.Load<Texture2D>("Map/lvl3floor1");
            lvl3floor2 = content.Load<Texture2D>("Map/lvl3floor2");
            lvl3floor3 = content.Load<Texture2D>("Map/lvl3floor3");
            treelvl3 = content.Load<Texture2D>("Map/treelvl3");

            hole = content.Load<Texture2D>("Map/hole");
            tree = content.Load<Texture2D>("Map/Tree1_scaled");

            doorN = content.Load<Texture2D>("Map/doorN");
            doorE = content.Load<Texture2D>("Map/doorE");
            doorS = content.Load<Texture2D>("Map/doorS");
            doorW = content.Load<Texture2D>("Map/doorW");

            stoneDoorN = content.Load<Texture2D>("Map/2doorN");
            stoneDoorE = content.Load<Texture2D>("Map/2doorE");
            stoneDoorS = content.Load<Texture2D>("Map/2doorS");
            stoneDoorW = content.Load<Texture2D>("Map/2doorW");

            doorlvl3N = content.Load<Texture2D>("Map/DoorlvlN");
            doorlvl3E = content.Load<Texture2D>("Map/Doorlvl3E");
            doorlvl3S = content.Load<Texture2D>("Map/Doorlvl3S");
            doorlvl3W = content.Load<Texture2D>("Map/Doorlvl3W");

            pot = content.Load<Texture2D>("Map/pot");

        }

        public void GenerateRoom(Random random, Vector2 roomPos, int stage)
        {
            this.stage = stage;
            tiles = GenerateRoomArray();
            if (tiles == null)
                throw new InvalidOperationException("Tiles array is not initialized.");

            int rdmNumber;
            Vector2 tilePos = roomPos;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                tilePos.X = roomPos.X;

                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    rdmNumber = random.Next(1, 10);
                    if (stage == 1)
                    {
                        if (rdmNumber <= 6)
                            tiles[i, j] = new Tile(tilePos, grass, false);
                        if (rdmNumber > 6 && rdmNumber <= 8)
                            tiles[i, j] = new Tile(tilePos, grass2, false);
                        if (rdmNumber > 8 && rdmNumber <= 10)
                            tiles[i, j] = new Tile(tilePos, grass3, false);
                    }
                    if (stage == 2)
                    {
                        if (rdmNumber <= 2)
                            tiles[i, j] = new Tile(tilePos, stone, false);
                        if (rdmNumber > 2 && rdmNumber <= 6)
                            tiles[i, j] = new Tile(tilePos, stone2, false);
                        if (rdmNumber > 6 && rdmNumber <= 8)
                            tiles[i, j] = new Tile(tilePos, stone3, false);
                        if (rdmNumber > 8 && rdmNumber <= 10)
                            tiles[i, j] = new Tile(tilePos, stone4, false);
                    }
                    if (stage == 3)
                    {
                        if (rdmNumber <= 6)
                            tiles[i, j] = new Tile(tilePos, lvl3floor1, false);
                        if (rdmNumber > 6 && rdmNumber <= 8)
                            tiles[i, j] = new Tile(tilePos, lvl3floor2, false);
                        if (rdmNumber > 8 && rdmNumber <= 10)
                            tiles[i, j] = new Tile(tilePos, lvl3floor3, false);
                    }
                    // for things like pots etc.
                    rdmNumber = random.Next(1, 100);
                    if (rdmNumber == 15 && i >= 4 && j >= 4 && i <= tiles.GetLength(0) - 4 && j <= tiles.GetLength(1) - 4)
                        tiles[i, j] = new Tile(tilePos, pot, true);

                    tilePos.X += 100;
                }
                tilePos.Y += 100;
            }
            tilePos.X = roomPos.X;
            tilePos.Y = roomPos.Y;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                tilePos.X = roomPos.X;
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || j == tiles.GetLength(1) - 1 || i == tiles.GetLength(0) - 1)
                    {
                        if (stage == 1)
                            tiles[i, j] = new Tile(tilePos, tree, true);
                        if (stage == 2)
                            tiles[i, j] = new Tile(tilePos, treelvl2, true);
                        if (stage == 3)
                            tiles[i, j] = new Tile(tilePos, treelvl3, true);
                    }
                    tilePos.X += 100;
                }
                tilePos.Y += 100;
            }
        }

        public void DrawRoom(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (stage == 1)
                        spriteBatch.Draw(grass, tiles[i, j].getPos(), Color.White); //damit die baumtexturen einen gras hintergrund haben
                    if (stage == 2)
                        spriteBatch.Draw(grasslvl2, tiles[i, j].getPos(), Color.White); //damit die baumtexturen einen gras hintergrund haben
                    if (stage == 3)
                        spriteBatch.Draw(lvl3floor1, tiles[i, j].getPos(), Color.White); //damit die baumtexturen einen gras hintergrund haben

                    spriteBatch.Draw(tiles[i, j].getTexture(), tiles[i, j].getPos(), Color.White);
                }
            }
            // This loop is to load the trees over the green textures
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (stage == 1)
                    {
                        if (i == tiles.GetLength(0) - 1)
                            spriteBatch.Draw(grass, new Vector2(tiles[i, j].getPos().X, tiles[i, j].getPos().Y + 100), Color.White);
                        if (tiles[i, j].getTexture() == tree)
                            spriteBatch.Draw(tiles[i, j].getTexture(), tiles[i, j].getPos(), Color.White);
                    }
                    if (stage == 2)
                    {
                        if (i == tiles.GetLength(0) - 1)
                            spriteBatch.Draw(grasslvl2, new Vector2(tiles[i, j].getPos().X, tiles[i, j].getPos().Y + 100), Color.White);
                        if (tiles[i, j].getTexture() == treelvl2)
                            spriteBatch.Draw(tiles[i, j].getTexture(), tiles[i, j].getPos(), Color.White);
                    }
                    if (stage == 3)
                    {
                        if (i == tiles.GetLength(0) - 1)
                            spriteBatch.Draw(lvl3floor1, new Vector2(tiles[i, j].getPos().X, tiles[i, j].getPos().Y + 100), Color.White);
                        if (tiles[i, j].getTexture() == treelvl3)
                            spriteBatch.Draw(tiles[i, j].getTexture(), tiles[i, j].getPos(), Color.White);
                    }
                }

            }
        }

        public Tile[,] GetTiles()
        {
            return tiles;
        }

        private Tile[,] GenerateRoomArray()
        {
            int width = random.Next(18, 25);
            int height = random.Next(18, 25);
            if (width % 2 == 0)
                width += 1;
            if (height % 2 == 0)
                height += 1;

            return new Tile[width, height];
        }

        public Tile setDoorLastDoor(int direction)
        {
            int mid;
            DoorTile doorTile;

            switch (direction)
            {
                case 0: // Norden
                    mid = (tiles.GetLength(1) / 2); // Vertikale Mitte

                    doorTile = new DoorTile(tiles[0, mid]);
                    doorTile.setTexture(stoneDoorN);

                    doorTile.TeleportPosition = new Vector2(5500, 5500);
                    doorTile.IsLastDoor = true;

                    tiles[0, mid] = doorTile;
                    return doorTile;

                case 1: // Osten
                    mid = (tiles.GetLength(0) / 2); // Horizontale Mitte

                    doorTile = new DoorTile(tiles[mid, tiles.GetLength(1) - 1]);
                    doorTile.setTexture(stoneDoorE);
                    if (stage == 1)
                        tiles[mid - 1, tiles.GetLength(1) - 1].setTexture(grass);
                    if (stage == 2)
                        tiles[mid - 1, tiles.GetLength(1) - 1].setTexture(grasslvl2);
                    if (stage == 3)
                        tiles[mid - 1, tiles.GetLength(1) - 1].setTexture(lvl3floor1);

                    doorTile.TeleportPosition = new Vector2(5500, 5500);
                    doorTile.IsLastDoor = true;

                    tiles[mid, tiles.GetLength(1) - 1] = doorTile;
                    return doorTile;

                case 2: // Süden
                    mid = (tiles.GetLength(1) / 2); // Vertikale Mitte

                    doorTile = new DoorTile(tiles[tiles.GetLength(0) - 1, mid]);
                    doorTile.setTexture(stoneDoorS);

                    doorTile.TeleportPosition = new Vector2(5500, 5500);
                    doorTile.IsLastDoor = true;

                    tiles[tiles.GetLength(0) - 1, mid] = doorTile;
                    return doorTile;

                case 3: // Westen
                    mid = (tiles.GetLength(0) / 2); // Horizontale Mitte

                    doorTile = new DoorTile(tiles[mid, 0]);
                    doorTile.setTexture(stoneDoorW);
                    if (stage == 1)
                        tiles[mid - 1, 0].setTexture(grass);
                    if (stage == 2)
                        tiles[mid - 1, 0].setTexture(grasslvl2);
                    if (stage == 3)
                        tiles[mid - 1, 0].setTexture(lvl3floor1);

                    doorTile.TeleportPosition = new Vector2(5500, 5500);
                    doorTile.IsLastDoor = true;

                    tiles[mid, 0] = doorTile;
                    return doorTile;
            }
            if (direction > 3 || direction < 0)
                throw Exception();
            return null;
        }

        private Exception Exception()
        {
            throw new NotImplementedException("not a himmelsrichtung mf");
        }

        public void setDoors(int direction, Room oppositeRoom)
        {
            int mid;
            int oppositeMid;
            DoorTile doorTile;
            DoorTile oppositeDoorTile;

            switch (direction)
            {
                case 0: // Norden
                    mid = (tiles.GetLength(1) / 2);
                    tiles[0, mid].setTexture(doorN);

                    doorTile = new DoorTile(tiles[0, mid]);
                    tiles[0, mid] = doorTile;

                    // Für den anderen Raum
                    oppositeMid = oppositeRoom.GetTiles().GetLength(1) / 2;
                    oppositeRoom.tiles[oppositeRoom.GetTiles().GetLength(0) - 1, oppositeMid].setTexture(doorS);

                    oppositeDoorTile = new DoorTile(oppositeRoom.GetTiles()[oppositeRoom.GetTiles().GetLength(0) - 1, oppositeMid]);
                    oppositeRoom.tiles[oppositeRoom.GetTiles().GetLength(0) - 1, oppositeMid] = oppositeDoorTile;

                    // ((DoorTile)tiles[0, mid]).setOtherSideDoor((DoorTile)oppositeRoom.GetTiles()[oppositeRoom.GetTiles().GetLength(0) - 1, oppositeMid]);
                    doorTile.setOtherSideDoor(oppositeDoorTile);
                    oppositeDoorTile.setOtherSideDoor(doorTile);

                    doorTile.TeleportPosition = new Vector2(doorTile.getPos().X + doorTile.getTexture().Width / 2, doorTile.getPos().Y + doorTile.getTexture().Height + 100);
                    oppositeDoorTile.TeleportPosition = new Vector2(oppositeDoorTile.getPos().X + oppositeDoorTile.getTexture().Width / 2, oppositeDoorTile.getPos().Y - oppositeDoorTile.getTexture().Height);

                    roomDoors[0] = doorTile;
                    oppositeRoom.roomDoors[2] = oppositeDoorTile;
                    break;

                case 1: // Osten
                    mid = (tiles.GetLength(0) / 2);
                    tiles[mid, tiles.GetLength(1) - 1].setTexture(doorE);

                    doorTile = new DoorTile(tiles[mid, tiles.GetLength(1) - 1]);
                    tiles[mid, tiles.GetLength(1) - 1] = doorTile;

                    // Für den anderen Raum
                    oppositeMid = oppositeRoom.GetTiles().GetLength(0) / 2;
                    oppositeRoom.tiles[oppositeMid, 0].setTexture(doorW);

                    oppositeDoorTile = new DoorTile(oppositeRoom.GetTiles()[oppositeMid, 0]);
                    oppositeRoom.tiles[oppositeMid, 0] = oppositeDoorTile;


                    doorTile.setOtherSideDoor(oppositeDoorTile);
                    oppositeDoorTile.setOtherSideDoor(doorTile);

                    doorTile.TeleportPosition = new Vector2(doorTile.getPos().X - doorTile.getTexture().Width, doorTile.getPos().Y + doorTile.getTexture().Height / 2);
                    oppositeDoorTile.TeleportPosition = new Vector2(oppositeDoorTile.getPos().X + oppositeDoorTile.getTexture().Width + 100, oppositeDoorTile.getPos().Y + oppositeDoorTile.getTexture().Height / 2);


                    if (stage == 1)
                    {
                        oppositeRoom.tiles[oppositeMid - 1, 0].setTexture(grass);
                        tiles[mid - 1, tiles.GetLength(1) - 1].setTexture(grass);
                    }
                    if (stage == 2)
                    {
                        oppositeRoom.tiles[oppositeMid - 1, 0].setTexture(grasslvl2);
                        tiles[mid - 1, tiles.GetLength(1) - 1].setTexture(grasslvl2);
                    }
                    if (stage == 3)
                    {
                        oppositeRoom.tiles[oppositeMid - 1, 0].setTexture(lvl3floor1);
                        tiles[mid - 1, tiles.GetLength(1) - 1].setTexture(lvl3floor1);
                    }

                    roomDoors[1] = doorTile;
                    oppositeRoom.roomDoors[3] = oppositeDoorTile;
                    break;

                case 2: // Süden
                    mid = (tiles.GetLength(1) / 2);
                    tiles[tiles.GetLength(0) - 1, mid].setTexture(doorS);

                    doorTile = new DoorTile(tiles[tiles.GetLength(0) - 1, mid]);
                    tiles[tiles.GetLength(0) - 1, mid] = doorTile;

                    // Für den anderen Raum
                    oppositeMid = oppositeRoom.GetTiles().GetLength(1) / 2;
                    oppositeRoom.tiles[0, oppositeMid].setTexture(doorN);

                    oppositeDoorTile = new DoorTile(oppositeRoom.GetTiles()[0, oppositeMid]);
                    oppositeRoom.tiles[0, oppositeMid] = oppositeDoorTile;

                    doorTile.setOtherSideDoor(oppositeDoorTile);
                    oppositeDoorTile.setOtherSideDoor(doorTile);

                    doorTile.TeleportPosition = new Vector2(doorTile.getPos().X + doorTile.getTexture().Width / 2, doorTile.getPos().Y - doorTile.getTexture().Height);
                    oppositeDoorTile.TeleportPosition = new Vector2(oppositeDoorTile.getPos().X + doorTile.getTexture().Width / 2, oppositeDoorTile.getPos().Y + oppositeDoorTile.getTexture().Height + 200);

                    roomDoors[2] = doorTile;
                    oppositeRoom.roomDoors[0] = oppositeDoorTile;
                    break;

                case 3: // Westen
                    mid = (tiles.GetLength(0) / 2);
                    tiles[mid, 0].setTexture(doorW);

                    doorTile = new DoorTile(tiles[mid, 0]);
                    tiles[mid, 0] = doorTile;

                    // Für den anderen Raum
                    oppositeMid = oppositeRoom.GetTiles().GetLength(0) / 2;
                    oppositeRoom.tiles[oppositeMid, oppositeRoom.GetTiles().GetLength(1) - 1].setTexture(doorE);

                    oppositeDoorTile = new DoorTile(oppositeRoom.GetTiles()[oppositeMid, oppositeRoom.GetTiles().GetLength(1) - 1]);
                    oppositeRoom.tiles[oppositeMid, oppositeRoom.GetTiles().GetLength(1) - 1] = oppositeDoorTile;

                    doorTile.setOtherSideDoor(oppositeDoorTile);
                    oppositeDoorTile.setOtherSideDoor(doorTile);

                    doorTile.TeleportPosition = new Vector2(doorTile.getPos().X + doorTile.getTexture().Width + 100, doorTile.getPos().Y + doorTile.getTexture().Height / 2);
                    oppositeDoorTile.TeleportPosition = new Vector2(oppositeDoorTile.getPos().X - oppositeDoorTile.getTexture().Width, oppositeDoorTile.getPos().Y + oppositeDoorTile.getTexture().Height / 2);

                    if (stage == 1)
                    {
                        oppositeRoom.tiles[oppositeMid - 1, oppositeRoom.GetTiles().GetLength(1) - 1].setTexture(grass);
                        tiles[mid - 1, 0].setTexture(grass);
                    }
                    if (stage == 2)
                    {
                        oppositeRoom.tiles[oppositeMid - 1, oppositeRoom.GetTiles().GetLength(1) - 1].setTexture(grasslvl2);
                        tiles[mid - 1, 0].setTexture(grasslvl2);
                    }
                    if (stage == 3)
                    {
                        oppositeRoom.tiles[oppositeMid - 1, oppositeRoom.GetTiles().GetLength(1) - 1].setTexture(lvl3floor1);
                        tiles[mid - 1, 0].setTexture(lvl3floor1);
                    }
                    roomDoors[3] = doorTile;
                    oppositeRoom.roomDoors[1] = oppositeDoorTile;
                    break;
            }
        }

        public void blockDirection(int direction)
        {
            blocked[direction] = true;
        }

        public bool allDirectionsBlocked()
        {
            if (blocked[0] == true && blocked[1] == true && blocked[2] == true && blocked[3] == true)
                return true;
            else
                return false;
        }

        public bool isDirectionBlockedOn(int direction)
        {
            return blocked[direction];
        }

    }
}
