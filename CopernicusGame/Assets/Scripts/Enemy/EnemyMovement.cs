using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int MovementSpeed = 20;
    public Transform Left, Right;
    private bool left = true;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - (MovementSpeed / 10 * Time.fixedDeltaTime), transform.position.y);

        if(transform.position.x <= Left.position.x || transform.position.x >= Right.position.x)
        {
            if(left == true)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
                left = false;
            }
            else
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
                left = true;
            }

            MovementSpeed *= -1;
        }
    }
}
