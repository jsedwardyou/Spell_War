using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProperty : MonoBehaviour {

    public GameObject[] Thighs;
    public GameObject[] Arms;
    public GameObject Chest;

    public List<Node.property> property = new List<Node.property>();

	// Use this for initialization
	void Start () {
        foreach (Node.property p in property) {
            switch (p) {
                case (Node.property)0:
                    foreach (GameObject arm in Arms) {
                        arm.transform.localScale += new Vector3(1, 1, 1);
                    }
                    break;
                case (Node.property)1:
                    foreach (GameObject thigh in Thighs)
                    {
                        thigh.transform.localScale += new Vector3(1, 1, 1);
                    }
                    break;
                case (Node.property)2:
                    Chest.transform.localScale += new Vector3(1, 1, 1);
                    break;
            }
        }
	}
	
	
}
