using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using devWorkSpace.SoundTeam.Scripts;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class MusicBox : MonoBehaviour
    {
        public GameObject door;
        Vector3 _doorPos;
        Vector3 _openPos;
        Vector3 _closePos;
        [SerializeField] float rise;
        [SerializeField] float timeLimit;

        float _counter;

        bool _doorState = false;
        bool _oldDoor = false;
        [SerializeField] PitchData pitch;
        BGM _bgm;
        SE _se;

        private const string _kAISAC_CONTROL_NAME_L_MUSICBOX = AisacNameList.MusicBox;
        private GameObject _player;

        // Start is called before the first frame update
        void Start()
        {
            //changeStatus();
            _doorPos = door.transform.position;
            _closePos = _doorPos;
            float exY = _doorPos.y + rise;
            _openPos = _doorPos;
            _openPos.y = exY;
            _bgm = GameObject.FindWithTag("BGM").GetComponent<BGMManager>().BGM;
            //_bgm.setAisacControl(_kAISAC_CONTROL_NAME_L_MUSICBOX);
            _player = GameObject.FindWithTag("Player");
            var sfx = GameObject.FindWithTag("SE");
            _se = sfx.GetComponent<SE>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_oldDoor&& !_doorState) //閉じる瞬間
            {
                door.transform.position = _closePos;
                soundStatus(0f,3f);
                _se.play(SENameList.Musicbox_Close);
            }
            if (!_oldDoor && _doorState) //開く瞬間
            {
                door.transform.position = _openPos;
                soundStatus(1f,3f);
                _se.play(SENameList.Switch);
            }

            _oldDoor = _doorState;

            if (_doorState)
            {
                _counter -= Time.deltaTime;
                //emissionStatus();
            }
            if (_counter < 0)
            {
                _doorState = false;
                //_mat.SetColor(_EMISSION_COLOR, Color.black);
                //changeStatus();
            }
            
        }

        void soundStatus(float value ,float time)
        {
            var ctrlName = AisacNameList.MusicBox;

            StartCoroutine(_bgm.changeAisacFromTime(ctrlName, value, time));
            //StartCoroutine( _bgm.changeAisacFromTime(ctrlName,0.5f,0.5f));

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("SoundWave"))
                return;
            
            if (pitch.Num == 0)
            {
                _doorState = true; _counter = timeLimit;
            }
            else if (collision.gameObject.GetComponent<SoundWaveBehaviour>())
            {
                var swPitch = collision.gameObject.GetComponent<SoundWaveBehaviour>().SwPitch;

                if (swPitch == pitch)
                {
                    _doorState = true; _counter = timeLimit;
                }
            }
            //se.PlaySwitch();
            Destroy(collision.gameObject);
        }
    }
}
