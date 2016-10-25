using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandInputController : MonoBehaviour
{
    InputField input;
    InputField.SubmitEvent se;
    public Text output;

    public void ShowCommandProcessResult(string aStr)
    {
        output.text = aStr;
    }

    // Use this for initialization
    void Start()
    {
        input = this.GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);

        input.onEndEdit = se;
    }

    private void SubmitInput(string arg0)
    {
        CommandProcessor aCmd = new CommandProcessor();
        aCmd.Parse(arg0, ShowCommandProcessResult);

        input.text = "";
        input.ActivateInputField();
    }
}
