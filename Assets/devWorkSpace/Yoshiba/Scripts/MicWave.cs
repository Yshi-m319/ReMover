using System.Runtime.CompilerServices;
using UnityEngine;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class MicWave : SoundWave
    {
        public new static GameObject instantiate(GameObject origin, Vector3 pos, Quaternion rotate, PitchData pitch,int bounceCnt)
        {
            var sw=origin.GetComponent<SoundWave>();
            sw.Bounce = bounceCnt;
            return instantiate(origin, pos, rotate, pitch);
        }
    }
}
