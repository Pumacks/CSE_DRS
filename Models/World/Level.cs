using System.Diagnostics.CodeAnalysis;
using GameStateManagementSample.Models.Entities;
using Microsoft.Xna.Framework;

namespace GameStateManagementSample.Models.World
{
    public class Level
    {

        #region attributes, fields and properties
        private List<Entity> enemies;
        public List<Entity> Enemies
        {
            get
            {
                return this.enemies;
            }
            set
            {
                this.enemies = value;
            }
        }
        private Entity player;
        public Entity Player
        {
            get
            {
                return this.player;
            }
            set
            {
                this.player = value;
            }
        }
        private List<Vector2> projectiles
        public List<Vector2> Projectiles
        {
            get
            {
                return this.projectiles;
            }
            set
            {
                this.projectiles = value;
            }
        }
        #endregion

    }
}