using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatePlayer))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private GameObject _warningPoint;
    [SerializeField] private Transform _path;

    private SpriteRenderer _sprite;
    private AnimatePlayer _animation;

    private float _speed = 1f;
    private int _currentPoint;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animation = GetComponent<AnimatePlayer>();
    }

    private void Update()
    {
        if (_currentPoint < _points.Count)
        {
            Transform target = _points[_currentPoint];
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            _animation.EnableWalk(_speed);

            if (transform.position.x == target.position.x)
            {
                _currentPoint++;
                if (transform.position.x == _warningPoint.transform.position.x)
                {
                    _speed = 2f;
                    _sprite.flipX = true;
                }
            }
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Home>(out Home home))
        {
            home.OpenDoor();
            _animation.EnableHit();            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Home>(out Home home))
        {
            home.CloseDoor();
        }
    }
}
