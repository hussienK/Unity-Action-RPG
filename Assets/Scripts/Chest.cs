/* < 8 - 15 - 2022 >
 * Hussien Kenaan
 * 
 * a collectable of type chest that gives player pesos
 */
using UnityEngine;

public class Chest : Collectable
{
    [SerializeField] private Sprite emptyChest;
    [SerializeField] private int pesosAmount;

    //override collect function
    protected override void OnCollect()
    {
        //give player pesos if not collected already
        if (!collected)
        {
            collected = true;

            GetComponent<SpriteRenderer>().sprite = emptyChest;

            GameManager.instance.AddPesos(pesosAmount);

            //show the UI
            GameManager.instance.ShowText("+" + pesosAmount + " pesos!", 25, Color.yellow, transform.position, Vector3.up * 25, 3.0f);
        }
    }
} 
