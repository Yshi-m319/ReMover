using System;
using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class SoundWaveBehaviour : MonoBehaviour
    {
        private Material _mat;
        protected const float kVEL = 10f;
        

        public PitchData SwPitch { get; set; }

        // Start is called before the first frame update
        protected void Start()
        {
            _mat = GetComponent<MeshRenderer>().material;
            init();
        }

        protected virtual void init()
        {
            
        }
        
        public static GameObject instantiate(GameObject origin,Vector3 pos,Quaternion rotate,PitchData pitch)
        {
            var obj = Instantiate(origin,pos,rotate);
            var comp = obj.GetComponent<SoundWaveBehaviour>();
            comp.SwPitch = pitch;
            return obj;
        }
        
        protected void changeStatus(PitchData pitch)
        {
            SwPitch = pitch;
            _mat.color = pitch.Color;
        }
    }
}
