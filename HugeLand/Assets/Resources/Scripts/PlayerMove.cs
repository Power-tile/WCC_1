﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove {
    public bool moving = false;
    public int selfNumber;
    public int maxEyeOfPlayer = maxeye;
    public int maxMoveOfPlayer = maxmove;

	// Use this for initialization
	void Start () {
        moving = false;
        selfNumber = (int)(this.name.Split('r')[1][0] - '0');
	}
	
	// Update is called once per frame
	void Update () {
        if (currentPlayerNumber == selfNumber) {
            Debug.DrawRay(transform.position, transform.forward);

            if (!moving) {
                FindPath(maxEyeOfPlayer, maxMoveOfPlayer);
                CheckMouse();
            } else {
                Move(this);
            }
        }
    }

    void CheckMouse() {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                GameObject now = hit.collider.gameObject;
                while (now.tag != "Tile") {
                    now = now.transform.parent.gameObject;
                }

                Tile t = now.GetComponent<Tile>();
                if (t.selectable) {
                    MoveToTile(this, t);
                }
            }
        }
    }
}
