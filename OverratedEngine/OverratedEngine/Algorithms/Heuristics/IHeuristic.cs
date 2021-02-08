using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OverratedEngine.Algorithms.Heuristics
{
    public interface IHeuristic
    {
        string Name { get; }
        int GetEstimate(Vector2 source, Vector2 destination);
    }
}
