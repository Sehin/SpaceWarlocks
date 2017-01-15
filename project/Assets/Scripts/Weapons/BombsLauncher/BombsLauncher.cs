using UnityEngine.Networking;
using UnityEngine;

public class BombsLauncher : AbstractWeapon
{
    public GameObject bullet;
    public float shootingAngle = 30;
    public float distCorrectionCoef = 10;

    public override void shoot(Vector3 target)
    {
		var sourcePosition = transform.position;
        //var newBullet = Instantiate(bullet, sourcePosition, Quaternion.identity);
        var newBullet = Instantiate(bullet, sourcePosition, Quaternion.identity);
        //Calculate force to shoot

        var shotingAngleDeg = Mathf.Deg2Rad * shootingAngle;

        var verticalDist = sourcePosition.y - target.y;

        var dist = Vector2.Distance(new Vector2(sourcePosition.x, sourcePosition.z),
                                      new Vector2(target.x, target.z));

        dist -= verticalDist / Mathf.Tan(shotingAngleDeg)*dist/distCorrectionCoef;


        var direction = (target - sourcePosition).normalized;
        direction.y = 0;


        direction = Vector3.RotateTowards(direction, Vector3.up, shotingAngleDeg, 10.0f).normalized;
        
        var startingSpeed = Mathf.Sqrt(dist * 9.81f / Mathf.Sin(2 * shotingAngleDeg));

        newBullet.GetComponent<Rigidbody>().velocity = direction * startingSpeed;

        NetworkServer.Spawn(newBullet);
    }
}
