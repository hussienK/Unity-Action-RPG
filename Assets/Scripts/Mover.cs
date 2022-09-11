/* < 8 - 17 - 2022 >
 * Hussien Kenaan
 * 
 * an abstract class for enemies, player, etc.. to inherit from. responsible for moving the player and inherits from fighter class
 */
using UnityEngine;

public abstract class Mover : Fighter
{
    private BoxCollider2D coll;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //reset move vector
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //assign correct look direction
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //add push vector if exists
        moveDelta += pushDirection;

        //reduce force based on off recover speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //cast a box to see if can move in direction Y
        hit = Physics2D.BoxCast(transform.position, coll.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //make the player move upward
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //cast a box to see if can move in direction X
        hit = Physics2D.BoxCast(transform.position, coll.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //make the player move Side
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
