using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTouch : MonoBehaviour {

    public GameObject spawnPos;

    public Pattern[] patternList;

    private LayerMask layermask = (1 << 8);

    private Node currentNode;
    private Node previousNode;

    public GameObject path;
    private Path[] paths = new Path[12];

    public GameObject node;
    private Node[] nodes = new Node[7];
    private List<Node> nodeList = new List<Node>();

    private List<Path> pathList = new List<Path>();

    private List<Node.property> NodeProperty = new List<Node.property>();

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
        PatternDetection();
    }

    private void PatternDetection() {
        RaycastHit2D hit = mousePointOnHexagon();
        if (!hit) return;

        Mouse(hit);
    }

    private RaycastHit2D mousePointOnHexagon() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, layermask);

        return hit;
    }

    private void Mouse(RaycastHit2D hit) {
        //onMouseDown
        if (Input.GetMouseButtonDown(0))
        {
            previousNode = hit.transform.GetComponent<Node>();
        }

        //onMouseDrag
        if (Input.GetMouseButton(0))
        {
            currentNode = hit.transform.GetComponent<Node>();
            foreach (Path p in paths)
            {
                if (!p.visited && p.ContainsNodes(previousNode, currentNode))
                {
                    previousNode = currentNode;
                    p.visited = true;
                    pathList.Add(p);
                    p.ChangeColor(Color.red);
                    if (!currentNode.visited) {
                        NodeProperty.Add(currentNode.NodeProperty);
                        currentNode.visited = true;
                        nodeList.Add(currentNode);
                    }
                }
            }
        }

        //onMouseUp
        if (Input.GetMouseButtonUp(0)) {
            
            foreach (Pattern p in patternList) {
                if (hasPattern(p)) {
                    if (p.monster == null) return;
                    GameObject spawnedMonster = Instantiate(p.monster, spawnPos.transform.position, Quaternion.identity);
                    spawnedMonster.transform.Rotate(new Vector3(0, 180, 0));
                    foreach (Node.property pro in NodeProperty) {
                        spawnedMonster.GetComponent<CharacterProperty>().properties.Add(pro);
                    }
                }
            }
            resetProperties();
        }
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

    private void resetProperties() {
        foreach (Path p in pathList)
        {
            p.visited = false;
            p.ChangeColor(Color.white);
        }

        foreach (Node n in nodes)
        {
            n.visited = false;
        }

        foreach (Node n in nodeList) {
            n.ChangeNodeProperty();
        }

        pathList.Clear();
        NodeProperty.Clear();
        nodeList.Clear();
       
    }
}
