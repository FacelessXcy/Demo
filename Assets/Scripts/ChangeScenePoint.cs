using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ChangeSceneDelegate : UnityEvent<Vector3>
{ }
public class ChangeScenePoint : MonoBehaviour
{
    public ChangeSceneDelegate beforeChangeScene;
    private GameObject text;
    public string toScene;
    public string nextSceneMusicName;
    public Vector3 nextSceneBornPos;
    private void Start()
    {
        text = transform.Find("Canvas/Text").gameObject;
        //if (beforeChangeScene == null)
        //{
        //   beforeChangeScene = new ChangeSceneDelegate();
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Player IN");
        text.SetActive(true);
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        GlobalObject.Instance.SaveData();
        //SceneManager.LoadScene(toScene);
        GlobalObject.Instance.LoadNewScene(toScene);
        GlobalObject.Instance.SetNextScenePos(nextSceneBornPos);
        AudioManager.Instance.ChangeScenePointPlayBGM(nextSceneMusicName);
        //beforeChangeScene.Invoke(transform.position);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // Debug.Log("Out");
        text.SetActive(false);
    }
}
