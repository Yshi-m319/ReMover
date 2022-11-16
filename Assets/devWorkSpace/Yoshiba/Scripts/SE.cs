using System;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace devWorkSpace.SoundTeam.Scripts
{
    
    public static class SENameList
    {
        // ReSharper disable once InconsistentNaming
        public const string Jump = "SFX07_Jump";
        public const string Switch = "SFX14_Gimmick_Switch";
        public const string Reflection01 = "SFX10_Soundwave_Reflection01";
        public const string Disappear = "SFX13_Soundwave_Disappear";
        public const string TuningFork = "SFX15_Gimmick_Tuningfork";
        public const string Microphone = "SFX16_Gimmick_Microphone";
        public const string Speaker = "SFX17_Gimmick_Speaker";
        public const string PitchChange = "SFX18_Gimmick_PitchChange";
        public const string Gimmick_Clear= "SFX19_Gimmick_Clear";
        public const string Clear= "Jingle01_Clear";
        public const string Decision= "SFX01_Decision";
        public const string Cancel= "SFX02_Cancell";
        public const string Gimmick_Failure = "SFX36_SoundimitationFailure";
        public const string Switch_OFF = "SFX38_MushroomswitchOFF";
        public const string Speaker_Turn = "SFX39_PhonographTurn";
        public const string LiftMove = "SFX42_LiftMove";
        public const string LiftMoveFirst = "SFX43_LiftMoveFirst";
        public const string LiftMoveFinal = "SFX44_LiftMoveFinal";
        public const string Chladni = "SFX45_Kuradoni";
        public const string Musicbox_Close = "SFX30_TreeFall";
        public const string ClearLabo = "Jingle04_ClearLabo";
        public const string Decision2 = "SFX01_Decision2";
        public const string Cancel2 = "SFX02_Cancell2";

    }
    [RequireComponent(typeof(CriAtomSource))]
    public class SE : MonoBehaviour
    {
        [SerializeField] CriAtomSource criAtomSource;

        public void playSoundWaveRelease(int pitchNumber)
        {
            if (pitchNumber == 0)
                pitchNumber = 1;
            // ReSharper disable once StringLiteralTypo
            criAtomSource.Play($"SFX{19+pitchNumber}_Soundwave_Release0{pitchNumber}");
        }

        public void play(string seName)
        {
            criAtomSource.Play(seName);
        }
        private void Reset()
        {
            criAtomSource = GetComponent<CriAtomSource>();
        }
    }
}
