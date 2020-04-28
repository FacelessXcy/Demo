using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Xcy.SceneLoadManager;

public class LoadingScene : MonoBehaviour
{
    public Slider loadingSlider;
    public Text loadingText;
    public Image loadingImg;
    public GameObject wanJieSaHua;
    
    private AsyncOperation _loadSceneAO;
    private bool _beginAO=false;
    private float _runTime;
    private float _valueTarget;
    void Start()
    {
        loadingSlider.value = 0;
        loadingText.text = 0+ " %";
        loadingImg.sprite = SceneLoadManager.Instance.BackGroundImage;
        Time.timeScale = 1;
        if (SceneLoadManager.Instance.WanJie)
        {
            wanJieSaHua.SetActive(true);
        }
        else
        {
            wanJieSaHua.SetActive(false);
        }

        if (SceneLoadManager.Instance.TargetSceneName=="Game3Scene")
        {
            loadingImg.color=Color.black;
        }
        else
        {
            loadingImg.color=Color.white;
        }
    }

    void Update()
    {
        if (!_beginAO)
        {
            StartCoroutine(AsyncLoading());
            _beginAO = true;
        }

        if (_loadSceneAO!=null)
        {
            _valueTarget = _loadSceneAO.progress;
            if (_valueTarget>=0.9f)
            {
                _valueTarget = 1;
            }

            if (_valueTarget!=loadingSlider.value)
            {
                loadingSlider.value = Mathf.Lerp(loadingSlider.value,_valueTarget,Time.deltaTime);
                if (Mathf.Abs(loadingSlider.value-_valueTarget)<0.01f)
                {
                    loadingSlider.value = _valueTarget;
                }
            }
            loadingText.text = ((int)(loadingSlider.value*100)).ToString() + " %";
            if (loadingSlider.value==1)
            {
                loadingText.text = "加载完成，按G键进入";
                if (Input.GetKeyDown(KeyCode.G))
                {
                    _loadSceneAO.allowSceneActivation = true;
                }
            } 
        }
    }
    IEnumerator AsyncLoading()
    {
        if (SceneLoadManager.Instance.LoadMode==SceneLoadMode.LoadByName)
        {
            _loadSceneAO=SceneManager.LoadSceneAsync(SceneLoadManager.Instance
                .TargetSceneName);
        }
        else
        {
            _loadSceneAO=SceneManager.LoadSceneAsync(SceneLoadManager.Instance
                .TargetSceneIndex);
        }
        _loadSceneAO.allowSceneActivation = false;
        yield return _loadSceneAO;
        Debug.Log("异步完成");
    }

}












//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;
//
//public class LoadingScene : MonoBehaviour
//{
//    public Slider loadingSlider;
//    public Text loadingText;
//    private AsyncOperation loadSceneAO;
//    private float valueTarget;
//
//
//    void Start()
//    {
//        loadingSlider.value = 0;
//        loadingText.text = (0*100).ToString() + " %";
//        Time.timeScale = 1;
//        if (SceneManager.GetActiveScene().name=="Loading")
//        {
//            StartCoroutine(StartLoading_4());
//        }
//
//    }
//
//    private IEnumerator StartLoading_4()
//    {
//        int displayProgress = 0;
//        int toProgress = 0;
//        loadSceneAO = SceneManager.LoadSceneAsync(GlobalObject.Instance.sceneName);
//        loadSceneAO.allowSceneActivation = false;
//        while (loadSceneAO.progress < 0.9f)
//        {
//            while (displayProgress < loadSceneAO.progress* 100.0f)
//            {
//                ++displayProgress;
//                SetSlider(displayProgress/100.0f);
//                yield return new WaitForEndOfFrame();
//            }
//        }
//        toProgress = 100;
//        while (displayProgress < toProgress)
//        {
//            ++displayProgress;
//            SetSlider(displayProgress/100.0f);
//            yield return new WaitForEndOfFrame();
//        }
//    }
//
//    private void SetSlider(float value)
//    {
//        if (value!=loadingSlider.value)
//        {
//            loadingSlider.value = Mathf.Lerp(loadingSlider.value,value,Time.deltaTime);
//            if (Mathf.Abs(loadingSlider.value-value)<0.01f)
//            {
//                loadingSlider.value = value;
//            }
//        }
//        loadingText.text = ((int)(loadingSlider.value*100)).ToString() + " %";
//    }
//
//    void Update()
//    {
//        
//        if (loadingSlider.value==1)
//        {
//            loadingText.text = "加载完成，按G键进入";
//            if (Input.GetKeyDown(KeyCode.G))
//            {
//                loadSceneAO.allowSceneActivation = true;
//            }
//        } 
//        
//    }
//
//}
