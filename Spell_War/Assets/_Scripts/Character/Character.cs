using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : CharacterProperty {

    Animator anim;

	// Use this for initialization
	void Start () {
        int Attack = -1; int Defense = -1; int Speed = -1;
        foreach (Node.property property in properties) {
            switch (property) {
                case (Node.property)0:
                    Attack += 1;
                    break;
                case (Node.property)1:
                    Speed += 1;
                    break;
                case (Node.property)2:
                    Defense += 1;
                    break;
            }
        }
        SetItems((Node.property)0, Attack);
        SetItems((Node.property)1, Speed);
        SetItems((Node.property)2, Defense);

        anim = GetComponent<Animator>();

    }

    private void SetItems(Node.property property, int num) {
        switch (property) {
            case (Node.property)0:
                if (num > -1) {
                    swords[num].SetActive(true);
                }
                break;
            case (Node.property)1:
                if (num > -1)
                {
                    boots[0].SetActive(true);
                }
                break;
            case (Node.property)2:
                if (num > -1)
                {
                    shields[num].SetActive(true);
                }
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!detect()) {
            move();
        }
	}

    private RaycastHit2D detect() {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, -Vector2.right, attackRange);

        return hit;
    }

    private void move()
    {
        anim.Play("move");
        transform.Translate(-Vector2.right * Time.deltaTime * speed);
    }
}
