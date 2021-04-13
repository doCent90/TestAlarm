using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AnimatePlayer : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void EnableHit()
    {
        _animator.SetTrigger("Hit");
    }

    public void EnableWalk(float speed)
    {
        if (speed > 0)
        {
            _animator.SetFloat("Walk", speed);
        }
        else
        {
            _animator.SetFloat("Walk", 0);
        }
    }
}
