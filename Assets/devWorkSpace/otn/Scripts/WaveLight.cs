using System;
using UnityEngine;

namespace devWorkSpace.Otani.Scripts
{
    public class WaveLight : MonoBehaviour
    {
        //DarknessAreaオブジェクトの格納場所
        private GameObject _dArea;

        //オブジェクトについているDarknessAreaスクリプト
        private DarknessArea _dScript;
        
        //生成するLight
        [SerializeField] private GameObject wLight;

        private void Start()
        {
            if (!GameObject.Find("DarknessArea"))
                return;
            _dArea = GameObject.Find("DarknessArea");
            _dScript = _dArea.GetComponent<DarknessArea>();
        }

        private void OnCollisionEnter(Collision other)
        {
            //dScriptがいて真っ暗エリアにいるなら
            if(_dScript is null||!_dScript.inDark)
                return;
            
            //ライト生成
            var pos = transform.position;
            Instantiate(wLight, new Vector3(pos.x, pos.y, -50f), Quaternion.identity);
        }
    }
}
