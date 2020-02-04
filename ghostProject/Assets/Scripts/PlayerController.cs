using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Input_Sources handType; 
    //public SteamVR_Action_Boolean teleportAction; 
    //public SteamVR_Action_Boolean grabAction;
    public SteamVR_Action_Boolean move;
    public SteamVR_Action_Boolean rotateRight;
    public SteamVR_Action_Boolean rotateLeft;

    public GameObject player;
    public GameObject playerCamera;
    private float _playerSpeed;
    private Vector3 _playerPosition;
    private float _playerRotation;
    private float _playerRotationSpeed;

    void Start()
    {
        move.AddOnStateDownListener(DpadNorthDown, handType);
        move.AddOnStateUpListener(DpadNorthUp, handType);

        rotateRight.AddOnStateDownListener(DpadEastDown, handType);
        rotateRight.AddOnStateDownListener(DpadEastUp, handType);

        rotateLeft.AddOnStateDownListener(DpadWestDown, handType);
        rotateLeft.AddOnStateDownListener(DpadWestUp, handType);
    }

    public void DpadNorthUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("dpad north is up");
        _playerSpeed = 0.0f;
        _playerRotationSpeed = 0.0f;
    }

    public void DpadNorthDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("dpad north is down");
        Movement();
    }

    public void DpadEastUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        _playerRotationSpeed = 0.0f;
    }

    public void DpadEastDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        RotateEast();
    }

    public void DpadWestUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        _playerRotationSpeed = 0.0f;
    }

    public void DpadWestDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        RotateWest();
    }

    public void Movement() {
        _playerSpeed = 2.0f;
        _playerRotationSpeed = 1.0f;
        _playerPosition = new Vector3(0.0f, 0.0f, 1.0f);
    }

    public void RotateEast()
    {
        _playerRotationSpeed = 1.0f;
        player.GetComponent<Rigidbody>().transform.Rotate(0.0f, 45 * _playerRotationSpeed, 0.0f);
    }

    public void RotateWest()
    {
        _playerRotationSpeed = 1.0f;
        player.GetComponent<Rigidbody>().transform.Rotate(0.0f, -45 * _playerRotationSpeed, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (GetTeleportDown())
        //{
        //    print("Teleport " + handType);
        //}

        //if (GetGrab())
        //{
        //    print("Grab " + handType);
        //}

        _playerRotation = playerCamera.transform.localRotation.y;
        player.GetComponent<Rigidbody>().transform.Rotate(0.0f, _playerRotation * _playerRotationSpeed, 0.0f);
        player.GetComponent<Rigidbody>().transform.Translate(_playerPosition * _playerSpeed * Time.deltaTime);
    }
}
