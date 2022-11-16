using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using devWorkSpace.Yoshiba.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using devWorkSpace.SoundTeam.Scripts;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class Chladni : MonoBehaviour
    {
        /*
            Ellipse  =1<<0,
            Dia      =1<<1,
            Square   =1<<2,
            Hex      =1<<3,
            Tri      =1<<4,
            InvTri   =1<<5,
            Circle   =1<<6
         */
        [System.Flags]
        private enum ShapesFlag :int
        {
            Ellipse  =1<<1,
            Dia      =1<<2,
            Square   =1<<3,
            Hex      =1<<4,
            Tri      =1<<5,
            InvTri   =1<<6,
            Circle   =1<<7
        }

        [SerializeField] private GameObject door;
        [SerializeField] private ShapesFlag ansFlag;
        [SerializeField,HideInInspector] private List<ChladniMask> ansShapes;
        SE se;

        private ShapesFlag _nowFlag=0;
         private readonly float[] _someLife=new float[7];

         private Action _changeColor;

         private readonly List<GameObject> _chladniObj = new List<GameObject>();
         private Material _mat;
         private List<ChladniMask> _allMask;
         private List<ChladniMask> _hintMasks;
         private List<float> _fades;
         
         private static readonly int cAlphaT = Shader.PropertyToID("_AlphaT");

         // Start is called before the first frame update
        void Start()
        {
            _mat = GetComponent<MeshRenderer>().material;
            _fades = new List<float>();
            for (var i = 0; i < 8; i++)
            {
                _fades.Add(1f);
            }
            _mat.SetFloatArray(cAlphaT,_fades);
            //マスク画像を取得する
            _allMask = new List<ChladniMask>();
            var op = Addressables.LoadAssetsAsync<ChladniMask>("ChladniData",null);
            op.WaitForCompletion();
            var result = op.Result;
            foreach (var msk in result)
            {
                _allMask.Add(msk);
            }
            _allMask.Sort((a,b)=>a.Num-b.Num);

            var fragCnt = 0;
            _hintMasks = new List<ChladniMask>();
            //フラグを持ってるか調べる
            foreach (ShapesFlag key in Enum.GetValues(typeof(ShapesFlag)))
            {
                if (fragCnt > 3)
                {
                    Debug.LogError("chladni:3つ以上の図形を指定しています");
                }
                if (ansFlag.HasFlag(key))
                {   //フラグを持っている
                    fragCnt++;
                    //何番目のフラグか
                    var shift = (int)Mathf.Log((int)key, 2);
                    _hintMasks.Add(_allMask[shift]);
                }
            }

            //ヒントに指定されたテクスチャをはっつける
            for (var i = 0; i < _hintMasks.Count; i++)
            {
                _mat.SetTexture("_HintMask"+i,_hintMasks[i].Tex);
            }

            _changeColor = changeTex;

            
            var sfx = GameObject.Find("SE");
            se = sfx.GetComponent<SE>();
            
        }

       
        private void Update()
        {
            updateLife();
            changeTex();
        }

        private bool _clear=false;
        void updateLife()
        {
            foreach (var msk in _allMask)
            {
                var i = msk.Num;
                switch (msk.isActive)
                {
                    case true when !_clear:
                        msk.life -= Time.deltaTime;
                        break;
                    case true:
                        msk.life = 3f;
                        break;
                }

                if (msk.life < 0)
                {
                    msk.isActive = false;
                    //フラグを折る
                    _nowFlag &= (ShapesFlag)~(1 << msk.Num);
                }

                _fades[i] = msk.Alpha;
            }
        }
        
        void changeTex()
        {
            for (int i = 0; i < _allMask.Count; i++)
            {
                var msk = _allMask[i];
                _mat.SetTexture("_Mask" + i, msk.isActive ? msk.Tex : Texture2D.blackTexture);
            }
            _mat.SetFloatArray(cAlphaT,_fades);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("SoundWave")||_clear) 
                return;

            var num = 0;
            if (other.gameObject.GetComponent<SoundWaveBehaviour>())
            {
                var wave = other.gameObject.GetComponent<SoundWaveBehaviour>();
                num = wave.SwPitch.Num;
            }


            //waveのswPitchでフラグを立てる
            _nowFlag |= (ShapesFlag)(1<<num);
            _allMask[num].isActive = true;
            _allMask[num].life = 3f;

            se.play(SENameList.Chladni);

            //正解の音が含まれてた時
            se.play(ansFlag.HasFlag((ShapesFlag)(1 << num)) ? SENameList.Switch : SENameList.Gimmick_Failure);
            
            //指定された図形と表示した図形が一致した場合
            if ((int)ansFlag==(int)_nowFlag)
            {
                door.SetActive(!door.activeInHierarchy);
                _clear = true;

                se.play(SENameList.Gimmick_Clear);
            }
            Destroy(other.gameObject);

        }
    }
}
