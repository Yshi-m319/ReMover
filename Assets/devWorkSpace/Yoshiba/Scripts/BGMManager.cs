using devWorkSpace.SoundTeam.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class BGMManager : MonoBehaviour
    {
        [SerializeField] private BGMBehaviour bgmControlScript;
        public BGM BGM => bgmControlScript.Bgm;
    }
}
