using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AddAudio : NetworkBehaviour
{
    Camera cam;
    public GameObject camObject;
    // Start is called before the first frame update
    void Start()
    {
        cam = camObject.GetComponent<Camera>();
        if(isLocalPlayer)
        {
            camObject.AddComponent<AudioListener>();
        }
        else
            Destroy(cam);
            
    }
}
