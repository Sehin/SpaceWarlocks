using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class NetworkCharacterController : NetworkBehaviour {

    private Character character;
    private Vector3 move;
    private Camera cam;

    override public void OnStartLocalPlayer()
    {
        GetComponentInChildren<MeshRenderer>().material.color = Color.blue; //Почему то не работает
        // get the transform of the main camera
        if (Camera.main != null)
        {
            cam = Camera.main;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
            // we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
        }
    }

    void Start()
    {
        character = GetComponent<Character>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        var h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        var v = CrossPlatformInputManager.GetAxisRaw("Vertical");

        move = new Vector3(h, 0, v);

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            //get mouse target
            RaycastHit hitInfo;

            var mouseRay = cam.ScreenPointToRay(CrossPlatformInputManager.mousePosition);

            if (Physics.Raycast(mouseRay, out hitInfo, 1000.0f))
            {
                CmdFire(hitInfo.point);
            }
        }

    }

    [Command]
    void CmdFire(Vector3 target)
    {
        character.Shoot(target);
    }

    private void FixedUpdate()
    {
        character.Move(move);
    }
}
