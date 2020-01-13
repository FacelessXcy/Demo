using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest
{
    public struct Scene1
    {
        public string sceneName;
        public Vector3 playerPos;
        public int currentHp;
        public int diamondCount;
        public List<bool> diamondsPosActive;
        public List<bool> damageableWallsActive;
        //保存存档时所播放的音乐
        public string musicName;
    }

    public struct Scene2
    {
        public string sceneName;
        public Vector3 playerPos;
        public int currentHp;
        public int diamondCount;
        public List<bool> diamondsPosActive;
        //保存存档时所播放的音乐
        public string musicName;
    }
    public Scene1 s1;
     public Scene2 s2;

   




}
