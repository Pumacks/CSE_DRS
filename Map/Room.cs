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

        public Room(string roomTxt)
        {
            roomTiles = ReadRoomAsTxtFile(roomTxt);
        }

        /*
        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tiles:tiles);
        }
        */

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
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            /*spriteBatch.Draw(texture: Texture,
                            position: position,
                            sourceRectangle: null,
                            color: Color.White,
                            rotation: 0f,
                            origin: Vector2.Zero,
                            scale: 0.2f,
                            effects: flipTexture ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                            layerDepth: 0f);*/

            Vector2 tile = new Vector2(0,0);
            for (int i = 0; i < roomTiles.GetLength(0); i++)
            {
                tile.X = 0;
                for (int j = 0; j < roomTiles.GetLength(1); j++)
                {
                    if (roomTiles[i, j] == 1){
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

        }



    }
}
