using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace devWorkSpace.Otani.Scripts
{
    public class DarknessArea : MonoBehaviour
    {
        //プレイヤーの周辺だけ明るくするLight
        [SerializeField] GameObject playerLight;
    
        //DirectionalLight
        [SerializeField] GameObject dLight;
    
        //WaveLight.csで使用
        [SerializeField] public Boolean inDark;
        
        //MainCamera
        private GameObject _mainCam;
        

        private void Start()
        {
            inDark = false;
            _mainCam = GameObject.Find("Main Camera");
        }

        private void Update()
        {
            var pos = new List<Vector3>();

            pos.Add(new Vector3(263f, 33, -18f));
            pos.Add(new Vector3(338f, 19, -18f));
            
            var mainCamPos = _mainCam.transform.position;

            for (int i = 0; i < 2; i++)
            {
                if (mainCamPos != pos[i])
                {
                    inDark = false;
                }
                else
                {
                    inDark = true;
                    break;
                }
            }
            
            //プレイヤーが真っ暗エリア外の時
            if(!inDark)
            {
                //プレイヤーについているライトの無効化
                playerLight.SetActive(false);
            
                //環境光の有効化
                dLight.SetActive(true);
                return;
            }else
                //プレイヤーが真っ暗エリアに入った時
            {
                //プレイヤーについているライトの有効化
                playerLight.SetActive(true);
            
                //環境光の無効化
                dLight.SetActive(false);
            }
            
        }
    }
}
