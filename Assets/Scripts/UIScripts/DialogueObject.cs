using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour
{
    [Header("Dialogue Properties")]
    //Line: The text you want to display.
    public string line;
    //Speaker: the name of who speaks the line.
    public string speaker;
    //Duration: The time you want the message to stay up
    //My recommendation: .4f * number of words (Based on 145 WPM used in subtitles)
    public float duration;
    //lastLine: if true, then the dialog box will close, if false then the dialog box will read the next line.
    public bool lastLine;

    [Header("Dialogue by HP")]
    //RunByHP: If set to true, will trigger this dialogue based on health
    public bool runByHP;
    //PercentageOfHealth: The percentage of HP at which the dialogue will show up.
    public float percentageOfHealth;

}
