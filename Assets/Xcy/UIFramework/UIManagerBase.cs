﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;

namespace Xcy.UIFramework
{
    public abstract class UIManagerBase : MonoSingleton<UIManager>
    { 
        public UILoadPath[] uiLoadPath;
        public UIType awakeLoadUI;
        private Canvas _canvas;
        //保存UIPrefab加载路径
        private Dictionary<UIType, string> _panelPathDic=new Dictionary<UIType, string>();
        //保存实例
        private Dictionary<UIType,BasePanel> _panelDic=new Dictionary<UIType, BasePanel>();
    
        private Stack<BasePanel> _panelStack=new Stack<BasePanel>();
        public override void Awake()
        {
            _destoryOnLoad = true;
            _canvas = GetComponent<Canvas>();
            LoadUIPanelInfo();
            base.Awake();
        }
    
        private void Start()
        {
            PushPanel(awakeLoadUI);
        }
    
        //页面入栈，位于栈顶，显示
        public void PushPanel(UIType uiType)
        {
            if (_panelStack==null)
            {
                _panelStack=new Stack<BasePanel>();
            }
    
            if (_panelStack.Count>0)
            {
                _panelStack.Peek().OnPaused();
            }
            
            BasePanel panel = GetPanel(uiType);
            panel.OnEnter();
            _panelStack.Push(panel);
        }
        //页面出栈，隐藏
        public void PopPanel()
        {
            if (_panelStack==null)
            {
                _panelStack=new Stack<BasePanel>();
            }
    
            if (_panelStack.Count<=0)
            {
                return;
            }
            _panelStack.Pop().OnExit();
    
            if (_panelStack.Count<=0)
            {
                return;
            }
            _panelStack.Peek().OnResume();
        }
    
    
        protected BasePanel GetPanel(UIType uiType)
        {
            if (_panelDic==null)
            {
                _panelDic=new Dictionary<UIType, BasePanel>(); 
            }
    
            if (!_panelDic.ContainsKey(uiType))
            {
                foreach (KeyValuePair<UIType,string> pair in _panelPathDic)
                {
                    if (pair.Key==uiType)
                    {
                        GameObject tempPanel = Instantiate(Resources.Load<GameObject>(pair.Value));
                        tempPanel.transform.SetParent(_canvas.transform,false);
                        _panelDic.Add(pair.Key,tempPanel.GetComponent<BasePanel>());
                    }
                }
            }
    
            return _panelDic[uiType];
        }
    
        private void LoadUIPanelInfo()
        {
            foreach (UILoadPath path in uiLoadPath)
            {
                Debug.Log("类型："+path.uiType+"  UI路径："+path.uiPath);
                _panelPathDic.Add(path.uiType,path.uiPath);
            }
        }

        public abstract void Create();

    }
}