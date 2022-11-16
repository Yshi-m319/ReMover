using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using devWorkSpace.knd.Scripts;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class TempGoal : MonoBehaviour
    {
        [SerializeField] GameObject can;
        SE _se;
        BGM _bgm;
        private GameObject _player;

        // Start is called before the first frame update
        void Start()
        {
            var sfx = GameObject.FindGameObjectWithTag("SE");
            _se = sfx.GetComponent<SE>();

            _bgm = GameObject.FindWithTag("BGM").GetComponent<BGMManager>().BGM;

            _player = GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                can.SetActive(true);
                Debug.Log("enter");
                switch (_player.GetComponent<PlayerUtility>().IsWhere)
                {
                    case Stage.Forest:                        
                        _bgm.stop();
                        _se.play(SENameList.Clear);
                        Debug.Log("Forest");
                        break;

                    case Stage.Remine:                        
                        Debug.Log("Remine");
                        _bgm.stop();
                        _se.play(SENameList.ClearLabo);
                        break;
                }
                _player.GetComponent<PlayerMove>().goal();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Time.timeScale = 0f;
            }
        }
    }
}
