using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Xcy.Common;


public class CheckPoint : MonoSingleton<CheckPoint>
{
    private Vector3 _lastCheckPoint;
    private Vector3 _lastChangeScenePoint;
    //private Transform playerTransform;

    //private Vector3 
   // public int sceneIndex;

   public override void Awake()
   {
       _destoryOnLoad = true;
       base.Awake();
       _lastCheckPoint = GlobalObject.Instance.GetNextScenePos();
       //playerTransform = GameObject.Find("Player").transform;
   }

   public void UpdateCheckPoint(Vector3 checkPoint)
    {
        _lastCheckPoint =checkPoint;
    }
    public void UpdateChangeScenePoint(Vector3 changeScenePos)
    {
        _lastCheckPoint = changeScenePos;
    }

    public Vector3 GetLastCheckPoint()
    {
        return _lastCheckPoint;
    }



}
