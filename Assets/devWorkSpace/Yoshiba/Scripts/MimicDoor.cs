using System;
using System.Collections;
using System.Collections.Generic;
using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace devWorkSpace.Yoshiba.Scripts
{
    [Serializable]
    public class MimicDoor : MonoBehaviour
    {
        [SerializeField, HideInInspector,Range(2,3)] private  uint sizeOfRings=2;
        [SerializeField, HideInInspector] private List<PitchData> pitches=new List<PitchData>();
        private const int _kMAX_NUM_OF_RINGS=3;
        public int MAXNumOfRings => _kMAX_NUM_OF_RINGS;
        private List<Ring> _rings;
        private int _nowFlag = 0;
        private int _ansFlag = 0;
        private BGM _bgm;
        private SE _se;

        public Action<int> actSetPitchParam= x=>{};

        //リングを生成するために必要
        public void genRings()
        {
            _rings = new List<Ring>(new Ring[_kMAX_NUM_OF_RINGS]);
            //3個リングを生成
            for (var i = 0; i < _kMAX_NUM_OF_RINGS; i++)
            {
                var origin = transform.Find("MimicSpeaker"+i).gameObject;
                _rings[i] = new Ring(origin, pitches[i]);
                _rings[i].speaker.SetActive(false);
            }
        }

        //設定するピッチの数に合わせてリングを表示する
        public void setRingsIsActive()
        {
            for (var i = 0; i < _kMAX_NUM_OF_RINGS; i++)
            {
                _rings[i].speaker.SetActive(i < sizeOfRings);
            }
        }

        public void moveRings()
        {
            //ここの操作はMimicSpeakerに行いたい
            switch (sizeOfRings)
            {
                case 2:
                    _rings[0].speaker.transform.localPosition = new Vector3(0f, 1.25f, 0f);
                    _rings[1].speaker.transform.localPosition = new Vector3(0f, -1.25f, 0f);
                    break;
                case 3:
                    _rings[0].speaker.transform.localPosition = new Vector3(0f, 1.75f, 0f);
                    _rings[1].speaker.transform.localPosition = new Vector3(0f, 0f, 0f);
                    _rings[2].speaker.transform.localPosition = new Vector3(0f, -1.75f, 0f);
                    break;
            }
        }

        public void changeRingEdgeColor()
        {
            for (var i=0;i < sizeOfRings;i++)
            {
                _rings[i].changeHintColor();
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
            _bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<BGMManager>().BGM;
            _se = GameObject.FindGameObjectWithTag("SE").GetComponent<SE>();
            
            //numが2の場合2^(2)-1=3となり、3は2進数で11なのでスピーカー2つ分の判定用フラグが出来る
            //同様にnumが3の場合2^(3)-1=7=0d111なのでスピーカー3つ分の判定用フラグが出来る
            _ansFlag = (int)Mathf.Pow(2, sizeOfRings)-1;
            
            genRings();
            setRingsIsActive();
            moveRings();
            
        }
        
        private void Update()
        {
            if (_ansFlag == _nowFlag)
            {
                StartCoroutine(
                changeDoor(0.5f, () =>
                {
                    var door = transform.Find("Door").gameObject;
                    var mat = door.GetComponent<MeshRenderer>().material;
                    mat.EnableKeyword("_EMISSION");
                }));
                StartCoroutine(
                    changeDoor(1f, () =>
                    {
                        this.gameObject.SetActive(false);
                    }));
            }
        }

        private IEnumerator changeDoor(float t,UnityAction act)
        {
            yield return new WaitForSeconds(t);
            //?.->actがnullじゃない時
            act?.Invoke();
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherGameObject = other.gameObject;
            if (!otherGameObject.CompareTag("SoundWave")) 
                return;
            var wave = otherGameObject.GetComponent<SoundWaveBehaviour>();
            for (var i = 0; i < sizeOfRings; i++)
            {
                var ring = _rings[i];
                if (ring.pitch.Num == wave.SwPitch.Num)
                {
                    ring.changeColor();
                    ring.IsHit = true;
                    actSetPitchParam(ring.pitch.Num);
                    
                    int x = Convert.ToInt32(ring.IsHit);
                    
                    //何番目のスピーカーかによってシフトする桁を変えます
                    //例えば2番目のスピーカーの場合、i=1なので1bit左にシフトします
                    //before:001 after:010
                    //これを現在のヒットフラグにOR演算します
                    //例えば今の_hitFlag=101、正しい音が当てられたのが2番目のスピーカーの場合
                    //x=001,x<<1=010これを101に足して111になります
                    _nowFlag |= x << i;
                    Destroy(otherGameObject);
                }
            }
            
            //WARNING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //ToDo:Fix to flexible
            //正解の音じゃなければ失敗音を鳴らす
            for(var i = 0; i < sizeOfRings; i++)
            {
                var ring = _rings[i];
                if (ring.pitch.Num == wave.SwPitch.Num) break;
                if (i == sizeOfRings - 1)
                {
                    _se.play(SENameList.Gimmick_Failure);
                    break;
                }
            }
        }
    }
}
