using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace GameStateManagementSample.Models.Room
{
    public class Room
    {
        private int[,] roomTiles;
        private Tile[,] tiles = new Tile[20, 20];
        private Texture2D grass;
        private Texture2D grass2;
        private Texture2D grass3;
        private Texture2D hole;
        private Texture2D tree;

        private Random random = new Random();

        public Room(string roomTxt)
        {
            roomTiles = ReadRoomAsTxtFile(roomTxt);
        }

        static int[,] ReadRoomAsTxtFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int rows = lines.Length;
                int cols = lines[0].Split(' ').Length;
                int[,] tiles = new int[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    string[] numbers = lines[i].Split(' ');
                    for (int j = 0; j < cols; j++)
                    {
                        tiles[i, j] = int.Parse(numbers[j]);
                    }
                }

                return tiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Lesen der Datei: {ex.Message}");
                return null;
            }
        }

        public void loadTextures(ContentManager content)
        {
            grass = content.Load<Texture2D>("Map/grass");
            grass2 = content.Load<Texture2D>("Map/grass2");
            grass3 = content.Load<Texture2D>("Map/grass3");
            hole = content.Load<Texture2D>("Map/hole");
            tree = content.Load<Texture2D>("Map/tree");

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tile = new Vector2(0, 0);
            for (int i = 0; i < roomTiles.GetLength(0); i++)
            {
                tile.X = 0;
                for (int j = 0; j < roomTiles.GetLength(1); j++)
                {
                    if (roomTiles[i, j] == 1 || roomTiles[i, j] == 2)
                    {
                        if (i == roomTiles.GetLength(0) - 1)
                            spriteBatch.Draw(grass, new Vector2(tile.X, tile.Y + 100), Color.White);

                        spriteBatch.Draw(grass, tile, Color.White);
                        tile.X += 100;
                    }

                    if (roomTiles[i, j] == 0)
                    {
                        spriteBatch.Draw(hole, tile, Color.White);
                        tile.X += 100;
                    }
                }
                tile.Y += 100;
            }
            tile.X = 0;
            tile.Y = 0;
            for (int i = 0; i < roomTiles.GetLength(0); i++)
            {
                tile.X = 0;
                for (int j = 0; j < roomTiles.GetLength(1); j++)
                {
                    if (roomTiles[i, j] == 2)
                    {
                        spriteBatch.Draw(tree, tile, Color.White);
                    }
                    tile.X += 100;
                }
                tile.Y += 100;
            }
        }


        public void generateRoom(SpriteBatch spriteBatch)
        {
            Vector2 tilePos = new Vector2(0, 0);
            int rdmNumber;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                tilePos.X = 0;
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    rdmNumber = random.Next(1,10);
                    if(rdmNumber <= 6)
                        spriteBatch.Draw(grass, tilePos, Color.White);
                    if(rdmNumber > 6 && rdmNumber <= 8)
                        spriteBatch.Draw(grass2, tilePos, Color.White);
                    if(rdmNumber > 8 && rdmNumber <= 10)
                        spriteBatch.Draw(grass3, tilePos, Color.White);
                    
                    tilePos.X += 100;

                }
                tilePos.Y += 100;
            }
            tilePos.X = 0;
            tilePos.Y = 0;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                tilePos.X = 0;
                for (int j = 0; j < tiles.GetLength(1); j++)
                {

                    if (i == 0 || j == 0 || j == tiles.GetLength(1) - 1)
                    {
                        tiles[i, j] = new Tile(tilePos, tree, true);
                        spriteBatch.Draw(tiles[i,j].getTexture(), tiles[i,j].getPos(), Color.White);
                    }
                    tilePos.X += 100;
                }
                tilePos.Y += 100;
            }
        }
    }
}
