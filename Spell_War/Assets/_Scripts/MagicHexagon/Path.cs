using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public Node Node1;
    public Node Node2;

    public bool visited;

    public void ChangeColor(Color color) {
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = color;
    }

    public bool ContainsNodes(Node n1, Node n2) {
        if ((Node1 == n1 || Node1 == n2) && (Node2 == n1 || Node2 == n2))
        {
            return true;
        }
        else
            return false;
    }
}
 