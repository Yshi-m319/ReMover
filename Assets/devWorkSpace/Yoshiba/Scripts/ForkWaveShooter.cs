using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    
    public class ForkWaveShooter : MonoBehaviour
    {
        
        private PitchData _forkPitch;
        public GameObject forkL;
        public GameObject forkR;
        private Vector3 _leftPos;
        private Vector3 _rightPos;
        private Vector3 _pos;

        private Animator _animator;
        private static readonly int cIsVibration = Animator.StringToHash(_kVIB_STR);
        private const string _kVIB_STR = "isVibration";

        // Start is called before the first frame update
        private void Start()
        {
            const float offset = 1.5f;
            _forkPitch = GetComponentInParent<ForkStand>().StandPitch;
            _pos = transform.position;
            _leftPos = _pos;
            _rightPos = _pos;
            _leftPos.x = _pos.x - offset;
            _rightPos.x = _pos.x + offset;
            _animator = GetComponentInParent<Animator>();
        }

        public void shotWave(PitchData fPitch)
        {            
            if (fPitch == _forkPitch)
            {
                var rv = new Vector3(1,0,0);
                var rotation = Quaternion.LookRotation(rv);
                SoundWaveBehaviour.instantiate(forkL,_leftPos, rotation, _forkPitch);
                SoundWaveBehaviour.instantiate(forkR,_rightPos, rotation, _forkPitch);

                animVib();
            }            
        }

        private void animVib()
        {
            _animator.SetBool(cIsVibration, false);
            _animator.SetBool(cIsVibration, true);
            Invoke(nameof(animReset), 1.5f);
        }

        private void animReset()
        {
            _animator.SetBool(cIsVibration, false);
        }
    }
}
