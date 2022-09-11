/* < 8 - 14 - 2022 >
 * Hussien Kenaan
 * 
 * makes the NPC show text when interacted with
 */
using UnityEngine;

public class NPCTextPerson : Collidable
{
    [SerializeField] private string message;

    private float cooldown = 4.0f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollide(Collider2D col)
    {
        base.OnCollide(col);
        //show the assigned text when collided with NPC, has a cooldown
        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 25, Color.white, transform.position + new Vector3(0, 0.16f, 0), Vector3.zero, cooldown);
        }
    }
}
