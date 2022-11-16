using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using devWorkSpace.SoundTeam.Scripts;
using devWorkSpace.Yoshiba.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SEManager : MonoBehaviour
{
    [FormerlySerializedAs("SEslider")] [SerializeField] Slider seSlider;
    CriAtomSource _atomSource;
    GameObject _player;

    private int _ct = 30;
    private float _oldValue;

    SE _se;

    PitchData _pitch;

    private VolumeDataIO _vol;
    // Start is called before the first frame update
    void Start()
    {
        var sfx = GameObject.Find("SE");
        _se = sfx.GetComponent<SE>();
        _player = GameObject.FindGameObjectWithTag("Player");

        if(SceneManager.GetActiveScene().name != "TempTitle")
            _pitch = _player.GetComponent<WaveShooter>().WavePitch;

        _atomSource = GetComponent<CriAtomSource>();
        seSlider.onValueChanged.AddListener(value => this._atomSource.volume = value);
    }
    private void Update()
    {
        _ct--;
        
        if (seSlider.value - _oldValue != 0)
        {            
            if (_ct < 0)
            {
                var num = 0;
                if (SceneManager.GetActiveScene().name == "TempTitle")
                    num = 0;
                else
                    num = _pitch.Num;
                _se.playSoundWaveRelease(num);
                _ct = 10;                
            }
        }

        _oldValue = seSlider.value;
    }
}
