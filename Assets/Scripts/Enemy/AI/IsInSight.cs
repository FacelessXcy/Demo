using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("MyConditionals")]
public class IsInSight : Conditional
{
    public SharedTransform PlayerTransform;

    public SharedFloat ViewAngel;
    public SharedFloat ViewDistance;

    public LayerMask IgnoreLayerMask;
    private EnemyStateController _enemyStateController;

    public override void OnStart()
    {
        _enemyStateController = Owner.transform
            .GetComponent<EnemyStateController>();
        
    }

    public override TaskStatus OnUpdate()
    {
        
        if (Vector2.Distance(PlayerTransform.Value.position,
        _enemyStateController.EyesPosition.position)<=ViewDistance.Value)
        {
            if (Vector2.Angle(_enemyStateController.CurrentForward,
                PlayerTransform.Value.position-
                _enemyStateController.EyesPosition.position)
                <=ViewAngel.Value)
            {
                RaycastHit2D hit2D = Physics2D.Raycast(
                    _enemyStateController.EyesPosition.position,
                    PlayerTransform.Value.position -
                    _enemyStateController.EyesPosition.position,
                    ViewDistance.Value, ~IgnoreLayerMask);
                //Debug.Log("hit2D.collider==null  "+(hit2D.collider==null));
                if (hit2D!=null&&hit2D.collider!=null&&hit2D.collider.CompareTag("Player"))
                {
                    return TaskStatus.Success;
                }
            }
        }

        return TaskStatus.Failure;
    }
}
