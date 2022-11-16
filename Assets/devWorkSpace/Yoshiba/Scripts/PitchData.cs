using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace devWorkSpace.Yoshiba.Scripts
{
    [CreateAssetMenu(menuName = "Create PitchData"),Serializable]    
    public class PitchData : ScriptableObject
    {
        [SerializeField,Range(0,7)] int num;
        [SerializeField] new string name;
        [SerializeField] Color color;
        private string _seName;
        public int Num => num;
        public string Name => name;
        public Color Color => color;

        /*public static PitchData NullPitch
        {
            get
            {
                var a = ScriptableObject.CreateInstance<PitchData>();
                a._color = new Color(0f, 0f, 0f, 0f);
                a._name = "NULL PITCH";
                a.num = 0;
                return a;
            }
        }*/

    }
}
