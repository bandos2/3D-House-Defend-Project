using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SpawningObjects))]
public class SpawningObjectsEditor : Editor
{
    //string[] _choices = new[] { "Spawn On Object with Script", "Spawn On Different Transform" };
    //int _choiceIndex = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SpawningObjects myScript = (SpawningObjects)target;
        //_choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
       // myScript.choice = _choices[_choiceIndex];
        if (GUILayout.Button("SpawnAllObjects"))
        {
            myScript.SpawnAllObjects();
        }
        if (GUILayout.Button("Spawn Object With id =1"))
        {
            myScript.SpawnObjectWithID1();
        }
        if (GUILayout.Button("Spawn Object With id =2"))
        {
            myScript.SpawnObjectWithID2();
        }

        // Save the changes back to the object
        EditorUtility.SetDirty(target);
    }
}