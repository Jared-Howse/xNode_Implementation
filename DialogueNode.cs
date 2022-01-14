using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class DialogueNode : BaseNode {

	//Entry and exit of each node
	[Input] public int entry;
	[Output] public int exit;

    public DialogueObject dialogue;

    //Variables: Name, text, portrait, sound
    public string speakerName;
	//public Sprite sprite;
    public AudioClip textSound;

    public GameObject positioningObject;

    public GameObject dialogueBox;
    //Accessing: overriding GetString in class BaseNode
    public override string GetString()
    {
        return "DialogueNode";
    }

    public override DialogueObject GetDialogueObject()
    {
        return dialogue;
    }

    public override AudioClip GetClip()
    {
        return textSound;
    }

    public void OnValidate()
    {
        name = dialogue.name;
    }

    //Accessing: overriding GetSprite in class BaseNode
    //public override Sprite GetSprite()
    //{
    //return sprite;
    //}

    public override string GetSpeakerName() 
    {
        return speakerName;
    }

    public override Vector2 GetSpeakerLocation(Vector2 position)
    {
        //var screenToWorldPosition = Camera.main.ScreenToWorldPoint(positioningObject.transform.position);

        return position;
    }
}