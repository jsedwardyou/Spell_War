using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public Vector2 pos;
    public property NodeProperty;

    public enum property { Attack, Speed, Defense };

    public bool visited = false;

    public void Start()
    {       
        int randomNum = Random.Range(0, 3);
        NodeProperty = (property)randomNum;
        SetColor();
    }

    public void SetColor() {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        switch (NodeProperty) {
            case (property)0:
                sprite.color = Color.red;
                break;
            case (property)1:
                sprite.color = Color.green;
                break;
            case (property)2:
                sprite.color = Color.blue;
                break;
        }
    }

    public void ChangeNodeProperty() {
        int randomNum = Random.Range(0, 3);
        NodeProperty = (property)randomNum;
        SetColor();
    }
}
