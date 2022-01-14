using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class TriggerNode : BaseNode
{
    [Input] public int entry;
    [Output] public int exit;


    public override string GetString()
    {
        return "TriggerNode";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
