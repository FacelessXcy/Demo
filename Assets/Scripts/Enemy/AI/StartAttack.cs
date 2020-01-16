using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("MyActions")]
public class StartAttack : Action
{
    
    private EnemyStateController _enemyStateController;
    
    public override void OnStart()
    {
        _enemyStateController=Owner.transform.GetComponent<EnemyStateController>();
    }


    public override TaskStatus OnUpdate()
    {
        _enemyStateController.StartAttack();
        return TaskStatus.Success;
    }
}
