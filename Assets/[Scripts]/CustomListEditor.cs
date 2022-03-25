// Website Source Code Origin: https://forum.unity.com/threads/display-a-list-class-with-a-custom-editor-script.227847/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor; // Gets Unity Inspector Editor API
 
// The Component script this custom Editor is for (CustomList)
[CustomEditor(typeof(GridOrganizer))]
public class CustomListEditor : Editor // base class where custom editor behaviour is derived
{
    // variable for storing the target (target is the (every) instance of script being inspected)
    GridOrganizer component;
    SerializedObject GetTarget;
 
    void OnEnable()
    {
        // target variable is an object, so it is casted to CustomList component script
        component = (GridOrganizer)target;
        GetTarget = new SerializedObject(component);
    }

    // Design implementation of Inspector GUI is made here (similar to Monobehaviour Update())
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}