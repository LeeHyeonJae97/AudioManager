using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObject/Settings")]
public class Settings : ScriptableObject
{
    public bool Bgm
    {
        get { return _audioSettings.bgm; }

        set
        {
            _audioSettings.bgm = value;
            _audioMixer.SetFloat("BgmVolume", value ? 0f : -80f);
        }
    }
    public bool Sfx
    {
        get { return _audioSettings.sfx; }

        set
        {
            _audioSettings.sfx = value;
            _audioMixer.SetFloat("PlayerSfxVolume", value ? 0f : -80f);
            _audioMixer.SetFloat("UISfxVolume", value ? 0f : -80f);
        }
    }
    private string DirPath => $"{Application.persistentDataPath}/Settings";
    private string FilePath => $"{DirPath}/Settings.json";

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSettings _audioSettings;

    /// <summary>
    /// should be called same or after 'Start' because of the snapshot of audiomixer
    /// </summary>
    public void Load()
    {
        if (File.Exists(FilePath))
        {
            JsonUtility.FromJsonOverwrite(File.ReadAllText(FilePath), _audioSettings);
        }

        Bgm = _audioSettings.bgm;
        Sfx = _audioSettings.sfx;
    }

    public void Save()
    {
        if (!Directory.Exists(DirPath))
        {
            Directory.CreateDirectory(DirPath);
        }
        File.WriteAllText(FilePath, JsonUtility.ToJson(_audioSettings));
    }

    [System.Serializable]
    private struct AudioSettings
    {
        public bool bgm;
        public bool sfx;
    }
}
