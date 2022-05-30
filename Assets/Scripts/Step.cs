using UnityEngine;

public class Step : MonoBehaviour
{
    #region Variables

    public string DebugHeaderText;
    [TextArea(4,8)]
    public string DescriptionText;
    [TextArea(4,6)]
    public string ChoicesText;
    [TextArea(4,6)]
    public string LocationText;

    public Sprite MyImage;

    public Step[] Choices;

    #endregion

}