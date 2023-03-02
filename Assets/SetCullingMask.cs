using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class SetCullingMask : MonoBehaviour
{
    
    // Culling Mask
    [SerializeField] public int CameraID    ;
    
    Camera cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<Camera>();

        cm.cullingMask =  Globals.GetCullingMask( (Side)CameraID, (Side)amblyopicEye);

        Debug.Log(CameraID + ": " + cm.cullingMask);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
