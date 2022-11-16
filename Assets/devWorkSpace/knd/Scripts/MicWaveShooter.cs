using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;

namespace devWorkSpace.knd.Scripts
{
    public class MicWaveShooter : MonoBehaviour
    {
        [SerializeField] GameObject bullet;
        GameObject _speaker;
        GameObject _rot;
        SE _se;
        // Start is called before the first frame update
        void Start()
        {
            _speaker = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
            _rot = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
            var sfx = GameObject.FindGameObjectWithTag("SE");
            _se = sfx.GetComponent<SE>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("SoundWave")) 
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
            MicWave.instantiate(bullet, pos, rotation,pitch,bounce);
            _se.play(SENameList.Microphone);
            _se.play(SENameList.Speaker);
            Destroy(other.gameObject);
        }

    }
}
