using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : MonoBehaviour {

    public Pattern[] patternList;

    private LayerMask layermask = (1 << 8);

    private Node currentNode;
    private Node previousNode;

    public GameObject path;
    private Path[] paths = new Path[12];

    private List<Path> pathList = new List<Path>();
    private List<Node> nodeList = new List<Node>();
    private List<Vector2> posList = new List<Vector2>();

    // Use this for initialization
    void Start () {
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = path.transform.GetChild(i).GetComponent<Path>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layermask);
        if (!hit) return;

        if (Input.GetMouseButtonDown(0)) {
            previousNode = hit.transform.GetComponent<Node>();
            nodeList.Add(previousNode);
        }

        if (Input.GetMouseButton(0)) {
            currentNode = hit.transform.GetComponent<Node>();
            foreach (Path p in paths) {
                if (!p.visited 
                    && (p.Node1 == currentNode || p.Node1 == previousNode) 
                    && (p.Node2 == currentNode || p.Node2 == previousNode)) {
                    posList.Add(currentNode.pos - previousNode.pos);
                    previousNode = currentNode;
                    p.visited = true;
                    nodeList.Add(currentNode);
                    pathList.Add(p);
                    
                }
            }   
        }

        if (Input.GetMouseButtonUp(0)) {
            foreach (Path p in pathList) {
                Debug.Log(p.name);
                p.visited = false;
            }
            foreach (Node n in nodeList) {
                Debug.Log(n.name);
            }
            foreach (Vector2 p in posList) {
                Debug.Log(p);
            }
            pathList.Clear();
            nodeList.Clear();
            posList.Clear();
        }
    }
}
