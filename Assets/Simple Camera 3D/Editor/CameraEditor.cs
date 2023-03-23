using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraScript))]
public class CameraEditor : Editor
{
    bool Statistics = false;
    bool OptimizeModes = false;
    bool CustomOptimized = false;
    bool is3rdPerson;

    string status = "Extra Settings";
    string stat = "Optimize Modes";
    string stats = "Custom";

    float minLimit = -250;
    float maxLimit = 250;

    Vector3 ThreeDOffset;



    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CameraScript cameraScript = (CameraScript)target;

        is3rdPerson = cameraScript.Is3rdPerson;

        //Warning Message
        if (cameraScript.transform.tag != "MainCamera")
        {
            EditorGUILayout.HelpBox("Script is not in the camera or camera tag is changed!", MessageType.Error);
        }

        //IsActive Bool
        cameraScript.IsActive = EditorGUILayout.Toggle("Active Camera", cameraScript.IsActive);

        if(cameraScript.IsActive)
        {
            //Debug Mode
            if(cameraScript.DebugMode)
            {
                Debug.Log("Sensitivity = " + cameraScript.Sensitivity);
                Debug.Log("1st Person = " + cameraScript.Is1stPerson);
                Debug.Log("3rd Person = " + cameraScript.Is3rdPerson);
                Debug.Log("Camera Lock = " + cameraScript.IsLockCursor);
                Debug.Log("Debug Mode = " + cameraScript.DebugMode);
                Debug.Log("Min Sensitivity = " + cameraScript.MinSensitivity);
                Debug.Log("Max Sensitivity = " + cameraScript.MaxSensitivity);
                Debug.Log("Camera Active = " + cameraScript.IsActive);
                Debug.Log("Y Offset = " + cameraScript.StartLookY);
                Debug.Log("Z Offset = " + cameraScript.StartLookZ);
                Debug.Log("Min Y = " + cameraScript.MinY);
                Debug.Log("Max Y = " + cameraScript.MaxY);
                cameraScript.DebugMode = false;
            }
            
            //Senitivity Slider
            cameraScript.Sensitivity = EditorGUILayout.Slider("Sensitivity", cameraScript.Sensitivity, cameraScript.MinSensitivity, cameraScript.MaxSensitivity);

            //List
            GUIContent arrayList = new GUIContent("Camera Settings");
            cameraScript.listIndx = EditorGUILayout.Popup(arrayList, cameraScript.listIndx, cameraScript.Lists);

            if (cameraScript.listIndx == 0)
            {
                cameraScript.Is1stPerson = true;
                cameraScript.Is3rdPerson = false;
            }
            else if (cameraScript.listIndx == 1)
            {
                cameraScript.Is3rdPerson = true;
                cameraScript.Is1stPerson = false;
            }
            if(is3rdPerson)
            {
                if (cameraScript.PlayerPlayer == null)
                {
                    EditorGUILayout.HelpBox("Create a GameObject and make it the parent of the Camera", MessageType.Info);
                }
            }

            //Editing
            Statistics = EditorGUILayout.Foldout(Statistics, status);
            if (Statistics)
            {
                if (Selection.activeTransform)
                {
                    //IsLockCursor Bool
                    cameraScript.IsLockCursor = EditorGUILayout.Toggle("Lock Cursor", cameraScript.IsLockCursor);

                    cameraScript.DebugMode = EditorGUILayout.Toggle("Debug Mode", cameraScript.DebugMode);

                    EditorGUILayout.Space(4);

                    //Sensitivity Level Constraints
                    EditorGUILayout.LabelField("Sensitivity Constraints:");

                    //Float field for min
                    cameraScript.MinSensitivity = EditorGUILayout.FloatField("Min", cameraScript.MinSensitivity);

                    //Float field for max
                    cameraScript.MaxSensitivity = EditorGUILayout.FloatField("Max", cameraScript.MaxSensitivity);

                    if (cameraScript.RoundFloats)
                    {
                        cameraScript.MaxSensitivity = Mathf.RoundToInt(cameraScript.MaxSensitivity);
                        cameraScript.MinSensitivity = Mathf.RoundToInt(cameraScript.MinSensitivity);
                        cameraScript.Sensitivity = Mathf.RoundToInt(cameraScript.Sensitivity);
                    }

                    EditorGUILayout.Space(6);

                    //1st Person Offset
                    EditorGUILayout.LabelField("Offset:");

                    GUILayout.BeginVertical();



                    //Y Rotation Variable
                    cameraScript.StartLookY = EditorGUILayout.FloatField("Y Rot", cameraScript.StartLookY);

                    //Z Rotation Variable
                    cameraScript.StartLookZ = EditorGUILayout.FloatField("Z Rot", cameraScript.StartLookZ);

                    GUILayout.EndVertical();

                    GUILayout.Space(6);

                    //Camera Constraints
                    GUILayout.Label("Y Constraints:");

                    //MinY Value
                    cameraScript.MinY = EditorGUILayout.FloatField("Min", cameraScript.MinY);


                    //MaxY Value
                    cameraScript.MaxY = EditorGUILayout.FloatField("Max", cameraScript.MaxY);

                    //Round floats if bool is active
                    if (cameraScript.RoundFloats)
                    {
                        cameraScript.MaxY = Mathf.RoundToInt(cameraScript.MaxY);
                        cameraScript.MinY = Mathf.RoundToInt(cameraScript.MinY);
                    }

                    //Slider
                    EditorGUILayout.MinMaxSlider(ref cameraScript.MinY, ref cameraScript.MaxY, minLimit, maxLimit);

                    status = "Extra Settings";
                }
            }

            OptimizeModes = EditorGUILayout.Foldout(OptimizeModes, stat);
            if (OptimizeModes)
            {
                if (Selection.activeTransform)
                {
                    if (GUILayout.Button("Not Optimized"))
                    {
                        cameraScript.NotOptimized();
                    }
                    EditorGUILayout.Space(1);
                    if (GUILayout.Button("Moderately Optimized"))
                    {
                        cameraScript.ModeratelyOptimized();
                    }
                    EditorGUILayout.Space(1);
                    if (GUILayout.Button("Extremely Optimized"))
                    {
                        cameraScript.ExtremelyOptimized();
                    }
                    EditorGUILayout.Space(1);
                    CustomOptimized = EditorGUILayout.Foldout(CustomOptimized, stats);
                    if (CustomOptimized)
                    {
                        if (Selection.activeTransform)
                        {
                            cameraScript.RoundFloats = EditorGUILayout.Toggle("Round Floats", cameraScript.RoundFloats);
                            cameraScript.LockCursorLive = EditorGUILayout.Toggle("Lock Cursor Live", cameraScript.LockCursorLive);
                        }
                    }
                }
            }
        }

        EditorGUILayout.Space(4);

        //Reset Button
        if (GUILayout.Button("Reset Values"))
        {
            cameraScript.Reset();
        }

        //End of GUI
    }



    public void OnInspectorUpdate()
    {
        this.Repaint();
    }
}