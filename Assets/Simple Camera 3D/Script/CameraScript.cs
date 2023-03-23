using System;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //float pool
    [HideInInspector] public float MinSensitivity = 0;
    [HideInInspector] public float Sensitivity = 90;
    [HideInInspector] public float MaxSensitivity = 200;
    [HideInInspector] public float StartLookY = 0.0f;
    [HideInInspector] public float StartLookZ = 0.0f;
    [HideInInspector] public float MaxY = 90f;
    [HideInInspector] public float MinY = -90f;
    //bool pool
    [HideInInspector] public bool Is1stPerson;
    [HideInInspector] public bool IsActive;
    [HideInInspector] public bool IsLockCursor;
    [HideInInspector] public bool DebugMode;
    [HideInInspector] public bool LockCursorLive;
    [HideInInspector] public bool Is3rdPerson;
    [HideInInspector] public bool RoundFloats;
    //list pool
    [HideInInspector] public int listIndx = 0;
    [HideInInspector] public string[] Lists = new string[] { "1st Person", "3rd Person" };
    //gameobject pool
    [HideInInspector] public GameObject GameObject3d;
    public GameObject Player;
    public GameObject PlayerPlayer;
    public static CameraScript instance;
    public float xMouse;

    private void Awake()
    {
        instance = this;
    }

    //private pool
    private float xRotation = 0.0f;
    //private pool

    //Start//

    void Start()
    {
        //Changes between 1st or 3rd person view
        if(listIndx == 0)
        {
            Is1stPerson = true;
            Is3rdPerson = false;
        } else if (listIndx == 1)
        {
            Is3rdPerson = true;
            Is1stPerson = false;
        }
        
        //Locks Cursor if LockCursor bool is active (On start)
        if(IsLockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        //Finds parent of Camera
        Player = transform.parent.gameObject;

        //Finds parent of parent if 3rd Person
        if (Is3rdPerson)
        {
            PlayerPlayer = Player.transform.parent.gameObject;
        }
    }

    //Update//

    void LateUpdate()
    {
        //Changes between 1st or 3rd person view live
        if (listIndx == 0)
        {
            Is1stPerson = true;
            Is3rdPerson = false;
        }
        else if (listIndx == 1)
        {
            Is3rdPerson = true;
            Is1stPerson = false;
        }

        //Locks Cursor if LockCursor and LockCursorLive bool is true
        if (IsLockCursor && LockCursorLive)
        {
            Cursor.lockState = CursorLockMode.Locked;
        } else if (IsLockCursor == false && LockCursorLive)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        //Runs 1st or 3rd person depending on your settings
        if(IsActive && Is1stPerson)
        {
            FirstPerson();
        } else if (IsActive && Is3rdPerson)
        {
            ThirdPerson();
        }

        //Rounds floats if bool is true
        if(RoundFloats)
        {
            Sensitivity = Mathf.RoundToInt(Sensitivity);
            MinSensitivity = Mathf.RoundToInt(MinSensitivity);
            MaxSensitivity = Mathf.RoundToInt(MaxSensitivity);
            StartLookY = Mathf.RoundToInt(StartLookY);
            StartLookZ = Mathf.RoundToInt(StartLookZ);
        }
    }

    //1st Person//
    
    public void FirstPerson()
    {
        //If the 1st person bool and is active bool is active, the 1st person script will play

        //Getting the mouse input values
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        xMouse = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        //Limiting the camera's Y rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, MinY, MaxY);

        //Starting camera rotation
        transform.localRotation = Quaternion.Euler(xRotation, StartLookY, StartLookZ);

        //Change camera rotation based on mouse input values
        Player.transform.Rotate(Vector3.up * mouseX);
    }

    //Third Person//

    public void ThirdPerson()
    {
        //Getting the mouse input values
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;

        //Limiting the camera's Y rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, MinY, MaxY);

        //Starting camera rotation
        Player.transform.localRotation = Quaternion.Euler(xRotation, StartLookY, StartLookZ);

        //Change camera rotation based on mouse input values
        PlayerPlayer.transform.Rotate(Vector3.up * mouseX);

        transform.LookAt(Player.transform.position);
    }


    //Editor Commands//

    //Editor Trigger
    public void Reset()
    {
        Debug.Log("Settings Reset");
        Is1stPerson = true;
        RoundFloats = true;
        IsActive = true;
        IsLockCursor = true;
        LockCursorLive = true;
        Is3rdPerson = false;
        DebugMode = false;
        Sensitivity = 90f;
        MinSensitivity = 0f;
        MaxSensitivity = 200f;
        StartLookY = 0.0f;
        StartLookZ = 0.0f;
        MaxY = 90f;
        MinY = -90f;
        listIndx = 0;
    }


    //Editor Trigger
    public void NotOptimized()
    {
        Debug.Log("Not Optimized");
        RoundFloats = true;
        LockCursorLive = true;
    }


    //Editor Trigger
    public void ModeratelyOptimized()
    {
        Debug.Log("Moderately Optimized");
        RoundFloats = true;
        LockCursorLive = false;
    }


    //Editor Trigger
    public void ExtremelyOptimized()
    {
        Debug.Log("Extremely Optimized");
        RoundFloats = false;
        LockCursorLive = false;
    }
}