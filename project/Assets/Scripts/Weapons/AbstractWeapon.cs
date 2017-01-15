using UnityEngine;
using Object = UnityEngine.Object;

public abstract class AbstractWeapon : MonoBehaviour, IWeapon {
    public abstract void shoot(Vector3 target);

//    public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
//    {
//        return Object.Instantiate(original, position, rotation);
//    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
