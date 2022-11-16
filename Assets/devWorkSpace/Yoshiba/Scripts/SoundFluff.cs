using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using static devWorkSpace.SoundTeam.Scripts.AisacNameList;
using static devWorkSpace.SoundTeam.Scripts.SENameList;
using static devWorkSpace.SoundTeam.Scripts.BGMNameList;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class SoundFluff : MonoBehaviour
    {
        [SerializeField] private PitchData pitch;
        public PitchData Pitch => pitch;
    }
}
