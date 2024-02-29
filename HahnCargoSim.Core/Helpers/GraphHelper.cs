using HahnCargoSim.Models.Grid;

namespace HahnCargoSim.Core.Helpers;

public class GraphHelper
{
    public List<Node> Nodes { get; private set; }
    public List<Edge> Edges { get; private set; }
    public List<Connection> Connections { get; private set; }
    public Dictionary<int, List<int>> AdjacencyList { get; private set; }

    public GraphHelper(Grid grid)
    {
        Nodes = grid.Nodes;
        Edges = grid.Edges;
        Connections = grid.Connections;

        AdjacencyList = new Dictionary<int, List<int>>();
        foreach (var connection in Connections)
        {
            if (!AdjacencyList.ContainsKey(connection.FirstNodeId))
                AdjacencyList[connection.FirstNodeId] = [];
            AdjacencyList[connection.FirstNodeId].Add(connection.SecondNodeId);

            if (!AdjacencyList.ContainsKey(connection.SecondNodeId))
                AdjacencyList[connection.SecondNodeId] = [];
            AdjacencyList[connection.SecondNodeId].Add(connection.FirstNodeId);
        }
    }

    public List<ConnectionEdge> AStarSearch(int startNodeId, int endNodeId)
    {
        var openSet = new HashSet<int>();
        var closedSet = new HashSet<int>();
        var cameFrom = new Dictionary<int, int>();
        var gScore = new Dictionary<int, int> { [startNodeId] = 0 };
        var fScore = new Dictionary<int, int> { [startNodeId] = Heuristic(startNodeId, endNodeId) };

        openSet.Add(startNodeId);

        while (openSet.Any())
        {
            var current = openSet.OrderBy(nodeId => fScore.GetValueOrDefault(nodeId, int.MaxValue)).First();

            if (current == endNodeId)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);

            if (!AdjacencyList.ContainsKey(current))
                continue;

            foreach (var neighbor in AdjacencyList[current])
            {
                if (closedSet.Contains(neighbor))
                    continue;

                var tentativeGScore = gScore.GetValueOrDefault(current, int.MaxValue) + GetCost(current, neighbor);

                if (!openSet.Contains(neighbor))
                    openSet.Add(neighbor);
                else if (tentativeGScore >= gScore.GetValueOrDefault(neighbor, int.MaxValue))
                    continue;

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore.GetValueOrDefault(neighbor, int.MaxValue) + Heuristic(neighbor, endNodeId);
            }
        }

        return null;
    }

    private List<ConnectionEdge> ReconstructPath(Dictionary<int, int> cameFrom, int current)
    {
        var path = new List<ConnectionEdge>();

        while (cameFrom.ContainsKey(current))
        {
            var next = cameFrom[current];
            var connection = Connections.First(c => (c.FirstNodeId == current && c.SecondNodeId == next) || (c.FirstNodeId == next && c.SecondNodeId == current));
            var edge = Edges.First(e => Connections.Any(c => (c.FirstNodeId == current && c.SecondNodeId == next) || (c.FirstNodeId == next && c.SecondNodeId == current)));
            path.Add(new ConnectionEdge { Id = edge.Id, Cost = edge.Cost, Time = edge.Time, ConnectionId = connection.Id });
            current = next;
        }

        path.Reverse();
        return path;
    }

    private int Heuristic(int nodeId, int endNodeId)
    {
        var node = Nodes.First(n => n.Id == nodeId);
        var endNode = Nodes.First(n => n.Id == endNodeId);
        return Math.Abs(node.Id - endNode.Id);
    }

    private int GetCost(int currentNode, int neighbor)
    {
        var connection = Connections.FirstOrDefault(c => (c.FirstNodeId == currentNode && c.SecondNodeId == neighbor) || (c.FirstNodeId == neighbor && c.SecondNodeId == currentNode));
        if (connection != null)
        {
            var edge = Edges.FirstOrDefault(e => e.Id == connection.EdgeId);
            if (edge != null)
                return edge.Cost;
        }
        return int.MaxValue;
    }
}

