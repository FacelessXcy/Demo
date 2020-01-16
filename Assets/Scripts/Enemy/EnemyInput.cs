using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    [Header("===== Output Signals=====")]
    public float horizontal;
    public bool jump;
    public bool attack;

    private float _moveTime=2.5f;
    
    void Start()
    {
        horizontal = 0;
    }

    
    void Update()
    {
        MoveToPlayer();
    }

    public void MoveInput()
    {
        horizontal = (Random.Range(0, 2) == 0) ? -1 : 1;
    }

    public void MoveToPlayer()
    {
        
    }


}
