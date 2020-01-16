using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("MyActions")]
public class StartIdle : Action
{
    
    private EnemyStateController _enemyStateController;
    
    public override void OnStart()
    {
        _enemyStateController=Owner.transform.GetComponent<EnemyStateController>();
    }


    public override TaskStatus OnUpdate()
    {
        _enemyStateController.StartIdle();
        return TaskStatus.Success;
    }
}
