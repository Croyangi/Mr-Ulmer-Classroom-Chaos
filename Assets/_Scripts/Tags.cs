using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    [SerializeField] public ScriptObj_Tag[] tags;

    public bool CheckTags(string name)
    {
        foreach (ScriptObj_Tag tag in tags)
        {
            if (tag.name == name)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckGameObjectTags(GameObject gameObj, string tag)
    {
        Tags _tags;
        if (gameObj.GetComponent<Tags>() != null)
        {
            _tags = gameObj.GetComponent<Tags>();
            if (_tags.CheckTags(tag) == true)
            {
                return true;
            }
        }
        return false;
    }
}
