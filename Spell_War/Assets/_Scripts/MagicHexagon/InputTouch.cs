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

    public GameObject node;
    private Node[] nodes = new Node[7];

    private List<Path> pathList = new List<Path>();
    private List<Node> nodeList = new List<Node>();
    private List<Vector2> posList = new List<Vector2>();

    private List<Path> possiblePattern = new List<Path>();

    // Use this for initialization
    void Start() {
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = path.transform.GetChild(i).GetComponent<Path>();
        }
        for (int i = 0; i < nodes.Length; i++) {
            nodes[i] = node.transform.GetChild(i).GetComponent<Node>();
        }
    }

    // Update is called once per frame
    void Update() {
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
                    p.ChangeColor(Color.red);
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            foreach (Path p in pathList) {
                //Debug.Log(p.name);
                p.visited = false;
                p.ChangeColor(Color.white);
            }
            foreach (Pattern p in patternList) {
                //Find out whether pattern and vectors match
                //FindPatternNumbers(p);
                if (hasPattern(p)) {
                    Debug.Log(p.name);
                }
            }
            pathList.Clear();
            nodeList.Clear();
            posList.Clear();
            possiblePattern.Clear();

            
        }
    }

    private bool CheckPattern(Pattern p) {

        if (p.pos.Length != posList.Count) {
            return false;
        }
        for (int i = 0; i < p.pos.Length; i++) {
            if (!posList.Contains(p.pos[i]))
                return false;
        }

        return true;
    }

    private int FindPatternNumbers(Pattern p) {
        Debug.Log(p.name);
        int count = 0;

        List<Path> tempPath = new List<Path>();

        foreach (Node n in nodes) {
            tempPath.Clear();
            Node currentN = n;
            int index = 0;
            for (int i = 0; i < p.pos.Length; i++) {
                Node nextNode = FindNextNode(nodes, currentN, p.pos[i]);
                if (nextNode == null) { break; }

               
                foreach (Path path in paths) {
                    if (path.ContainsNodes(currentN, nextNode)) {
                        Debug.Log(path.name);
                        tempPath.Add(path);
                    }
                }
                
                index++;
                currentN = nextNode;
                
            }

            if (index == p.pos.Length) {
                count++;
                for (int i = 0; i < tempPath.Count; i++) {
                    possiblePattern.Add(tempPath[i]);
                }
            }
            
        }

        return count;
    }

    private bool hasPattern(Pattern p) {

        bool patternExist = false;

        List<Path> temp = new List<Path>();

        foreach (Node n in nodes) {
            Node currentN = n;
            for (int i = 0; i < p.pos.Length; i++) {
                Node nextNode = FindNextNode(nodes, currentN, p.pos[i]);
                if (nextNode == null) { temp.Clear();  break; }

                foreach (Path path in paths) {
                    if (path.ContainsNodes(currentN, nextNode)) {
                        temp.Add(path);
                    }
                }
                
                currentN = nextNode;
            }

            if (temp.Count == 0) continue;
            if (temp.Count != pathList.Count) continue;

            foreach (Path path in temp) {
                if (!pathList.Contains(path)) {
                    patternExist = false; break;
                }
                patternExist = true;
            }
            if (patternExist) {
                return true;
            }
            temp.Clear();
        }
        return false;
    }

    private Node FindNextNode(Node[] nodes, Node node, Vector2 direction) {
        for (int i = 0; i < nodes.Length; i++) {
            if (nodes[i].pos == node.pos + direction) {
                return nodes[i];
            }
        }
        return null;
    }
}
