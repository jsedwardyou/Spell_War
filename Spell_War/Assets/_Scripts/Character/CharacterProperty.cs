using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterProperty : MonoBehaviour {

    public GameObject[] swords;
    public GameObject[] shields;
    public GameObject[] boots;

    public List<Node.property> properties = new List<Node.property>();

    public float attackRange;
    public float speed;
}
