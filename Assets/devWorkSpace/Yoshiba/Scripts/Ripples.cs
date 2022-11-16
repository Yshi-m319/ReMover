using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class Ripples : MonoBehaviour
    {
        private PitchData _pitch;
        
        SE _se;
        BGM _bgm;
        [SerializeField] private GameObject bgmManager;

        private GameObject _player;
        private PlayerUtility _pUtil;
        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            _pUtil = _player.GetComponent<PlayerUtility>();
            
            _pitch = GetComponentInParent<SoundFluff>().Pitch;
            
            var sfx = GameObject.FindWithTag("SE");
            _bgm = GameObject.FindWithTag("BGM").GetComponent<BGMManager>().BGM;
            _se = sfx.GetComponent<SE>();
        }

        
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var waveShooter = other.gameObject.GetComponent<WaveShooter>();
                if (waveShooter.WavePitch.Num != _pitch.Num)
                {
                    waveShooter.WavePitch = _pitch;
                    _pUtil.changeManta();
                    soundStatus();
                    _se.play(SENameList.PitchChange);
                }
            }
        }
        
        void soundStatus()
        {
            var ctrlName = AisacNameList.TutorialPitchChange;
            switch (_player.GetComponent<PlayerUtility>().IsWhere)
            {
                case Stage.Tutorial:
                    ctrlName = AisacNameList.TutorialPitchChange;
                    break;
                case Stage.Forest:
                    ctrlName = AisacNameList.ForestPitchChange;
                    break;
                case Stage.Labo:
                    ctrlName = AisacNameList.LabPitchChange;
                    break;
                case Stage.Remine:
                    ctrlName = AisacNameList.RemPitchChange;
                    break;
            }
            
            StartCoroutine( _bgm.changeAisacFromTime(ctrlName,_pitch.Num/10f,0.5f));
            //StartCoroutine( _bgm.changeAisacFromTime(ctrlName,0.5f,0.5f));

        }
    }
}
