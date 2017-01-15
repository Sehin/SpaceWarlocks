using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
    public GameObject target;
    private Camera thisCamera;

	// Use this for initialization
	void Start () {
        thisCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        thisCamera.transform.LookAt(target.transform.position);     
   	}
}
