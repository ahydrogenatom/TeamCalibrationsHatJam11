﻿using UnityEngine;
using System.Collections;

public class Darkness : MonoBehaviour {

    public Transform greayscalePlane;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Ray rayToPlayerPos = Camera.main.ScreenPointToRay(screenPos);
        int layermask = (int)(1 << 8);
        RaycastHit hit;
        Debug.DrawRay(rayToPlayerPos.origin, rayToPlayerPos.direction*1000);
        if(Physics.Raycast(rayToPlayerPos, out hit, 1000))
        {
            greayscalePlane.GetComponent<Renderer>().material.SetVector("_PlayerPos", hit.point);
        }

	}
}
