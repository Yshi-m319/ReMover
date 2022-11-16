using System;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

namespace devWorkSpace.Yoshiba.Scripts
{
    [Serializable]
    public class VolumeData
    {
        [SerializeField] float bgmVol;
        [SerializeField] float seVol;
        public float BgmVol
        {
            get => bgmVol;
            set => bgmVol = value;
        }

        public float SeVol 
        {
            get => seVol;
            set => seVol = value;
        }

        public VolumeData()
        {
            
        }
    }
    public class VolumeDataIO:MonoBehaviour
    {

        private VolumeData _data;
        private string _path;
        public VolumeData Data
        {
            get 
            {
                var json = File.ReadAllText(_path);
                return JsonUtility.FromJson<VolumeData>(json);
            }
            set
            {
                var json = JsonUtility.ToJson(value);
                File.WriteAllText(_path,json);
            }
            
        }
        void Awake()
        {
            _path=Application.persistentDataPath+"/VolumeDataIO.json";
            _data = new VolumeData();
            if (!File.Exists(_path))
            {
                _data.BgmVol = 1f;
                _data.SeVol = 1f;
                var json = JsonUtility.ToJson(_data);
                File.WriteAllText(_path,json);
            }
            else
            {
                var json = File.ReadAllText(_path);
                var tmp = JsonUtility.FromJson<VolumeData>(json);
                _data.BgmVol = tmp.BgmVol;
                _data.SeVol = tmp.SeVol;
            }
        }
    }
}
