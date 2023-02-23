using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadTest : MonoBehaviour
{
    TMPro.TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text =  "Select      : " + GamepadUtils.ButtonValue("MainSelect") + "\n" + 
                    "Alternative : " + GamepadUtils.ButtonValue("AltSelect") + "\n" +
                    "up : " + GamepadUtils.ButtonValue("Up") + "\n" + 
                    "Look : " + GamepadUtils.ButtonValue("Look");

        Debug.Log(txt.text);
    }
}
