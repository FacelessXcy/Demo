using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.SceneLoadManager;

public class Seal : MonoBehaviour
{
    private int _state = 0;//0:dirty,1:clean,2:finish
    private GameObject _dirty;
    private GameObject _clean;
    private Collider2D[] _collider2Ds;
    private TextMesh _textMesh;
    
    private void Start()
    {
        _dirty = transform.Find("dirty_seal").gameObject;
        _clean = transform.Find("clean_seal").gameObject;
        _textMesh = transform.Find("TestMeshGO")
            .GetComponent<TextMesh>();
        _collider2Ds = GetComponents<Collider2D>();
        _clean.SetActive(false);
        _textMesh.text = "我被油困住了，请帮我拿3个海藻，\n让我清洁身上的油渍。";
    }

    private void Update()
    {
        switch (_state)
        {
            case 1:
                
                break;
            case 2:
                
                break;
            case 3:
                
                break;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
     
            if (_state==0&&InventorySystem.Instance.GetItemAmount
            (ItemType.HaiZao)==3)
            {
                InventorySystem.Instance.RemoveItemAmount(
                    ItemType.HaiZao,3);
                UIManager.Instance.UpdateAllGamingUI();
                _state = 1;
                _dirty.SetActive(false);
                _clean.SetActive(true);
                _textMesh.text = "谢谢你，但我现在饿得游不动，\n请再帮我拿三条丁香鱼。";
            }
            if (_state==1&&InventorySystem.Instance.GetItemAmount
            (ItemType.DingXiangYu)==3)
            {
                InventorySystem.Instance.RemoveItemAmount(
                    ItemType.DingXiangYu,3);
                UIManager.Instance.UpdateAllGamingUI();
                _state = 2;
                for (int i = 0; i < _collider2Ds.Length; i++)
                {
                    _collider2Ds[i].enabled = false;
                }
                _textMesh.text = "谢谢你!愿你成功!";
                StartCoroutine(State2());
            }
        }
    }

    IEnumerator State2()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
