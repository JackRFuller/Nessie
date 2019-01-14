using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    private Camera menuCamera;

    // Start is called before the first frame update
    void Start()
    {
        menuCamera = GetComponent<Camera>();
        GameManager.Instance.NetworkManager.ServerFull += TurnOffCamera;
    }

    private void TurnOffCamera()
    {
        menuCamera.enabled = false;
    }
}
