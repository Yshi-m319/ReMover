using System.Runtime.InteropServices;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class ForkWave : SoundWaveBehaviour
    {
        public enum ForkMode
        {
            Right,
            Left
        }
        [SerializeField] private ForkMode mode;
        public ForkMode Mode => mode;

        protected override void init()
        {
            var sign = mode == ForkMode.Right ? 1f : -1f;
            var tr = transform;
            var rb = GetComponent<Rigidbody>();
            var rv=tr.forward * sign;
            tr.rotation=Quaternion.LookRotation(rv);
            rb.AddForce(kVEL*tr.forward, ForceMode.Impulse);
            changeStatus(SwPitch);
        }

        private void Update()
        {
            if (this.transform.position.z != 0.0f)
            {
                var thisTr = this.transform;
                var p = thisTr.position;
                p.z = 0.0f;
                thisTr.position = p;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag("TuningFork"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
