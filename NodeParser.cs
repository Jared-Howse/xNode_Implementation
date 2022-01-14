using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using XNode;
using UnityEngine.SceneManagement;
using System;

public class NodeParser : MonoBehaviour
{
    public DialogueGraph graph;
    Coroutine _parser;
    public TMP_Text textLabel;
    public GameObject dialogueBox;

    private GameManager manager;

    private TypewriterEffect typewriterEffect;

    private DialogueObject dialogue;

    public bool isRunning;

    public ResponseHandler handler;

    public bool isChoosing;



    private void Start()
    {
        dialogueBox.SetActive(false);
        manager = GetComponentInParent<GameManager>();
        typewriterEffect = GetComponentInParent<TypewriterEffect>();
        //Loop through nodes until we find the starting node
        FindStart();
    }

    private void FindStart()
    {
        foreach (BaseNode b in graph.nodes)
        {
            if (b.GetString() == "Start")
            {
                //Make this node the staring point
                graph.current = b;
                //Stop finding start node
                break;
            }
        }
    }

    public IEnumerator ParseNode()
    {
        BaseNode b = graph.current;
        string data = b.GetString();
        switch (data)
        {

            case "Start":
                NextNode("exit");
                break;
            case "DialogueNode":
                //Run Dialogue processing
                dialogue = b.GetDialogueObject();
                
                //speaker.text = b.GetSpeakerName();
                yield return StartCoroutine(StepThroughDialogue(dialogue, b.GetClip()));
                NextNode("exit");
                break;
            case "TriggerNode":
                manager.IncreaseInt();
                manager.SetInt();
                NextNode("exit");
                break;
            case "BreakerNode":
                ChangeNode(b.ReturnNode());
                break;
            case "SceneNode":
                SceneManager.LoadScene(b.GetSceneName());
                break;
            case "ChangeGraph":
                graph = b.GetNewGraph();
                FindStart();
                break;
            case "ChoiceNode":
                dialogueBox.SetActive(true);
                Response[] responsesArray = b.GetResponses();
                yield return StartCoroutine(PickResponse(responsesArray));
                break;
            case "ResponseNode":
                NextNode("exit");
                break;                
        }
    }

    public void ChangeNode(BaseNode node)
    {
        graph.current = node;
        StopCoroutine(ParseNode());
    }

    private IEnumerator PickResponse(Response[] responses)
    {
        textLabel.text = "";
        gotResponse = false;
        handler.ShowResponses(responses);
        //yield return new WaitUntil(() => gotResponse);
        yield return null;
    }

    public bool gotResponse;

    public void GotResponse(Response response)
    {
        gotResponse = true;
        SetNext(response);
    }

    public void SetNext(Response response)
    {
        graph.current = response;
        _parser = StartCoroutine(ParseNode());
    }
    public void ShowDialogue(DialogueObject dialogueObject, AudioClip clip)
    {
        StartCoroutine(StepThroughDialogue(dialogueObject, clip));
    }
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject, AudioClip clip)
    {
        dialogueBox.SetActive(true);
        isRunning = true;
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue, clip);

            textLabel.text = dialogue;

            yield return null;
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        isRunning = false;
        CloseDialogueBox();
    }

    private IEnumerator RunTypingEffect(string dialogue, AudioClip clip)
    {
        typewriterEffect.Run(dialogue, textLabel, clip);

        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetMouseButtonDown(0))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
    }

    public void NextNode(string fieldName)
    {
        bool stop = false;
        //find port with this name
        if (_parser != null)
        {
            StopCoroutine(_parser);
            _parser = null;
        }
        foreach(NodePort p in graph.current.Ports)
        {
            try
            {
                //Check if this is the port we want
                if (p.fieldName == fieldName)
                {
                    graph.current = p.Connection.node as BaseNode;
                    break;
                }

            }
            catch (Exception e)
            {
                Debug.Log("No Node Connection");
                stop = true;
            }
        }
        if (!(stop)){ 
            _parser = StartCoroutine(ParseNode()); 
        }
    } 
}
