using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace devWorkSpace.Yoshiba.Scripts
{
    public class VolumeManager : MonoBehaviour
    {
        [SerializeField] Slider bgmVolSlider;
        [SerializeField] Slider seVolSlider;
        const string _kBGM_TAG="BGM",_kSE_TAG="SE";
        CriAtomSource _bgmAtom,_seAtom;
        private int _pNum;
        private VolumeData _vol;
        private VolumeDataIO _io;
        private SE _se;
    
        // Start is called before the first frame update
        void Start()
        {
            _io = gameObject.GetComponent<VolumeDataIO>();
            //ファイル読み込み
            _vol = _io.Data;
            _bgmAtom = GameObject.FindGameObjectWithTag(_kBGM_TAG).GetComponent<CriAtomSource>();
            _seAtom = GameObject.FindGameObjectWithTag(_kSE_TAG).GetComponent<CriAtomSource>();
            _se = GameObject.FindGameObjectWithTag(_kSE_TAG).GetComponent<SE>();
        
            if(GameObject.FindWithTag("Player"))
            {
                var player = GameObject.FindWithTag("Player");
                _pNum = player.GetComponent<WaveShooter>().WavePitch.Num;
            }
            else
            {
                _pNum = 0;
            }
        
        
            CriAtom.SetCategoryVolume(_kBGM_TAG, _vol.BgmVol);
            //CriAtom.SetCategoryVolume(_kSE_TAG, _vol.SeVol);
            _seAtom.volume = _vol.SeVol;

            //Slider操作で音量が変わりる
            //VolumeDataクラスに登録される
            bgmVolSlider.onValueChanged.AddListener((value) =>
            {
                _vol.BgmVol = value;
                CriAtom.SetCategoryVolume(_kBGM_TAG, value);
            });
        
            seVolSlider.onValueChanged.AddListener((value) =>
            {
                _vol.SeVol = value;
                _seAtom.volume = _vol.SeVol;
            });
            //Sliderの値を音量と同期させる
            bgmVolSlider.value = _vol.BgmVol;
            seVolSlider.value = _vol.SeVol;
        }

        private float _oldV;
        private int _ct = 10;
        private bool _oldActive = false;
        private void Update()
        {
            //オプションが閉じれば彼らも消えるので判定に使う
            var active = seVolSlider.IsActive() && bgmVolSlider.IsActive();
            if (active)
            {
                _ct--;
                if (seVolSlider.value - _oldV != 0&&_ct<0)
                {
                    _se.playSoundWaveRelease(_pNum);
                    _ct = 10;
                }
                _oldV = seVolSlider.value;
            }
            else if(_oldActive!=false)
            {
                _io.Data = _vol;
            }
            _oldActive = active;
        }
    }
}
