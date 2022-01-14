using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ChoiceNode : BaseNode
{
    [Input] public int entry;

    public Response[] responses;

    public override Response[] GetResponses()
    {
        return responses;
    }

    public override string GetString()
    {
        return "ChoiceNode";
    }

    public override Node returnResponseNode(int responseIndex)
    {
        return responses[responseIndex];
    }

    public override int getArrayLength()
    {
        return responses.Length;
    }
}
