using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private PlayerSfxChannel _playerSfxChannel;

    private AudioSource _playeSfxAudioSource;

    private void Start()
    {
        if (AudioChannel.TryGet("Bgm", out var channel))
        {
            channel.Play("Bgm", loop: true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playeSfxAudioSource = _playerSfxChannel.Play("PlayerSfx", transform, Vector3.zero, loop: true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _playerSfxChannel.Stop(_playeSfxAudioSource);
        }
    }
}
