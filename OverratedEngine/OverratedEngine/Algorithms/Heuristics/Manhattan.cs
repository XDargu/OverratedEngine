using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OverratedEngine.Algorithms.Heuristics
{
    public class Manhattan : IHeuristic
    {
        public string Name
        {
            get { return "Manhattan"; }
        }
        public int GetEstimate(Vector2 source, Vector2 destination)
        {
            return (int)(Math.Abs(destination.X - source.X) + Math.Abs(destination.Y - source.Y));
        }
    }
}
