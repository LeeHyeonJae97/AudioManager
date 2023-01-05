using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCanvas : MonoBehaviour
{
    [SerializeField] private Toggle _bgmToggle;
    [SerializeField] private Toggle _sfxToggle;
    [SerializeField] private Settings _settings;

    private void Start()
    {
        _settings.Load();

        _bgmToggle.isOn = _settings.Bgm;
        _sfxToggle.isOn = _settings.Sfx;

        _bgmToggle.onValueChanged.AddListener(OnBgmToggleValueChanged);
        _sfxToggle.onValueChanged.AddListener(OnSfxToggleValueChanged);
    }

    private void OnDestroy()
    {
        _settings.Save();
    }

    private void OnBgmToggleValueChanged(bool value)
    {
        if (AudioChannel.TryGet("UISfx", out var channel))
        {
            channel.Play("UISfx");
        }

        _settings.Bgm = value;
    }

    private void OnSfxToggleValueChanged(bool value)
    {
        if (AudioChannel.TryGet("UISfx", out var channel))
        {
            channel.Play("UISfx");
        }

        _settings.Sfx = value;
    }
}
