using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Xcy.Common;


public class CheckPointSave : MonoSingleton<CheckPointSave>
{
    private Vector3 _lastCheckPoint;
    private Vector3 _lastChangeScenePoint;

    public override void Awake()
   {
       _destoryOnLoad = true;
       base.Awake();
   }

   public void UpdateCheckPoint(Vector3 checkPoint)
    {
        _lastCheckPoint =checkPoint;
    }

   public Vector3 GetLastCheckPoint()
    {
        return _lastCheckPoint;
    }



}
