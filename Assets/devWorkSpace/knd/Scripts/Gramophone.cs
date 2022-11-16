using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;

namespace devWorkSpace.knd.Scripts
{
    public class Gramophone : MonoBehaviour
    {
        private GameObject _speaker;
        private GameObject _rot;
        [SerializeField] private GameObject bullet;
        private PitchData _givePitch;
        private float _angleChangeTime;
        private bool _stop;
        private Vector3 _blastAngle;

        //private int _x=0;
        private int _y=0;
        private int _z=0;

        private SE _se;
        // Start is called before the first frame update
        private void Start()
        {
            _angleChangeTime = 1.25f;
            _speaker = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
            _rot = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
            var sfx = GameObject.FindGameObjectWithTag("SE");
            _se = sfx.GetComponent<SE>();
        }

        // Update is called once per frame
        private void Update()
        {
            _blastAngle = this.gameObject.transform.GetChild(0).GetChild(0).gameObject.transform.eulerAngles;                

            if (_angleChangeTime > 0)
            {
                _angleChangeTime -= Time.deltaTime;
            }

            if ((_y % 2 == 0) && _angleChangeTime <= 0)
            {
                _y++;
                _z = 0;
            }

            if (_y % 2 != 0 && _y !=9)
            {
                _blastAngle = new Vector3(_blastAngle.x, _blastAngle.y, _blastAngle.z + 3f);
                _z++;
                if (_z == 15)
                {
                    _y++;
                    _angleChangeTime = 1.25f;
                }
            }

            if (_y == 9)
            {
                _blastAngle = new Vector3(_blastAngle.x, _blastAngle.y, _blastAngle.z - 12.0f);
                _z++;
                if (_z == 15)
                {
                    _y = 0;
                    _angleChangeTime = 1.25f;
                }
            }

            this.gameObject.transform.GetChild(0).GetChild(0).gameObject.transform.eulerAngles = _blastAngle;
        }

        void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("SoundWave"))
                return;
            
            var bounce = 4;
            if (!other.GetComponent<ForkWave>())
            {
                var sw = other.GetComponent<SoundWave>();
                bounce = sw.Bounce;
            }
            
            var rotation = Quaternion.LookRotation(_rot.transform.rotation*Vector3.up);
            other.gameObject.transform.rotation = rotation;
            var pitch=other.GetComponent<SoundWaveBehaviour>().SwPitch;

            var pos = _speaker.transform.position;
            pos.z -= 1f;
            MicWave.instantiate(bullet, pos, rotation,pitch, bounce);
            _se.play(SENameList.Microphone);
            _se.play(SENameList.Speaker);
            Destroy(other.gameObject);
        }
    }
}
