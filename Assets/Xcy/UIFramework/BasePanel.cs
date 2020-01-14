using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xcy.UIFramework
{
    public class BasePanel : MonoBehaviour
    {

        protected CanvasGroup canvasGroup;


        public virtual void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void OnEnter()
        {
            if (canvasGroup==null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        public virtual void OnPaused()
        {
        
        }

        public virtual void OnResume()
        {
        
        }
    
        public virtual void OnExit()
        {
        
        }
        
        public void PanelShow(CanvasGroup cg)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        public void PanelHide(CanvasGroup cg)
        {
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
        
    }
}