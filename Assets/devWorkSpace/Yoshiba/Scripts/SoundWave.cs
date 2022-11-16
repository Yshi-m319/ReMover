using System.Diagnostics;
using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class SoundWave : SoundWaveBehaviour
    {
        private float _lifetime = 10;

        private SE _se;
        
        [SerializeField] protected int bounce = 4;
        public int Bounce
        {
            get => bounce;
            set => bounce = value;
        }

        protected override void init()
        {
            var rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward*kVEL,ForceMode.VelocityChange);
            _se = GameObject.FindGameObjectWithTag("SE").GetComponent<SE>();
            changeStatus(SwPitch);
        }
        
        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0)
                Destroy(this.gameObject);
            
            if (this.transform.position.z == 0.0f) 
                return;
            
            var thisTr = this.transform;
            var p = thisTr.position;
            p.z = 0.0f;
            thisTr.position = p;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            bounce--;
            if (bounce >= 1)
            {
                _se.play(SENameList.Reflection01);
            }

            if (other.gameObject.CompareTag($"MuteObject") || bounce <= 0)
            {
                _se.play(SENameList.Disappear);
                Destroy(this.gameObject);

            }
        }
    }
}
