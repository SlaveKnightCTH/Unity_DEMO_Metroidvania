using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private float xinput;
    private Animator animator;
    private Rigidbody2D rb;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        xinput = Input.GetAxis("Horizontal");

        if (xinput != 0)
        {
            animator.SetBool("move", true);
            rb.velocity = new Vector2(xinput, 0);
        }


    }
}
