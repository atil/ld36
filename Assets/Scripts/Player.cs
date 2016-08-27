using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public Transform HoldSlot;

    private Transform _heldItem;
    private Vector3 _midScreen;
    private LayerMask _fireLayer;

    private Vector3 _rotateAnchor;
    private FirstPersonController _fpsController;

	void Start ()
    {
        _fpsController = GetComponent<FirstPersonController>();
        _midScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        _fireLayer = ~(1 << LayerMask.NameToLayer("Fire"));
    }
	
	void Update ()
    {
        Ray midRay = Camera.main.ScreenPointToRay(_midScreen);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(midRay, out hit, 2f, _fireLayer)) // Passes through fire
            {
                var hitObj = hit.transform;
                if (hitObj.GetComponent<Clay>()
                    || hitObj.GetComponent<Brick>())
                {
                    hitObj.SetParent(HoldSlot);
                    hitObj.localPosition = Vector3.zero;
                    _heldItem = hitObj;

                    // PUT STILL DAMMIT
                    hitObj.GetComponent<Collider>().enabled = false;
                    hitObj.GetComponent<Rigidbody>().useGravity = false;
                    hitObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    hitObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
            }
        }

        if (Input.GetMouseButtonDown(1) && _heldItem != null)
        {
            _heldItem.GetComponent<Collider>().enabled = true;
            _heldItem.GetComponent<Rigidbody>().useGravity = true;
            _heldItem = null;
            HoldSlot.DetachChildren();
        }


        if (_heldItem != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _fpsController.IsCursorLocked = false;
                _fpsController.IsMouseLookEnabled = false;
            }

            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                _fpsController.IsCursorLocked = true;
                _fpsController.IsMouseLookEnabled = true;
                _rotateAnchor = Input.mousePosition;
            }

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                var delta = _rotateAnchor - Input.mousePosition;
                _heldItem.Rotate(Vector3.up, delta.x, Space.World);
                _heldItem.Rotate(Vector3.right, delta.y, Space.World);
                _rotateAnchor = Input.mousePosition;
            }

        }
    }
}
