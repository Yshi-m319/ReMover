using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class ForkStand : MonoBehaviour
    {
        [SerializeField] private PitchData standPitch;
        public PitchData StandPitch => standPitch;

        private SE _se;
        // Start is called before the first frame update
        void Start()
        {
            var sfx = GameObject.Find("SE");
            _se = sfx.GetComponent<SE>();
        }


        public void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("SoundWave")) return;
            if (collision.gameObject.GetComponent<ForkWave>()) return;
            Destroy(collision.gameObject);
                
            var forks = GameObject.FindGameObjectsWithTag("Metal");
            foreach (var fork in forks)
            {
                var fws = fork.GetComponent<ForkWaveShooter>();
                fws.shotWave(standPitch);
            }
                
            _se.play(SENameList.TuningFork);
        }
    }
}
