using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    //外部赋值
    public Map map;

    public void AStarPath(Node start, Node end)
    {
        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();
        CalcNodeG(start);
        CalcNodeF(start, end);

        openList.Add(start);

        while (openList.Count > 0)
        {
            Node node = FindCurEdgeMinF(openList);
            openList.Remove(node);
            closeList.Add(node);
            List<Node> edgeNodes = GetEdgeNodes(node, closeList);
            foreach (Node item in edgeNodes)
            {
                if (openList.Contains(item))
                {
                    float curG = PreCalcG(item, node);
                    if(curG < item.G)
                    {
                        item.UpdateParentAndG(node, curG);
                    }
                }
                item.SetParent(node);
                CalcNodeG(item);
                CalcNodeF(item, end);
                if(!openList.Contains(item))
                    openList.Add(item);
            }
            if (openList.Contains(end))
            {
                break;
            }
        }
        
    }

    //在openlist里找出F最小的Node
    public Node FindCurEdgeMinF(List<Node> opens)
    {
        Node tmp = null;
        float curF = 999f;
        foreach (Node item in opens)
        {
            if(item.F < curF)
            {
                tmp = item;
                curF = item.F;
            }
        }
        return tmp;
    }

    //找到当前点周围的点(不包括障碍、已经在closelist的)
    public List<Node> GetEdgeNodes(Node node, List<Node> close)
    {
        //对周围8个Node进行筛选
        Node LeftUp, Up, RightUp, Left, Right, LeftDown, Down, RightDown;

        //防止数组越界
        bool upCheck = node.Y + 1 <= map.Height - 1;
        bool leftCheck = node.X - 1 >= 0;
        bool rightCheck = node.X + 1 <= map.Width - 1;
        bool downCheck = node.Y - 1 >= 0;

        List<Node> edges = new List<Node>();


        if (leftCheck && upCheck)
        {
            LeftUp = map.nodes[node.X - 1, node.Y + 1];
            if (LeftUp != null && !LeftUp.isWall)
            {
                edges.Add(LeftUp);
            }
        }
        if(upCheck)
        {
            Up = map.nodes[node.X, node.Y + 1];
            if (Up!=null && !Up.isWall)
            {
                edges.Add(Up);
            }
        }
        if(rightCheck && upCheck)
        {
            RightUp = map.nodes[node.X + 1, node.Y + 1];
            if(RightUp!= null && !RightUp.isWall)
            {
                edges.Add(RightUp);
            }
        }
        if (leftCheck)
        {
            Left = map.nodes[node.X - 1, node.Y];
            if(Left!=null && !Left.isWall)
            {
                edges.Add(Left);
            }
        }
        if (rightCheck)
        {
            Right = map.nodes[node.X + 1, node.Y];
            if(Right!=null && !Right.isWall)
            {
                edges.Add(Right);
            }
        }
        if(leftCheck && downCheck)
        {
            LeftDown = map.nodes[node.X - 1, node.Y - 1];
            if(LeftDown!=null && !LeftDown.isWall)
            {
                edges.Add(LeftDown);
            }
        }
        if (downCheck)
        {
            Down = map.nodes[node.X, node.Y - 1];
            if(Down!=null && !Down.isWall)
            {
                edges.Add(Down);
            }
        }
        if(rightCheck && downCheck)
        {
            RightDown = map.nodes[node.X + 1, node.Y - 1];
            if(RightDown!=null && !RightDown.isWall)
            {
                edges.Add(RightDown);
            }
        }

        foreach (Node item in close)
        {
            if (edges.Contains(item))
            {
                edges.Remove(item);
            }
        }

        return edges; 

    }

    public void CalcNodeG(Node node)
    {
        float G;
        if (node.parent != null)
        {
            G = node.parent.G + ((node.X == node.parent.X || node.Y == node.parent.Y) ? 10 : 14);
        }
        else
        {
            G = 0;
        }
        node.G = G;
    }

    public void CalcNodeF(Node node, Node end)
    {
        float H;
        int dx = Mathf.Abs(node.X - end.X);
        int dy = Mathf.Abs(node.Y - end.Y);

        //曼哈顿距离
        //H = (dx + dy)*10;

        //对角距离
        H = 10 * (dx + dy) +
            (1.414f * 10 - 20) * Mathf.Min(dx, dy);

        node.H = H;
        node.F = node.G + node.H;
    }

    public float PreCalcG(Node node, Node parent)
    {
        float G;
        G = parent.G + ((node.X == parent.X || node.Y == parent.Y) ? 10 : 14);
        return G;
    }
}
