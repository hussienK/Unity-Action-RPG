/* < 8 - 14 - 2022 >
 * Hussien Kenaan
 * 
 * makes the boss behaive like an enemy and adds orbitals
 */
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] float[] fireballSpeed = { 2.5f, -2.5f};
    [SerializeField] float distance = 0.25f;
    [SerializeField] Transform[] fireballs;

    private void Update()
    {
        //go through the enemies and make them loop around boss
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + (new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance, Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0));
        }
    }

    protected override void RecieveDamage(Damage dmg)
    {
        base.RecieveDamage(dmg);
    }

}
