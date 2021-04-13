using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class Home : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private AudioMixerGroup _audio;

    private AudioSource _alarm;
    private bool _isPlayerInside;

    private void Start()
    {
        _alarm = GetComponent<AudioSource>();
        _audio.audioMixer.SetFloat("Alarm", -60);
        _isPlayerInside = false;
    }

    private void Update()
    {
        ChangeVolume(20, -60, 0);
    }

    private void ChangeVolume(float maxDelta, float minVolumeDB, float maxVolumeDB)
    {
        float volumeDB;
        _audio.audioMixer.GetFloat("Alarm", out volumeDB);

        if (_isPlayerInside)
        {
            if (volumeDB < maxVolumeDB)
            {
                SetAlarmVolume(Mathf.MoveTowards(volumeDB, maxVolumeDB, maxDelta * Time.deltaTime));
            }
        }
        else if (_isPlayerInside == false)
        {
            if (volumeDB > minVolumeDB)
            {
                SetAlarmVolume(Mathf.MoveTowards(volumeDB, minVolumeDB, maxDelta * Time.deltaTime));

                if (volumeDB == minVolumeDB)
                {
                    DeactiveAlarm();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            _isPlayerInside = true;
            ActivateAlarm();            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            _isPlayerInside = false;
        }
    }

    private void ActivateAlarm()
    {
        _alarm.Play();
    }

    private void DeactiveAlarm()
    {
        _alarm.Stop();
    }

    private void SetAlarmVolume(float volume)
    {
        _audio.audioMixer.SetFloat("Alarm", volume);
    }
    public void OpenDoor()
    {
        _door.SetActive(true);
    }

    public void CloseDoor()
    {
        _door.SetActive(false);
    }
}
