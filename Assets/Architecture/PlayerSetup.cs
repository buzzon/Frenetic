using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentToDisable;
    Camera preview;
    Canvas UI;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (!isLocalPlayer)
            foreach (var component in componentToDisable)
                component.enabled = false;

        preview = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (preview != null)
            preview.gameObject.SetActive(false);


        UI = GameObject.Find("UI").GetComponent<Canvas>();
        if (UI != null)
            UI.enabled = true;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (!isLocalPlayer) return;

        GameObject netCamera = GameObject.Find("NetCamera").gameObject;
        CameraManager cameraManager = netCamera.GetComponent(typeof(CameraManager)) as CameraManager;
        cameraManager.setTarget(GetComponent(typeof(Transform)) as Transform);
        cameraManager.enabled = true;

        GameObject miniCamera = GameObject.Find("MinimapCamera").gameObject;
        MiniCameraManager miniCameraManager = miniCamera.GetComponent(typeof(MiniCameraManager)) as MiniCameraManager;
        miniCameraManager.setTarget(GetComponent(typeof(Transform)) as Transform);
        miniCameraManager.enabled = true;
    }

    public void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (preview != null)
            preview.gameObject.SetActive(true);

        UI = GameObject.Find("UI").GetComponent<Canvas>();
        if (UI != null)
            UI.enabled = false;

        GameObject netCamera = GameObject.Find("NetCamera").gameObject;
        CameraManager cameraManager = netCamera.GetComponent(typeof(CameraManager)) as CameraManager;
        cameraManager.enabled = false;

        GameObject miniCamera = GameObject.Find("MinimapCamera").gameObject;
        MiniCameraManager nimiCameraManager = miniCamera.GetComponent(typeof(MiniCameraManager)) as MiniCameraManager;
        nimiCameraManager.enabled = false;
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
