using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalkingMonster : Entity
{
    //private float speed = 3.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;
    private Transform player;
    private Rigidbody2D rb;
    private Vector3 movement;
    public float moveSpeed = 300f;
    public int lives = 6;
    public int Maxlives = 12;

    public HealthbarBehavior HealthbarBehavior;

    

    private void Start()
    {
        lives = Maxlives;
        HealthbarBehavior.SetHealth(lives, Maxlives);
        dir = transform.right;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Hero").transform;
    }
    private void Patrool()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
        if (colliders.Length > 0) {
            dir *= -1f; 
        }
            transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
        
    }
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 1.5f, 0.1f);
        /*if (colliders.Length > 0)
        {
            dir *= -1f;
        }*/
        transform.position = Vector3.MoveTowards(transform.position, player.position, (moveSpeed * Time.deltaTime));
    }
    /*private void TeleBomb()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
        if (colliders.Length > 0)
        {
            dir *= -1f;
        }
        movement = player.position - transform.position;
    }*/
    /*private void MoveAir()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;

        
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
            lives--;
            HealthbarBehavior.SetHealth(lives, Maxlives);

            Debug.Log("У ходуна  " + lives);
        }
        if (lives < 1)
        {
            HealthbarBehavior.SetHealth(lives, Maxlives);
            Die();
        }
    }
    void moveCharachters (Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    private void FixedUpdate()
    {
        moveCharachters(movement);
    }
    private void Update()
    {
        Move();
       

    }
}
