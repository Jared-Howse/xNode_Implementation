using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
// This class represents a start node. Inherit base node.
public class StartNode : BaseNode {

	[Output] public int exit;
    [Input] public int entry;

    public override string GetString()
    {
        return "Start";
    }
}