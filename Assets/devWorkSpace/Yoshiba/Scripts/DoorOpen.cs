using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class DoorOpen : MonoBehaviour
    {
        [SerializeField] GameObject door;
        [SerializeField] PitchData switchPitch;
        GameObject _sfx;
        SE _se;
        // Start is called before the first frame update
        void Start()
        {
            _sfx = GameObject.Find("SE");
            _se = _sfx.GetComponent<SE>();
        }

        void changeDoorSituation()
        {
            if (door.activeSelf)//消えるとき
            {
                _se.play(SENameList.Switch_OFF);
            }
            else //現れるとき
            { 
                _se.play(SENameList.Switch);
            }
            door.SetActive(!door.activeInHierarchy);
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("SoundWave"))
            {
                if (switchPitch.Num==0)
                {
                    changeDoorSituation();
                }
                else if(collision.gameObject.GetComponent<SoundWaveBehaviour>())
                {
                    var swPitch = collision.gameObject.GetComponent<SoundWaveBehaviour>().SwPitch;

                    if (swPitch == switchPitch)
                    {
                        changeDoorSituation();
                    }
                }

                Destroy(collision.gameObject);
            }
        }
    }
}
