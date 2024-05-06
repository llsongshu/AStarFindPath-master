using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeItem : MonoBehaviour
{
    public Node self;
    public void SetNode(Node node)
    {
        self = node;
    }

    private void OnMouseDown()
    {
        self.SetEndNode();
    }
}
