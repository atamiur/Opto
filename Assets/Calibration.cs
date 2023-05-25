using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;


using static GamepadUtils;

public class Calibration : MonoBehaviour
{
    // placeholders definition

    [SerializeField] private Transform  _EyeLeft        ;                       // left eye position
    [SerializeField] private Transform  _EyeRight       ;                       // rigth eye position
    [SerializeField] private Transform  _EyeLookAt      ;                       // camera's convergence point

    [SerializeField] private Camera     _CameraLeft     ;                       // left camera's current position
    [SerializeField] private Camera     _CameraRight    ;                       // right camera's current position

    [SerializeField] private float      _CalibrationSpeed   = 0f    ;
    
    [SerializeField] private Vector3    _prefabRighCamPosition;
    [SerializeField] private Vector3    _prefabLeftCamPosition;

    [SerializeField] public  bool       CalibrationMode     = false ;

    private void Awake()
    {
        // Set placeholder's position to current camera's gameObjects position in environment
        _EyeLeft = _CameraLeft.transform;
        _EyeRight = _CameraRight.transform;
        //_EyeLookAt.LookAt();

        // init Gamepad
        GamepadUtils.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCalibrationMode();
        if (CalibrationMode) { CalibrateCameras();}
    }

    void SetCalibrationMode()
    {
        if (CalibrationMode)
        {
            if ((ButtonValue("Select") != 0 && ButtonValue("MainSelect") != 0) || (
                (Input.GetKey(KeyCode.LeftControl)) && 
                (Input.GetKey(KeyCode.LeftShift)) && 
                (Input.GetKeyDown(KeyCode.Q)) ) )
            {
                CalibrationMode = false;
            }
        } else {
            if ((ButtonValue("Select") != 0 && ButtonValue("MainSelect") != 0) || (
                (Input.GetKey(KeyCode.LeftControl)) && 
                (Input.GetKey(KeyCode.LeftShift)) && 
                (Input.GetKeyDown(KeyCode.Q)) ) )
            {
                CalibrationMode = true;
            }
        }
    }


    void CalibrateCameras()
    {
        // apply position transformation, based on pressed buttons
        float _eyeSeparation = 0.0f;
        float _convergenceOffset = 0.0f;

        // claculate eye separation
        _eyeSeparation += ButtonValue("Right") * _CalibrationSpeed / 2;
        _eyeSeparation += (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) * _CalibrationSpeed / 2;
        
        
        _eyeSeparation -= ButtonValue("Left") * _CalibrationSpeed / 2;
        _eyeSeparation -= (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0) * _CalibrationSpeed / 2;

        // calculate convergence distance
        _convergenceOffset += ButtonValue("Up") * _CalibrationSpeed;
        _convergenceOffset += (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) * _CalibrationSpeed;
        _convergenceOffset -= ButtonValue("Down") * _CalibrationSpeed;
        _convergenceOffset -= (Input.GetKey(KeyCode.DownArrow) ? 1 : 0) * _CalibrationSpeed;


        // apply offset in gameobjects 
        _EyeLeft.transform.Translate ( new Vector3(-_eyeSeparation, 0, _convergenceOffset)     ); // Cameras: Left Eye
        _EyeRight.transform.Translate( new Vector3(_eyeSeparation, 0, _convergenceOffset)     ); // Cameras: Right Eye
            
        // Convergence
        //_EyeLookAt.transform.Translate( new Vector3(0, 0, _convergenceOffset)); // convergence distance
        
        _EyeLeft.LookAt(_EyeLookAt);
        _EyeRight.LookAt(_EyeLookAt);
    }
}
