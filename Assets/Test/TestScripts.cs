using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour
{
    public int a;
    private int b;
    private Test2 _test2;
    private void Awake()
    {
        Debug.Log("Awake");
        _test2 = GetComponent<Test2>();
        Debug.Log("_test2==null"+(_test2==null));
        a = 1;
        b = 2;
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        Debug.Log("a="+a);
        Debug.Log("b="+b);
        Debug.Log("_test2==null"+(_test2==null));
    }

    void Start()
    {
        Debug.Log("Start");
        
    }

    
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        Debug.Log("a="+a);
        Debug.Log("b="+b);
    }
}
