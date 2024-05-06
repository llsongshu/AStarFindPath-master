using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Player player;
    public AStar Astarpath;

    public int Height = 4;
    public int Width = 4;
    public Node[,] nodes;
   
    
    void Start()
    {
        InitMap();
        player.SetCurNode(nodes[0, 0]);
    }

    void InitMap()
    {
        nodes = new Node[Width, Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                nodes[i, j] = new Node(i, j);
                nodes[i, j].SetMap(this);
                if (i == 0 && j == 0)
                    continue;
                bool wall = Random.Range(0, 10) > 7f;
                nodes[i, j].SetIsWall(wall);
            }
        }
    }

    public void StartFindPath(Node start, Node end)
    {
        Astarpath.AStarPath(start, end);
        ShowPath(end);
    }

    void ShowPath(Node end)
    {
        end.ShowPathNode();
        player.MoveToEnd(end);
    }

    //重新随机一下Map
    public void ReSetMap()
    {
        foreach (Node item in nodes)
        {
            item.ReSetNode();
            if (item.X == player.GetCurNode().X && item.Y == player.GetCurNode().Y)
                continue;
            bool wall = Random.Range(0,10) > 7f;
            item.SetIsWall(wall);
        }  
    }

    public void ReFind()
    {
        foreach (Node item in nodes)
        {
            item.ReSetNormalNode();
        }
    }
}
