using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale == 0)
        {
            animator.speed = 0;
            return;
        }
        else
        {
            animator.speed = 1;
        }

    }

    public void TriggerAttackAnimation()
    {
        animator.SetTrigger("attack");
    }

    public void SetHurt()
    {
        animator.SetBool("hurt", true);
        Invoke("ResetHurt", 1f);
    }

    public void SetDie()
    {
        animator.SetBool("die", true);
    }

    private void ResetHurt()
    {
        animator.SetBool("hurt", false);
    }
}