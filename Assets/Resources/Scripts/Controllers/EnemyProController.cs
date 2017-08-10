using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyProController : DinamicObjectController
{
    private Rigidbody enemyRigidbody;
    private Animator animator;
    private Vector3 step;

    public override void Move()
    {
        this.animator.Play("Run");
        this.SetMove(enemyRigidbody, step.x, this.RotationByY(step.x, step.z), step.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //this.animator.Play("Attack");
            //Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (collision.gameObject.tag != "Ground")
        //{
        //    Vector3 nextPosition = Round(gameObject.transform.position + step);
        //    Vector3 collisionObjectPosition = Round(collision.gameObject.transform.position);

        //    if (CanStepForward(collisionObjectPosition, nextPosition))
        //    {
        //        //EndWalk();
        //    }
        //}
    }

    private void Start()
    {
        this.enemyRigidbody = gameObject.GetComponent<Rigidbody>();

        this.Speed = Game.DinamicObjectSpeed;
        this.animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        int[,] pathMatrix = new int[Game.Row, Game.Col];

        for (int i = 0; i < Game.Row; i++)
        {
            for (int j = 0; j < Game.Col; j++)
            {
                if (!IsNoWalkObject(i, j))
                {
                    pathMatrix[i, j] = 1;
                }
            }
        }

        Vector3 currentPosition = Round(gameObject.transform.position);
        Vector3 endPosition = Round(GameObject.FindGameObjectWithTag("Player").transform.position);
        List<Vector3> path = this.FindPath(pathMatrix, currentPosition, endPosition);
        if (path != null)
        {
            this.step = path.ToArray()[0] - currentPosition;
            Debug.Log(this.step);
            this.Move();
        }
    }

    //private Vector3 GetPath()
    //{
    //    int[,] pathMatrix = new int[Game.Row, Game.Col];
    //    bool makeStep = true;

    //    for (int i = 0; i < Game.Row; i++)
    //    {
    //        for (int j = 0; j < Game.Col; j++)
    //        {
    //            if (!IsNoWalkObject(i,j))
    //            {
    //                pathMatrix[i, j] = 1;
    //            }
    //        }
    //    }

    //    Vector3 currentPosition = Round(gameObject.transform.position);
    //    Vector3 endPosition = Round(GameObject.FindGameObjectWithTag("Player").transform.position);

    //    //Stack<Vector3> field = new Stack<Vector3>();
    //    //field.Push(endPosition);
    //    //for(int i = 0; i < 4; i++)
    //    //{
    //    //    if (pathMatrix[(int)endPosition.z, (int)endPosition.x] == 0)
    //    //    {
    //    //        field.Push(new Point(i, j));
    //    //        while (field.Count != 0)
    //    //        {
    //    //            Point point = field.Pop();

    //    //            if (CheckDownCell(mainPlane, point))
    //    //                FloodingField(mainPlane, new Point(point.X + 1, point.z), field);

    //    //            if (CheckUpCell(mainPlane, point))
    //    //                FloodingField(mainPlane, new Point(point.X - 1, point.z), field);

    //    //            if (CheckRightCell(mainPlane, point))
    //    //                FloodingField(mainPlane, new Point(point.X, point.z + 1), field);

    //    //            if (CheckLeftCell(mainPlane, point))
    //    //                FloodingField(mainPlane, new Point(point.X, point.z - 1), field);

    //    //        }
    //    //    }
    //    //}

    //    return new Vector3();
    //}

    private bool IsNoWalkObject(int row, int col)
    {
        return Game.MatrixMap[row, col] == (int)ObjectType.UnbreakWall
            || Game.MatrixMap[row, col] == (int)ObjectType.BreakWall
            || Game.MatrixMap[row, col] == (int)ObjectType.Bomb;
    }

    //private bool CanStepForward(Vector3 collisionObjectPosition, Vector3 nextPosition)
    //{
    //    return collisionObjectPosition.x == nextPosition.x && collisionObjectPosition.z == nextPosition.z;
    //}

    //private Vector3 Step(int x, int z)
    //{
    //    return new Vector3(x, 0, z);
    //}

    public List<Vector3> FindPath(int[,] field, Vector3 start, Vector3 end)
    {
        var closedSet = new List<PathNode>();
        var openSet = new List<PathNode>();

        PathNode startNode = new PathNode()
        {
            Position = start,
            CameFrom = null,
            PathLengthFromStart = 0,
            HeuristicEstimatePathLength = GetHeuristicPathLength(start, end)
        };

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            var currentNode = openSet.OrderBy(node =>
              node.EstimateFullPathLength).First();

            if (currentNode.Position == end)
                return GetPathForNode(currentNode);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (var neighbourNode in GetNeighbours(currentNode, end, field))
            {
                if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                    continue;

                var openNode = openSet.FirstOrDefault(node =>
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
        return null;
    }

    private int GetDistanceBetweenNeighbours()
    {
        return 1;
    }

    private int GetHeuristicPathLength(Vector3 from, Vector3 to)
    {
        return (int)(Math.Abs(from.x - to.x) + Math.Abs(from.z - to.z));
    }

    private List<PathNode> GetNeighbours(PathNode pathNode, Vector3 goal, int[,] field)
    {
        var result = new List<PathNode>();

        Vector3[] neighbourPoints = new Vector3[4];
        neighbourPoints[0] = new Vector3(pathNode.Position.x + 1, 0, pathNode.Position.z);
        neighbourPoints[1] = new Vector3(pathNode.Position.x - 1, 0, pathNode.Position.z);
        neighbourPoints[2] = new Vector3(pathNode.Position.x, 0, pathNode.Position.z + 1);
        neighbourPoints[3] = new Vector3(pathNode.Position.x, 0, pathNode.Position.z - 1);

        foreach (var point in neighbourPoints)
        {
            if (point.x < 0 || point.x >= field.GetLength(0))
                continue;
            if (point.z < 0 || point.z >= field.GetLength(1))
                continue;

            if ((field[(int)point.z, (int)point.x] != 0))
                continue;

            var neighbourNode = new PathNode()
            {
                Position = point,
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart + GetDistanceBetweenNeighbours(),
                HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
            };
            result.Add(neighbourNode);
        }
        return result;
    }

    private List<Vector3> GetPathForNode(PathNode pathNode)
    {
        var result = new List<Vector3>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Position);
            currentNode = currentNode.CameFrom;
        }
        result.Reverse();
        return result;
    }

}

