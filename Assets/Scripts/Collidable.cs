/* < 8 - 15 - 2022 >
 * Hussien Kenaan
 * 
 * base script for anything that collides
 */
using UnityEngine;

public class Collidable : MonoBehaviour
{
    [SerializeField] private ContactFilter2D filter;
    private BoxCollider2D coll;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        coll = gameObject.GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        //get collisions on collider 
        coll.OverlapCollider(filter, hits);

        //loop through all collisions and handle them
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            OnCollide(hits[i]);

            //clear the collsiion list
            hits[i] = null;
        }
    }

    //called when colliding, changed by inherited object based on needs
    protected virtual void OnCollide(Collider2D col)
    {

    }
}
