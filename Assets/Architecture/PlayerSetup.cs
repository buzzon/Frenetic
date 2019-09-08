using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentToDisable;
    Camera preview;

    void Start()
    {
        if (!isLocalPlayer)
            foreach (var component in componentToDisable)
                component.enabled = false;

        preview = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (preview != null)
            preview.gameObject.SetActive(false);

    }

    private void OnDisable()
    {
        if (preview != null)
            preview.gameObject.SetActive(true);
    }
}
