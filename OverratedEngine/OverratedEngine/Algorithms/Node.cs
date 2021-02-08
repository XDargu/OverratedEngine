using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OverratedEngine.Algorithms
{
    public enum NodeType
    {
        Walkable,
        Unwalkable
    }

    public enum NodeState
    {
        Open,
        Closed,
        NotVisited
    }

    public class Node
    {
        public double G { get; set; }
        public double H { get; set; }
        public double F { get { return G + H; } }
        public NodeState State { get; set; }
        public NodeType Type { get; set; }
        public Vector2 Position { get; private set; }
        public List<Node> Neighbors { get; set; }
        public Node Parent { get; set; }

        public Node(Vector2 position, List<Node> neighbors)
        {
            Type = NodeType.Walkable;
            Position = position;
            State = NodeState.NotVisited;
            G = 0;
            H = 0;
            Neighbors = neighbors;
            Parent = null;
        }

        public Node(Vector2 position)
        {
            Type = NodeType.Walkable;
            Position = position;
            State = NodeState.NotVisited;
            G = 0;
            H = 0;
            Neighbors = new List<Node>();
            Parent = null;
        }
    }
}
