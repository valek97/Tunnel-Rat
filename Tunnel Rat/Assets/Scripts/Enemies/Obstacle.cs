using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : Entity
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }

       /* if (this.transform.position.y < -20f)
        {
            Die();
        }*/

    }

    private void Update()
    {
        if (this.transform.position.y < -20f)
        {
            Die();
        }
    }

}
