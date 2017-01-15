using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AIController : MonoBehaviour {

    private Character character;
	public Vector2 platformCenter = new Vector2(-20, 0);
	public Vector2 platformDimensions = new Vector2 (10, 10);
	public int waypointsCount = 15;
	public float waypointRadius = 0.4f;
	public List<Vector2> waypoints = new List<Vector2>();
	public int currentWaypoint = 0;

   public Vector3 moveVector;
    // Use this for initialization
    void Start () {
		//generate waypoints 
		for (var i = 0; i < waypointsCount; i++) {
			waypoints.Add(new Vector2(Random.value*platformDimensions.x - platformDimensions.x/2 + platformCenter.x,
				Random.value*platformDimensions.y - platformDimensions.y/2 + platformCenter.y));
		}
		currentWaypoint = 0;

        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update () {
		var i = getCurrentWaypointNumber ();
		var vm = getDirectionToWaypoint(i);
		moveVector = new Vector3 (vm.x, 0, vm.y);
	}

    void FixedUpdate(){
		character.Move (moveVector);
	}


	int getCurrentWaypointNumber(){
		var current = waypoints[currentWaypoint];
		var currentCharacterPosition = v2(character.transform.position);
		if (Vector2.Distance(current, currentCharacterPosition)<waypointRadius){
			currentWaypoint=(++currentWaypoint)%waypointsCount;
		}
		return currentWaypoint;
	}

	Vector2 getDirectionToWaypoint(int waypointNumber){
		var waypoint = waypoints[waypointNumber];
		var currentCharacterPosition = v2(character.transform.position);
		return waypoint - currentCharacterPosition;
	}

	Vector2 v2(Vector3 v){
		return new Vector2 (v.x, v.z);
	}

}
