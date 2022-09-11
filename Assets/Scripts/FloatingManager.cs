/* < 8 - 16 - 2022 >
 * Hussien Kenaan
 * 
 * Manage the floating texts in the scene, the floating text system works using pooling to save on performance
 */
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingManager : MonoBehaviour
{
    //references
    [SerializeField] private GameObject textContainer;
    [SerializeField] private GameObject textPrefab;

    //keep track of all floating text
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    //update all the floating texts spawned
    private void Update()
    {
        foreach (FloatingText t in floatingTexts)
        {
            t.UpdateFloatingText();
        }
    }

    //called to find a floating text and give reference to it, saves performance
    private FloatingText GetFloatingText()
    {
        //finds a none active txt in list and assigns it as txt
        FloatingText txt = floatingTexts.Find(t => !t.active);

        //if no none active txt is found, create a new txt GO
        if (txt == null)
        {
            GameObject go = Instantiate(textPrefab);
            txt = go.AddComponent<FloatingText>();
            txt.go = go;
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<TextMeshProUGUI>();

            floatingTexts.Add(txt);
        }

        return txt;
    }

    //called to modify the selected txt GO to selected settings and set to visible
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        //get or create a txt
        FloatingText txt = GetFloatingText();

        //set the text vars
        txt.txt.text = msg;
        txt.txt.fontSize = fontSize;
        txt.txt.color = color;

        //set more vars
        txt.go.transform.position = Camera.main.WorldToScreenPoint(position); //transfer worls space to screen space so we can use it in the UI
        txt.motion = motion;
        txt.duration = duration;

        //activate the txt
        txt.Show();
    }
}
