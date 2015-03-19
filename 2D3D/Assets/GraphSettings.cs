using UnityEngine;
using System.Collections;

public class GraphSettings : MonoBehaviour {

    public UnityEngine.UI.Toggle absolute;
    public UnityEngine.UI.Slider resolution;
    public UnityEngine.UI.Text displayMode;
    public Grapher3 graph3;
    public bool mouseIsDown;

	void Awake() {
        graph3 = GameObject.Find("Graph 3").GetComponent<Grapher3>();
        ModeText();
        ResolutionVal();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) {
            Camera.main.transform.Rotate(graph3.transform.position, Input.GetAxis("Mouse X") * 100);
            mouseIsDown = true;
        }
        else
        {
            mouseIsDown = false;
        }
	}

    public void NextMode()
    {
        graph3.NextMode();
        ModeText();
    }
    public void PrevMode()
    {
        graph3.PrevMode();
        ModeText();
    }
    public void ModeText()
    {
        if (graph3){
            displayMode.text = graph3.GetMode();
        }
    }
    public void OnSliderChange(float val)
    {
        graph3.resolution = (int)val;
    }
    public void ResolutionVal()
    {
        resolution.maxValue = 30;
        resolution.minValue = 10;
        resolution.value = graph3.resolution;
    }
    public void GetAbs()
    {
        absolute.isOn = graph3.absolute;
    }

    public void SetAbsolute(bool value)
    {
        graph3.absolute = value;
    }
}
