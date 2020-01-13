using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public string toScene;
    public Vector3 nextScenePos;
    public void OnClick()
    {
        Debug.Log("点击开始键，切换场景");
        GlobalObject.Instance.SetNextScenePos(nextScenePos);
        GlobalObject.Instance.LoadNewScene(toScene);
        GlobalObject.Instance.SetNextScenePos(new Vector3(-177.4f,-218.6f,0));
        AudioManager.Instance.csol = AudioManager.ChangeSceneOrLoad.ChangeScene;
    }
}
