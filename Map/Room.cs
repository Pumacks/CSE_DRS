using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using System.IO;

namespace GameStateManagementSample.Models.Room
{
    public class Room
    {
        private int[,] roomTiles;
        Texture2D grass;
        Texture2D hole;
        Texture2D tree;

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
            hole = content.Load<Texture2D>("Map/hole");
            tree = content.Load<Texture2D>("Map/tree");
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 tile = new Vector2(0,0);
            for (int i = 0; i < roomTiles.GetLength(0); i++)
            {
                tile.X = 0;
                for (int j = 0; j < roomTiles.GetLength(1); j++)
                {
                    if (roomTiles[i, j] == 1 || roomTiles[i, j] == 2){
                        spriteBatch.Draw(grass, tile, Color.White);
                        tile.X += 100;
                    }

                    if (roomTiles[i, j] == 0){
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
                    if (roomTiles[i, j] == 2){
                        spriteBatch.Draw(tree, tile, Color.White);
                        tile.X += 100;
                    }
                }
                tile.Y += 100;
            }
        }
    }
}
