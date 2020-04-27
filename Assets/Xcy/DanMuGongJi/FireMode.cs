using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;
using Xcy.Utility;

namespace Xcy.DanMu
{
    public class FireMode : MonoBehaviour
    {
        public float bulletSpeed;
        private SimpleObjectPool<MonsterBulletCharacter> _bulletPool;
        [FormerlySerializedAs("bulletTemplate")] public MonsterBulletCharacter monsterBulletTemplate;
        [FormerlySerializedAs("firPoint")] public Transform firePoint;
        //public List<MonsterBulletCharacter> tempBullets;
        void Start()
        {
            //tempBullets = new List<MonsterBulletCharacter>();
            _bulletPool=new SimpleObjectPool<MonsterBulletCharacter>
            (FactoryMethod,ResetMethod,50);
        }

//        private void Update()
//        {
//            if (UnityEngine.Input.GetKeyDown(KeyCode.A))
//            {
//                FireShotGun();
//            }
//            if (UnityEngine.Input.GetKeyDown(KeyCode.S))
//            {
//                FireRound();
//            }
//            if (UnityEngine.Input.GetKeyDown(KeyCode.D))
//            {
//                FirRoundGroup();
//            }
//            if (UnityEngine.Input.GetKeyDown(KeyCode.F))
//            {
//                FireTurbine();
//            }
//        }

        public void FireShotGun()
        {
            
            StartCoroutine(FirShotgunIEnu());
        }

        public void FireRound()
        {
            StartCoroutine(FireRoundIEnu(3,firePoint.position));
        }

        public void FirRoundGroup()
        {
            StartCoroutine(FirRoundGroupIEnu());
        }

        public void FireTurbine()
        {
            StartCoroutine(FireTurbineIEnu());
        }

        /// <summary>
        /// 发射散弹
        /// </summary>
        /// <returns></returns>
        IEnumerator FirShotgunIEnu()
        {
            Vector3 bulletDir = -firePoint.transform.right;
            Quaternion leftRota = Quaternion.AngleAxis(-30, Vector3.forward);
            Quaternion RightRota = Quaternion.AngleAxis(30, Vector3.forward); //使用四元数制造2个旋转，分别是绕Z轴朝左右旋转30度
            for (int i=0;i<10;i++)     //散弹发射次数
            {
                for (int j=0;j<3;j++) //一次发射3颗子弹
                {
                    switch (j)
                    {
                        case 0:
                            AllocateBullet(bulletDir, firePoint.transform.position);  //发射第一颗子弹，方向不需要进行旋转
                            break;
                        case 1:
                            bulletDir = RightRota * bulletDir;//第一个方向子弹发射完毕，旋转方向到下一个发射方向
                            AllocateBullet(bulletDir, firePoint.transform.position);
                            break;
                        case 2:
                            bulletDir = leftRota*(leftRota * bulletDir); //右边方向发射完毕，得向左边旋转2次相同的角度才能到达下一个发射方向
                            AllocateBullet(bulletDir, firePoint.transform.position);
                            bulletDir = RightRota * bulletDir; //一轮发射完毕，重新向右边旋转回去，方便下一波使用
                            break;
                    }
                }
                yield return new WaitForSeconds(0.5f); //协程延时，0.5秒进行下一波发射
            }
        }
    
        /// <summary>
        /// 发射圆形弹幕
        /// </summary>
        /// <returns></returns>
        IEnumerator FireRoundIEnu(int number,Vector3 creatPoint)
        {
            Vector3 bulletDir = -firePoint.transform.right;
            Quaternion rotateQuate = Quaternion.AngleAxis(60, Vector3.forward);//使用四元数制造绕Z轴旋转10度的旋转
            for (int i=0;i< number; i++)    //发射波数
            {
                for (int j=0;j<6;j++)
                {
                    AllocateBullet(bulletDir, creatPoint);
                    bulletDir = rotateQuate * bulletDir; //让发射方向旋转10度，到达下一个发射方向
                }
                yield return new WaitForSeconds(0.5f); //协程延时，0.5秒进行下一波发射
            }
            yield return null;
        }
    
        /// <summary>
        /// 发射组合圆形弹幕
        /// </summary>
        /// <returns></returns>
        IEnumerator FirRoundGroupIEnu()
        {
            Vector3 bulletDir = -firePoint.transform.right;
            Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);//使用四元数制造绕Z轴旋转45度的旋转
            List<MonsterBulletCharacter> bullets = new List<MonsterBulletCharacter>();       //装入开始生成的8个弹幕
            for (int i=0;i<8;i++)
            {
                var tempBullet = AllocateBullet(bulletDir, firePoint.transform.position);
                bulletDir = rotateQuate * bulletDir; //生成新的子弹后，让发射方向旋转45度，到达下一个发射方向
                bullets.Add(tempBullet); 
            }
            yield return new WaitForSeconds(1.0f);   //1秒后在生成多波弹幕
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].speed = 0; //弹幕停止移动
                RecycleBullet(bullets[i]);
                StartCoroutine(FireRoundIEnu(3, bullets[i].transform.position));//通过之前弹幕的位置，生成多波多方向的圆形弹幕
            }
        }
    
        /// <summary>
        /// 发射涡轮型弹幕
        /// </summary>
        /// <returns></returns>
        IEnumerator FireTurbineIEnu()
        {
            Vector3 bulletDir = -firePoint.transform.right;      //发射方向
            Quaternion rotateQuate = Quaternion.AngleAxis(20, Vector3.forward);//使用四元数制造绕Z轴旋转20度的旋转
            float radius = 0.6f;        //生成半径
            float distance = 0.2f;      //每生成一次增加的距离
            for (int i=0;i<18;i++)
            {
                Vector3 firePoint = this.firePoint.transform.position + bulletDir * radius;   //使用向量计算生成位置
                StartCoroutine(FireRoundIEnu(1, firePoint));     //在算好的位置生成一波圆形弹幕
                yield return new WaitForSeconds(0.05f);      //延时较小的时间（为了表现效果），计算下一步
                bulletDir = rotateQuate * bulletDir;        //发射方向改变
                radius += distance;     //生成半径增加
            }
        }
        
//        public MonsterBulletCharacter CreatBullet(Vector3 dir,Vector3 creatPoint)
//        {
//            MonsterBulletCharacter monsterBulletCharacter = Instantiate(monsterBulletTemplate, creatPoint, Quaternion.identity);
//            monsterBulletCharacter.gameObject.SetActive(true);
//            monsterBulletCharacter.dir = dir;
//            tempBullets.Add(monsterBulletCharacter);
//            return monsterBulletCharacter;
//        }

        public MonsterBulletCharacter AllocateBullet(Vector3 dir,Vector3 creatPoint)
        {
            MonsterBulletCharacter temp= _bulletPool.Allocate();
            temp.transform.position = creatPoint;
            temp.gameObject.SetActive(true);
            temp.enabled = true;
            temp.isMove = true;
            temp.speed=bulletSpeed;
            temp.FireMode = this;
            temp.dir = dir;
            return temp;
        }

        public MonsterBulletCharacter FactoryMethod()
        {
            MonsterBulletCharacter monsterBulletCharacter = Instantiate
            (monsterBulletTemplate, firePoint.position, Quaternion.identity);
            monsterBulletCharacter.isMove = false;
            monsterBulletCharacter.enabled = false;
            monsterBulletCharacter.gameObject.SetActive(false);
            return monsterBulletCharacter;
        }

        public void RecycleBullet(MonsterBulletCharacter monsterBulletCharacter)
        {
            _bulletPool.Recycle(monsterBulletCharacter);
        }
        
        public void ResetMethod(MonsterBulletCharacter monsterBulletCharacter)
        {
            monsterBulletCharacter.isMove = false;
            monsterBulletCharacter.enabled = false;
            monsterBulletCharacter.gameObject.SetActive(false);
        }
        
    }
}