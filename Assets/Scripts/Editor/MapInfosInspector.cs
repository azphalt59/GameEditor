using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(MapInfos))]
public class MapInfosInspector : Editor
{
    MapInfos mapInfos;
    // Start is called before the first frame update
    public void Awake()
    {
        mapInfos = GameObject.Find("GameManager").GetComponent<MapInfos>();

    }
    public override void OnInspectorGUI()
    {
        //Called whenever the inspector is drawn for this object.
        DrawDefaultInspector();
        if (GUILayout.Button("Save Map"))
        {
            mapInfos.SaveData();
        }
        if (GUILayout.Button("Load Map"))
        {
            mapInfos.InstantiateSavedMap();
        }
    }
}
