using System;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using static devWorkSpace.SoundTeam.Scripts.BGMNameList;
using static devWorkSpace.SoundTeam.Scripts.AisacNameList;
namespace devWorkSpace.SoundTeam.Scripts
{
    //チュートリアル～森専用
    public class TutorialToForest : BGMBehaviour
    {
        
        GameObject _player;//サウロくん
        const string _kCUE_NAME = TutorialIntro;
        const string _kAISAC_CONTROL_NAME_TTO_F = AisacNameList.TutorialToForest;
        const string _kT_PITCH_CHANGE = TutorialPitchChange;
        

        // Start is called before the first frame update
        void Awake()
        {
            _player=GameObject.FindWithTag("Player");
            BGM = new BGM(CueSheetName.BGM1,_kCUE_NAME);
            
            BGM.setAisacControl(new []{
                _kAISAC_CONTROL_NAME_TTO_F,
                _kT_PITCH_CHANGE,
                "FPitchChange"});
            BGM.play();
        }

        // Update is called once per frame
        void Update()
        {
            var position = _player.transform.position;

            Vector2 start = new Vector2(126, position.y);
            Vector2 end = new Vector2(175, position.y);

            if (position.x >= 126f && position.x <=175f)
            {
                
                StartCoroutine(BGM.changeAisacFromTime(_kT_PITCH_CHANGE, 0f, 0.5f));
                BGM.changeAisacFromPositionBorder(_kAISAC_CONTROL_NAME_TTO_F, start, end, position);
            }
        }
        
    }
}
