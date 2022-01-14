using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BaseNode : Node {

    DialogueObject dialogue;
	public virtual string GetString()
    {
		return null;
    }

    public virtual DialogueObject GetDialogueObject()
    {
        return null;
    }

    public virtual string GetSpeakerName()
    {
        return null;
    }

    public virtual Sprite GetSprite()
    {
        return null;
    }

    public virtual string GetDialogue()
    {
        return null;
    }

    public virtual string GetSceneName()
    {
        return null;
    }

    public virtual AudioClip GetClip()
    {
        return null;
    }

    public virtual Vector2 GetSpeakerLocation(Vector2 position)
    {
        return default;
    }
    public virtual DialogueGraph GetNewGraph()
    {
        return null;
    }

    public virtual Response[] GetResponses()
    {
        return null;
    }

    public virtual Node returnResponseNode(int responseIndex)
    {
        return null;
    }

    public virtual BaseNode ReturnNode()
    {
        return null;
    }

    public virtual int getArrayLength()
    {
        return 0;
    }
}
