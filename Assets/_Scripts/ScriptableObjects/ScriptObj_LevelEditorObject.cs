using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Name", menuName = "Cro's Scriptable Objs/New Level Editor Toolbox Object")]
public class ScriptObj_LevelEditorObject : ScriptableObject
{
    public GameObject levelEditorObject;
    public Vector3 objectOffset = new Vector3(0f, 0f, 0f);
    public Vector2 levelEditorObjectSize = new Vector2(1f, 1f);
    public string levelEditorObjectName;

    public List<string> tags;

    private void OnValidate()
    {
        if (levelEditorObject.GetComponent<Renderer>() != null)
        {
            levelEditorObjectSize = levelEditorObject.GetComponent<Renderer>().bounds.size;
        }
    }
}
