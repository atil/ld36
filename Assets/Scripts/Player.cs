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
    public BoxCollider BoxColl;

    private Ui _ui;

	void Start ()
    {
        _fpsController = GetComponent<FirstPersonController>();
        BoxColl = GetComponent<BoxCollider>();
        _midScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        _fireLayer = ~(1 << LayerMask.NameToLayer("Fire"));
        _ui = FindObjectOfType<Ui>();
    }
	
	void Update ()
    {
        Ray midRay = Camera.main.ScreenPointToRay(_midScreen);

        RaycastHit hit;
        if (Physics.Raycast(midRay, out hit, 2f, _fireLayer)) // Passes through fire
        {
            var hitObj = hit.transform;
            if ((hitObj.GetComponent<Clay>() || hitObj.GetComponent<Brick>())
                && _heldItem == null)
            {
                _ui.SetCrosshairTo(hitObj.gameObject);
            }
            else
            {
                _ui.SetCrosshairTo(null);
            }
        }
        else
        {
            _ui.SetCrosshairTo(null);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(midRay, out hit, 2f, _fireLayer)) // Passes through fire
            {
                var hitObj = hit.transform;
                if (hitObj.GetComponent<Clay>() || hitObj.GetComponent<Brick>()
                    && _heldItem == null)
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
            if (_heldItem.GetComponent<Brick>())
            {
                _heldItem.GetComponent<Brick>().IsGhost = false;
            }
            _heldItem.GetComponent<Collider>().enabled = true;
            _heldItem.GetComponent<Rigidbody>().useGravity = true;
            _heldItem = null;
            HoldSlot.DetachChildren();

            _fpsController.IsCursorLocked = true;
            _fpsController.IsMouseLookEnabled = true;
            _rotateAnchor = Input.mousePosition;
        }

        if (_heldItem != null && _heldItem.GetComponent<Brick>())
        {
            _heldItem.position = HoldSlot.position;
            _heldItem.GetComponent<Brick>().IsGhost = false;

            if (Input.GetKey(KeyCode.F))
            {
                if (Physics.Raycast(midRay, out hit, 2f, _fireLayer)) 
                {
                    var hitBrick = hit.transform.GetComponent<Brick>();
                    if (hitBrick != null)
                    {
                        var t = hitBrick.GetNeighborSlot(hit.normal);
                        _heldItem.GetComponent<Brick>().IsGhost = true;
                        _heldItem.position = t.position;
                        _heldItem.rotation = t.rotation;
                    }
                }

            }

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
