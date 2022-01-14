using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Activator : MonoBehaviour
{
    private NodeParser parser;
    private bool interactable;
    public GameObject objectNotifier;
    public TMP_Text text;

    public bool isRunning => parser.dialogueBox.activeInHierarchy;
    public bool isChoosing;
  

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       
        anim = GetComponent<Animator>();
        objectNotifier.SetActive(false);
        parser = GetComponent<NodeParser>();
    }

    public bool GetInteractionStatus()
    {
        return interactable;
    }

    public string GetObjectText()
    {
        return this.name;
    }
     
    private void startRoutine()
    {
        parser.StartCoroutine(parser.ParseNode());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMoveStripped player))
        {
            anim.SetBool("Interactive", true);
            anim.Play("OpenInteractionText");
            objectNotifier.SetActive(true);
            text.text = GetObjectText();
            interactable = true;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactable && !isRunning)
        {
            objectNotifier.SetActive(false);
            startRoutine();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        interactable = false;
        objectNotifier.SetActive(false);
        anim.SetBool("Interactive", false);
        parser.CloseDialogueBox();
    }
}
