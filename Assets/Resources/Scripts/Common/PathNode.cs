using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Vector3 Position { get; set; }

    public int PathLengthFromStart { get; set; }

    public PathNode CameFrom { get; set; }

    public int HeuristicEstimatePathLength { get; set; }

    public int EstimateFullPathLength
    {
        get
        {
            return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
        }
    }
}
