using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 keyboardInput;
    private float zoomAxis;

    public Vector2 KeyboardInput {get {return keyboardInput;}}
    public float ZoomAxis { get {return zoomAxis;}}

    // Update is called once per frame
    void Update()
    {
        GetKeyBoardInput();
        GetMouseScrollInput();
    }

    void GetKeyBoardInput()
    {
        keyboardInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    void GetMouseScrollInput()
    {
        zoomAxis = Input.GetAxis("Mouse ScrollWheel");
    }
}
