using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Input_Sources handType; 
    public SteamVR_Action_Boolean move;
    public SteamVR_Action_Boolean rotateRight;
    public SteamVR_Action_Boolean rotateLeft;
    public SteamVR_Action_Boolean jumpAction;

    public GameObject player;
    public GameObject playerCamera;
    private float _playerSpeed;
    private Vector3 _playerPosition;
    private float _playerRotation;
    private float _playerRotationSpeed;

    public bool DpadNorthUp()
    {
        Debug.Log("dpad north is up");
        _playerSpeed = 0.0f;
        _playerRotationSpeed = 0.0f;
        return move.GetState(handType);

    }

    public bool DpadNorthDown()
    {
        Debug.Log("dpad north is down");
        Movement();
        return move.GetState(handType);

    }

    public bool DpadEastUp()
    {
        Debug.Log("dpad east is up");
        _playerRotationSpeed = 0.0f;
        return rotateRight.GetState(handType);

    }

    public bool DpadEastDown()
    {
        Debug.Log("dpad east is down");
        RotateEast();
        return rotateRight.GetState(handType);

    }

    public bool DpadWestUp()
    {
        Debug.Log("dpad west is up");
        _playerRotationSpeed = 0.0f;
        return rotateLeft.GetState(handType);

    }

    public bool DpadWestDown()
    {
        Debug.Log("dpad west is down");
        RotateWest();
        return rotateLeft.GetState(handType);

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

    void FixedUpdate()
    {
        if (move.GetLastStateDown(handType))
        {
            DpadNorthDown();
        }else if(move.GetLastStateUp(handType))
        {
            DpadNorthUp();
        }

        if (rotateRight.GetLastStateDown(handType))
        {
            DpadEastDown();
        }else if(rotateRight.GetLastStateUp(handType))
        {
            DpadEastUp();
        }

        if (rotateLeft.GetLastStateDown(handType))
        {
            DpadWestDown();
        }
        else if (rotateLeft.GetLastStateUp(handType))
        {
            DpadWestUp();
        }

        _playerRotation = playerCamera.transform.localRotation.y;
        player.GetComponent<Rigidbody>().transform.Rotate(0.0f, _playerRotation * _playerRotationSpeed, 0.0f);
        player.GetComponent<Rigidbody>().transform.Translate(_playerPosition * _playerSpeed * Time.deltaTime);
    }
}
