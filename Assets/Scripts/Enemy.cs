/* < 8 - 17 - 2022 >
 * Hussien Kenaan
 * 
 * handles the enemy AI
 */
using UnityEngine;

public class Enemy : Mover
{
    //experience
    [SerializeField] private int xpValue = 1;

    //logic
    [SerializeField] private float triggerLength = 1f;
    [SerializeField] private float chaseLength = 5f;
    private bool chasing;
    private bool collisionWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //hitbox
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    [SerializeField] private ContactFilter2D filter;

    protected override void Start()
    {
        base.Start();

        //assign initial values
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //is the player in chase range
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
             if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                chasing = true;
            }

            //if should start chasing
            if (chasing)
            {
                //move toward player
                if (!collisionWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                //return back
                UpdateMotor((startingPosition - transform.position).normalized);
            }
        }
        //if went out of chasing range
        else
        {
            //snap back when almost reached point
            if (Vector3.Distance(transform.position, startingPosition) < 0.02f)
            {
                transform.position = startingPosition;
                Debug.Log("IN POSITION");
            }
            else
            {
                //return back
                UpdateMotor((startingPosition - transform.position));
                chasing = false;
            }
        }

        //check for overlaps
        collisionWithPlayer = false;
        //get collisions on collider 
        hitbox.OverlapCollider(filter, hits);

        //loop through all collisions and handle them
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].tag == "Figher" && hits[i].name == "Player")
            {
                collisionWithPlayer = true;
            }

            //clear the collsiion list
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        GameManager.instance.GrantXP(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        Destroy(gameObject);
    }
}
