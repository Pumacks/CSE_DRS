using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStateManagementSample.Models.Helpers
{
    public class CollisionDetector
    {
        public static bool IsIntersecting(Rectangle objA, Rectangle objB)
        {
            return objA.Intersects(objB);
        }
    }
}
