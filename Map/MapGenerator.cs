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
            rooms[0].GenerateRoom(random, new Vector2(5000, 5000));
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
                        if (!rooms[randomRoom].isDirectionBlockedOn(0))
                        {
                            posOfRoom = rooms[randomRoom].GetTiles()[0, 0].getPos();
                            posOfRoom.Y -= ROOM_DISTANCE;

                            rooms[randomRoom].blockDirection(0);
                            rooms[randomRoom].setDoorInDirection(0);
                            
                            rooms[i].GenerateRoom(random, posOfRoom);
                            rooms[i].blockDirection(2);
                            rooms[i].setDoorInDirection(2);

                            Console.WriteLine("generated from room " + randomRoom + " in the North" + " on pos " + posOfRoom);
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
                        if (!rooms[randomRoom].isDirectionBlockedOn(1))
                        {
                            posOfRoom = rooms[randomRoom].GetTiles()[0, 0].getPos();
                            posOfRoom.X += ROOM_DISTANCE;

                            rooms[randomRoom].blockDirection(1);
                            rooms[randomRoom].setDoorInDirection(1);
                            
                            rooms[i].GenerateRoom(random, posOfRoom);
                            rooms[i].blockDirection(3);
                            rooms[i].setDoorInDirection(3);
                            
                            Console.WriteLine("generated from room " + randomRoom + " in the East" + " on pos " + posOfRoom);
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
                        if (!rooms[randomRoom].isDirectionBlockedOn(2))
                        {
                            posOfRoom = rooms[randomRoom].GetTiles()[0, 0].getPos();
                            posOfRoom.Y += ROOM_DISTANCE;

                            rooms[randomRoom].blockDirection(2);
                            rooms[randomRoom].setDoorInDirection(2);

                            rooms[i].GenerateRoom(random, posOfRoom);
                            rooms[i].blockDirection(0);
                            rooms[i].setDoorInDirection(0);

                            Console.WriteLine("generated from room " + randomRoom + " in the South" + " on pos " + posOfRoom);
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
                        if (!rooms[randomRoom].isDirectionBlockedOn(3))
                        {
                            posOfRoom = rooms[randomRoom].GetTiles()[0, 0].getPos();
                            posOfRoom.X -= ROOM_DISTANCE;

                            rooms[randomRoom].blockDirection(3);
                            rooms[randomRoom].setDoorInDirection(3);
                            
                            rooms[i].GenerateRoom(random, posOfRoom);
                            rooms[i].blockDirection(1);
                            rooms[i].setDoorInDirection(1);
                            
                            Console.WriteLine("generated from room " + randomRoom + " in the West" + " on pos " + posOfRoom);
                        }
                        else
                            i--;
                        break;
                }
                
            }
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