using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class Ring
    {
        public readonly GameObject speaker;
        private Material _ansMat,_hintMat;
        public readonly PitchData pitch;
        private static readonly int cEmissionColor = Shader.PropertyToID("_EmissionColor");
        public bool IsHit { get; set; } = false;

        public Ring(GameObject speaker,PitchData pitch)
        {
            this.speaker = speaker;
            this.pitch = pitch;
            
        }
        
        public void changeColor()
        {
            _ansMat = speaker.transform.Find("SpeakerRing").gameObject.GetComponent<MeshRenderer>().material;
            _ansMat.SetColor(cEmissionColor,pitch.Color);
        }

        public void changeHintColor()
        {
            #if UNITY_EDITOR
            var renderer = speaker.transform.Find("HintRing").gameObject.GetComponent<MeshRenderer>();
            _hintMat = new Material(renderer.sharedMaterial);
            _hintMat.SetColor(cEmissionColor,pitch.Color);
            renderer.sharedMaterial = _hintMat;
            #else
            _hintMat = speaker.transform.Find("HintRing").gameObject.GetComponent<MeshRenderer>().material;
            _hintMat.SetColor(cEmissionColor,pitch.Color);
            #endif
            
        }
    }
}
