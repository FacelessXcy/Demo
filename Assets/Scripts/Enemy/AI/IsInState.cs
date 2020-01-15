using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;



[TaskCategory("MyConditionals")]
public class IsInState : Conditional
{
    
    public EnemyState enemyState;

    private EnemyStateController _enemyStateController;
    
    public override void OnStart()
    {
        _enemyStateController =
            Owner.transform.GetComponent<EnemyStateController>();
    }
    
    public override TaskStatus OnUpdate()
    {
        //Debug.Log(_enemyStateController.enemyState);
        if (_enemyStateController.enemyState==enemyState)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
