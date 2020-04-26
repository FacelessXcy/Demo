using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Xcy.Common;


public class CheckPointSave : MonoSingleton<CheckPointSave>
{
    public Vector3 lastCheckPoint;

    public override void Awake()
   {
       _destoryOnLoad = true;
       base.Awake();
   }

   public void UpdateCheckPoint(Vector3 checkPoint)
    {
        lastCheckPoint =checkPoint;
    }

   public Vector3 GetLastCheckPoint()
    {
        return lastCheckPoint;
    }



}
