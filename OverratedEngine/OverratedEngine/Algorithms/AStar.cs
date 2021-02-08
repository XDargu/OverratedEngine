using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OverratedEngine.Algorithms.Heuristics;
using Microsoft.Xna.Framework;

namespace OverratedEngine.Algorithms
{
    public class AStar
    {
        public List<Node> World { get; set; }
        public IHeuristic Heuristic { get; set; }

        private Node currentNode;
        private List<Node> openList;
        private List<Node> path;

        public AStar(List<Node> world, IHeuristic heuristic)
        {
            World = world;
            Heuristic = heuristic;
        }

        /// <summary>
        /// Creates a graph with an adyacency matrix and a list of nodes
        /// </summary>
        /// <param name="world">The list of nodes</param>
        /// <param name="adyacency_matrix">The adyacency matrix</param>
        /// <returns>The list of nodes with the neigbors updated</returns>
        public List<Node> createWorld(List<Node> world, bool[,] adyacency_matrix)
        {
            List<Node> neighbors;

            for (int i = 0; i < world.Count; i++)
            {
                neighbors = new List<Node>();
                for (int e = 0; e < world.Count; e++)
                {
                    if (adyacency_matrix[i, e])
                        neighbors.Add(world[e]);
                }
                world[i].Neighbors = neighbors;
            }

            return world;
        }

        /// <summary>
        /// Finds a path between two nodes
        /// </summary>
        /// <param name="source">Source node</param>
        /// <param name="destination">Destination node</param>
        /// <returns>A Vector2 list with the solution path. If there's no solution, a node empty list</returns>
        public List<Vector2> FindVectorPath(Node source, Node destination)
        {
            List<Node> nodeList = FindPath(source, destination);
            List<Vector2> path = new List<Vector2>();

            foreach (Node node in nodeList)
                path.Add(node.Position);

            return path;
        }
        
        /// <summary>
        /// Finds a path between two nodes
        /// </summary>
        /// <param name="source">Source node</param>
        /// <param name="destination">Destination node</param>
        /// <returns>A node list with the solution path. If there's no solution, a node empty list</returns>
        public List<Node> FindPath(Node source, Node destination)
        {
            bool active = true;

            path = new List<Node>();
            openList = new List<Node>();

            CleanWorld();

            currentNode = source;
            currentNode.State = NodeState.Closed;           

            while (active)
            {
                foreach (Node node in currentNode.Neighbors)
                {
                    if (node.Type == NodeType.Walkable)
                    {
                        if (node.State == NodeState.NotVisited)
                        {
                            node.State = NodeState.Open;
                            node.Parent = currentNode;
                            node.H = Heuristic.GetEstimate(node.Position, destination.Position);
                            node.G = currentNode.G + DistanceBetweenNodes(currentNode, node);
                            openList.Add(node);
                            // Sort the openList
                            /*openList.Sort(delegate(Node a, Node b)
                            {
                                return b.F.CompareTo(a.F);
                            });*/
                        }
                    }
                }

                // No path
                if (openList.Count == 0)
                {
                    active = false;
                    path = new List<Node>();
                }

                // Takes the next node
                double min = double.MaxValue;
                Node minNode = currentNode;
                foreach (Node node in openList)
                {
                    if (node.F < min)
                    {
                        minNode = node;
                        min = node.F;
                    }
                }
                currentNode = minNode;
                openList.Remove(minNode);
                currentNode.State = NodeState.Closed;

                // Path found
                if (destination.State == NodeState.Closed)
                {
                    Node currNode = currentNode;

                    while (currNode != null)
                    {
                        path.Add(currNode);
                        currNode = currNode.Parent;
                    }
                    path.Reverse();
                    path.RemoveAt(0); // Remove the source node
                    active = false;
                }
            }


            return path;
        }

        private double DistanceBetweenNodes(Node a, Node b)
        {
            return Math.Sqrt((a.Position.X - b.Position.X) * (a.Position.X - b.Position.X) + (a.Position.Y - b.Position.Y) * (a.Position.Y - b.Position.Y));
        }

        private void CleanWorld()
        {
            foreach (Node node in World)
            {
                node.State = NodeState.NotVisited;
                node.Type = NodeType.Walkable;
                node.H = 0;
                node.G = 0;
                node.Parent = null;
            }
        }
    }
}
