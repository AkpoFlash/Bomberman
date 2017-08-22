using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class AStar
{
    [Command]
    public static List<Vector3> CmdFindPath(int[,] field, Vector3 start, Vector3 end)
    {
        List<PathNode> closedSet = new List<PathNode>();
        List<PathNode> openSet = new List<PathNode>();

        PathNode startNode = new PathNode()
        {
            Position = start,
            CameFrom = null,
            PathLengthFromStart = 0,
            ApproximatePathLength = GetApproximatePathLength(start, end)
        };

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            PathNode currentNode = openSet.OrderBy(node =>
              node.ExpectedPathLength).First();

            if (currentNode.Position == end)
                return GetPathForNode(currentNode);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (var neighbourNode in GetNeighbours(currentNode, end, field))
            {
                if (closedSet.Count(node => node.Position == neighbourNode.Position) == 0)
                {
                    PathNode openNode = openSet.FirstOrDefault(node =>
                    node.Position == neighbourNode.Position);

                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                        {
                            openNode.CameFrom = currentNode;
                            openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                        }
                }
            }
        }
        return null;
    }

    private static int GetApproximatePathLength(Vector3 from, Vector3 to)
    {
        return (int)(Math.Abs(from.x - to.x) + Math.Abs(from.z - to.z));
    }

    private static List<PathNode> GetNeighbours(PathNode pathNode, Vector3 goal, int[,] field)
    {
        List<PathNode> result = new List<PathNode>();

        Vector3[] neighbourPoints = new Vector3[4];
        neighbourPoints[0] = new Vector3(pathNode.Position.x + 1, 0, pathNode.Position.z);
        neighbourPoints[1] = new Vector3(pathNode.Position.x - 1, 0, pathNode.Position.z);
        neighbourPoints[2] = new Vector3(pathNode.Position.x, 0, pathNode.Position.z + 1);
        neighbourPoints[3] = new Vector3(pathNode.Position.x, 0, pathNode.Position.z - 1);

        foreach (var point in neighbourPoints)
        {
            if (CanMove(point, field))
            {
                PathNode neighbourNode = new PathNode()
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart + 1,
                    ApproximatePathLength = GetApproximatePathLength(point, goal)
                };
                result.Add(neighbourNode);
            }
        }
        return result;
    }

    private static List<Vector3> GetPathForNode(PathNode pathNode)
    {
        List<Vector3> result = new List<Vector3>();
        PathNode currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Position);
            currentNode = currentNode.CameFrom;
        }
        result.Reverse();
        return result;
    }

    private static bool CanMove(Vector3 point, int[,] field)
    {
        return (point.x >= 0 || point.x < field.GetLength(1))
            && (point.z >= 0 || point.z < field.GetLength(0))
            && (field[(int)point.z, (int)point.x] != 1);
    }

}
