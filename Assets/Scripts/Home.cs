using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private AudioMixerGroup _audio;

    private AudioSource _alarm;
    private bool _isHacked;

    private void Start()
    {
        _alarm = gameObject.GetComponent<AudioSource>();
        _audio.audioMixer.SetFloat("Alarm", -60);
        _isHacked = false;
    }

    private void Update()
    {
        ChangeVolume(20, -60, 0);
    }

    private void ChangeVolume(float step, float min, float max)
    {
        float value;
        _audio.audioMixer.GetFloat("Alarm", out value);

        if (_isHacked)
        {
            if (value < max)
            {
                SetAlarmVolume(Mathf.MoveTowards(value, max, step * Time.deltaTime));
            }
        }
        else if (!_isHacked)
        {
            if (value > min)
            {
                SetAlarmVolume(Mathf.MoveTowards(value, min, step * Time.deltaTime));

                if (value == min)
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
            _isHacked = true;
            ActivateAlarm();            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMove>(out PlayerMove player))
        {
            _isHacked = false;
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

    private void SetAlarmVolume(float value)
    {
        _audio.audioMixer.SetFloat("Alarm", value);
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
