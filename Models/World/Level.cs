using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GameStateManagementSample.Models.Entities;
using GameStateManagementSample.Models.Items;
using Microsoft.Xna.Framework;

namespace GameStateManagementSample.Models.World
{
    public class Level
    {

        // This entire class can be scrapped. It's all in Engine.
        #region attributes, fields and properties
        private List<Enemy> enemies;
        public List<Enemy> Enemies
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
        private List<Projectile> projectiles;
        public List<Projectile> Projectiles
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