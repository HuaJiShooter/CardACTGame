using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

public class LogMessageTest : Simulation.Event<LogMessageTest>
{
    // Start is called before the first frame update
    public override void Execute()
    {
        Debug.Log("成功执行LogMessageTest文件");
    }
}
