using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int X;
    public int Y;
    public float F = 0;
    public float G = 0;
    public float H = 0;

    public bool isWall; //是否障碍

    public Node parent;
    public Map map;
    private Renderer nodeRender;
    private NodeItem nodeItem;


    public Node(int x, int y, Node parent = null)
    {
        this.X = x;
        this.Y = y;
        if (parent != null)
        {
            this.parent = parent;
        }
        CreateNode();
    }

    public void SetIsWall(bool wall)
    {
        isWall = wall;
        if (isWall) { nodeRender.material.SetColor("_Color", Color.black); }
    }

    public void SetParent(Node p)
    {
        this.parent = p;
    }

    public void UpdateParentAndG(Node p, float G)
    {
        this.parent = p;
        this.G = G;
        this.F = this.G + H;
    }
    void CreateNode()
    {
        GameObject node = GameObject.Instantiate(Resources.Load("MapNode"), GameObject.Find("Map").transform) as GameObject;
        node.transform.position = new Vector3(this.X + 0.5f, this.Y + 0.5f, 0f);
        node.transform.name = "Node(" + X + "," + Y + ")";
        nodeRender = node.GetComponent<Renderer>();

        nodeItem = node.GetComponent<NodeItem>();
        nodeItem.SetNode(this);
    }

    public void ShowPathNode()
    {
        nodeRender.material.SetColor("_Color", Color.green);
        if (parent != null)
        {
            parent.ShowPathNode();
        }
    }

    public void ReSetNode()
    {
        F = 0;
        G = 0;
        H = 0;
        isWall = false;
        parent = null;
        nodeRender.material.SetColor("_Color", Color.white);
    }

    public void ReSetNormalNode()
    {
        F = 0;
        G = 0;
        H = 0;
        parent = null;
        if(!isWall)
            nodeRender.material.SetColor("_Color", Color.white);
    }


    public void SetMap(Map m)
    {
        map = m;
    }
    public void SetEndNode()
    {
        map.ReFind();
        map.StartFindPath(map.player.GetCurNode(), this);
    }

    public NodeItem GetNodeItem()
    {
        return nodeItem;
    }
}
