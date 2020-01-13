using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public Slider loadingSlider;
    public Text loadingText;
    private AsyncOperation loadSceneAO;
    private float valueTarget;


    void Start()
    {
        loadingSlider.value = 0;
        loadingText.text = (0*100).ToString() + " %";
        if (SceneManager.GetActiveScene().name=="Loading")
        {
            StartCoroutine(AsyncLoading());
        }
        Time.timeScale = 1;
    }

    void Update()
    {
        valueTarget = loadSceneAO.progress;
        //Debug.Log(valueTarget);
        if (valueTarget>=0.9f)
        {
            valueTarget = 1;
        }

        if (valueTarget!=loadingSlider.value)
        {
            loadingSlider.value = Mathf.Lerp(loadingSlider.value,valueTarget,Time.deltaTime);
            if (Mathf.Abs(loadingSlider.value-valueTarget)<0.01f)
            {
                loadingSlider.value = valueTarget;
            }
        }
        loadingText.text = ((int)(loadingSlider.value*100)).ToString() + " %";
        if (loadingSlider.value==1)
        {
            loadingText.text = "加载完成，按G键进入";
            if (Input.GetKeyDown(KeyCode.G))
            {
                loadSceneAO.allowSceneActivation = true;

            }
        } 
        
    }
    IEnumerator AsyncLoading()
    {
        loadSceneAO = SceneManager.LoadSceneAsync(GlobalObject.Instance.sceneName);
        loadSceneAO.allowSceneActivation = false;
        yield return loadSceneAO;
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
