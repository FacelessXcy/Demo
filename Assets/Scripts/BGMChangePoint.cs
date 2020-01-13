using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChangePoint : MonoBehaviour
{
    [Tooltip("是否为场景切换点")]
    public bool isChangeScenePoint = false;
    [Tooltip("如果是场景切换点,播放音乐的名称")]
    public string musicName;
    [Tooltip("如果不是场景切换点,从左向右通过时,音乐切换为...")]
    public string leftToRight;
    [Tooltip("如果不是场景切换点,从右向左通过时,音乐切换为...")]
    public string rightToLeft;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isChangeScenePoint)
            AudioManager.Instance.ChangeScenePointPlayBGM(musicName);
                //StartCoroutine(ChangeScenePointPlayBGM(musicName));
            //if (!GameManager.Instance.GetMusicPlayState())
            //{
            //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isChangeScenePoint)
        {
            
            Vector2 offset = collision.transform.position - transform.position;
            AudioManager.Instance.NormalPointPlayBGM(rightToLeft,leftToRight,offset);
        //StartCoroutine(NormalPointPlayBGM(rightToLeft,leftToRight, temp));
        }
        
    }

    

}
