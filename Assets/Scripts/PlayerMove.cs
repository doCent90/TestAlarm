using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private SpriteRenderer _sprite;
    private AnimatePlayer _animation;
    private Transform[] _points;

    private float _speed = 1f;
    private int _currentPoint;

    private void Start()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _animation = gameObject.GetComponent<AnimatePlayer>();
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _points.Length; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        if (_currentPoint < _points.Length)
        {
            Transform target = _points[_currentPoint];

            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

            _animation.EnableWalk(_speed);            

            if (transform.position.x == target.position.x)
            {
                _currentPoint++;
                if (_currentPoint == 3)
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
