using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace GameStateManagementSample.Models.Map
{
    public class MapGenerator
    {
        private static Random random = new Random();
        Room[] rooms = new Room[5];
        bool[,] mapVisual = new bool[10, 10];
        int xmapVisual = 5;
        int ymapVisual = 5;

        public Room[] Rooms
        {
            get => rooms;
            set => rooms = value;
        }


        const int ROOM_DISTANCE = 2100; // Einheitliche Distanz



        public MapGenerator()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i] = new Room();
            }
        }

        public void GenerateMap()
        {
            rooms[0].GenerateRoom(random, new Vector2(5000,5000));
            mapVisual = new bool[10,10];
            xmapVisual = 5;
            ymapVisual = 5;
            mapVisual[xmapVisual, ymapVisual] = true;

            int randomdirection;
            int randomRoom;
            Vector2 posOfRoom;

            for (int i = 1; i < rooms.Length; i++)
            {
                randomdirection = random.Next(1, 5);
                randomRoom = random.Next(0, i);

                switch (randomdirection) //direction 0 = north, 1 = east, 2 = south, 3 = west
                {
                    case 1: // North
                        if (rooms[randomRoom].allDirectionsBlocked())
                        {
                            i--;
                            break;
                        }
                        if (!rooms[randomRoom].isDirectionBlockedOn(0) && !mapVisual[xmapVisual, ymapVisual - 1])
                        {
                            ymapVisual -= 1;
                            mapVisual[xmapVisual, ymapVisual] = true;
                            posOfRoom = rooms[randomRoom].GetTiles()[0, 0].getPos();
                            posOfRoom.Y -= ROOM_DISTANCE;

                            rooms[i].GenerateRoom(random, posOfRoom);

                            rooms[randomRoom].blockDirection(0);
                            rooms[i].blockDirection(2);

                            rooms[randomRoom].setDoors(0, rooms[i]);

                            if (i == rooms.Length - 1)
                                rooms[i].setDoorLastDoor(0);

                        }
                        else
                            i--;
                        break;

                    case 2: // East
                        if (rooms[randomRoom].allDirectionsBlocked())
                        {
                            i--;
                            break;
                        }
                        if (!rooms[randomRoom].isDirectionBlockedOn(1) && !mapVisual[xmapVisual + 1, ymapVisual])
                        {
                            xmapVisual += 1;
                            mapVisual[xmapVisual, ymapVisual] = true;
                            posOfRoom = rooms[randomRoom].GetTiles()[0, 0].getPos();
                            posOfRoom.X += ROOM_DISTANCE;

                            rooms[i].GenerateRoom(random, posOfRoom);

                            rooms[randomRoom].blockDirection(1);
                            rooms[i].blockDirection(3);


                            rooms[randomRoom].setDoors(1, rooms[i]);

                            if (i == rooms.Length - 1)
                                rooms[i].setDoorLastDoor(1);
                        }
                        else
                            i--;
                        break;
                    case 3: // South
                        if (rooms[randomRoom].allDirectionsBlocked())
                        {
                            i--;
                            break;
                        }
                        if (!rooms[randomRoom].isDirectionBlockedOn(2) && !mapVisual[xmapVisual, ymapVisual + 1])
                        {
                            ymapVisual += 1;
                            mapVisual[xmapVisual, ymapVisual] = true;
                            posOfRoom = rooms[randomRoom].GetTiles()[0, 0].getPos();
                            posOfRoom.Y += ROOM_DISTANCE;

                            rooms[i].GenerateRoom(random, posOfRoom);

                            rooms[randomRoom].blockDirection(2);
                            rooms[i].blockDirection(0);

                            rooms[randomRoom].setDoors(2, rooms[i]);

                            if (i == rooms.Length - 1)
                                rooms[i].setDoorLastDoor(2);
                        }
                        else
                            i--;
                        break;
                    case 4: // West
                        if (rooms[randomRoom].allDirectionsBlocked())
                        {
                            i--;
                            break;
                        }
                        if (!rooms[randomRoom].isDirectionBlockedOn(3) && !mapVisual[xmapVisual - 1, ymapVisual])
                        {
                            xmapVisual -= 1;
                            mapVisual[xmapVisual, ymapVisual] = true;
                            posOfRoom = rooms[randomRoom].GetTiles()[0, 0].getPos();
                            posOfRoom.X -= ROOM_DISTANCE;

                            rooms[i].GenerateRoom(random, posOfRoom);

                            rooms[randomRoom].blockDirection(3);
                            rooms[i].blockDirection(1);

                            rooms[randomRoom].setDoors(3, rooms[i]);

                            if (i == rooms.Length - 1)
                                rooms[i].setDoorLastDoor(3);
                        }
                        else
                            i--;
                        break;
                }

            }
        }

        public MapGenerator getMap(){
            return this;
        }
        public void DrawMap(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i].DrawRoom(spriteBatch);
            }
        }

        public void LoadMapTextures(ContentManager content)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i].LoadTextures(content);
            }
        }

    }
}