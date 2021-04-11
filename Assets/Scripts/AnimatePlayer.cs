using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void EnableHit()
    {
        _animator.SetTrigger("Hit");
    }

    public void EnableWalk(float value)
    {
        if (value > 0)
        {
            _animator.SetFloat("Walk", value);
        }
        else
        {
            _animator.SetFloat("Walk", 0);
        }
    }
}
