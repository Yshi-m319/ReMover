using System;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using static devWorkSpace.SoundTeam.Scripts.BGMNameList;
using static devWorkSpace.SoundTeam.Scripts.SENameList;
using static devWorkSpace.SoundTeam.Scripts.AisacNameList;
using UnityEngine.Serialization;

namespace devWorkSpace.SoundTeam.Scripts
{
    public class LaboToRemine : BGMBehaviour
    {
        const string _kCUE_NAME = Labo;
        private GameObject _mimicArea;
        [SerializeField] private GameObject labMimicObj;
        private MimicDoor _labMimic;
        
        private GameObject _player;
        private Vector2 _aisacPoint;
        private float _lPitchParam=0.3f;
        private float _rPitchParam = 0.3f;
        SE _se;
        BGM _bgm;
        // Start is called before the first frame update
        void Awake()
        {
            _aisacPoint = new Vector2(146, 17);
            
            _labMimic = labMimicObj.GetComponent<MimicDoor>();
            _labMimic.actSetPitchParam += (pitch) =>
            {
                switch (pitch)
                {
                    case 1:
                        _lPitchParam = 0.1f;
                        break;
                    case 4:
                        _lPitchParam = 0.2f;
                        break;
                }

                switch (pitch)
                {
                    case 2:
                        _rPitchParam = 0.2f;
                        break;
                    case 4:
                        _rPitchParam = 0.4f;
                        break;
                    case 7:
                        _rPitchParam = 0.7f;
                        break;
                }
            };
            
            _player = GameObject.FindGameObjectWithTag("Player");
            
            BGM = new BGM(CueSheetName.BGM2,_kCUE_NAME);
            BGM.setAisacControl(LabPitchChange);
            BGM.play();
            var sfx = GameObject.Find("SE");
            _se = sfx.GetComponent<SE>();

            _bgm = GameObject.FindWithTag("BGM").GetComponent<BGMManager>().BGM;
        }
        
        private bool _isLabMimic = true;
        void Update()
        {
            var endPoint = new Vector2(_aisacPoint.x,_aisacPoint.y);
            var pPos = _player.transform.position;
            var mPos = _labMimic.transform.position;
            
            //ラボの音真似ドアが死んだときにBGMを戻す
            if ( _isLabMimic!= labMimicObj.activeSelf)
            {
                clearLabSound();
                _bgm.setBlockId(3);
            }
            _isLabMimic = labMimicObj.activeSelf;

            if(labMimicObj.activeSelf)
            {
                BGM.changeAisacFromPositionPoint(AisacNameList.LabMimicDistance, 8, mPos, pPos);
                BGM.changeAisacFromPositionPoint(AisacNameList.LabMimic, 30, mPos, pPos, 0f, _lPitchParam);
                BGM.changeAisacFromPositionPoint(AisacNameList.MimicReduce, 8, mPos, pPos, 0f, 1f);
            }

        }

        void clearLabSound()
        {
            //Cut4を仮置きしています。if文の入れ子で条件分岐する必要あり。（ID2,3,4）
            //TGS用に横遷移オフにしています
            BGM.setAisacControl(LabMimicDistance, 0.1f);
            BGM.setAisacControl(LabMimic, 0.1f);
            BGM.setAisacControl(MimicReduce, 0.1f);
            _se.play(SENameList.Gimmick_Clear);
        }

    }
}