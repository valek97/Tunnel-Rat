using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Entity
{
    [SerializeField]private float speed = 5f;
    [SerializeField]private int lives = 5;
    [SerializeField]private float jumpForce = 50f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get;  set; }

    private States State
    {
        get { return (States)anim.GetFloat("state"); }
        set { anim.SetFloat("state",(float)value); }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Instance = this;
    }
    private void FixedUpdate()
    {
        Checkground();
    }
    private void Update()
    {
        if (isGrounded) State = States.idle;
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButton("Jump")) Jump();
    }
    private void Run()
    {
        if (isGrounded) State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.01f;
    }
    private void Jump()
    {
        
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void Checkground()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.8f);
        isGrounded = collider.Length > 1;
        if (!isGrounded) State = States.jump;
        //Debug.Log($"collider.Length = "+ collider.Length);
    }
    public override void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }

}
public enum States
{
    idle, 
    run,
    jump
}
