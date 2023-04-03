using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseRotate : MonoBehaviour
{


    public Slider xSlider;
    public Slider ySlider;
    public GameObject theTarget;

    void Start()
    {

        GetXRotation();
        GetYRotation();

    }

    public void GetXRotation()
    {
        xSlider.value = theTarget.transform.eulerAngles.x;

    }

    public void SetXRotation()
    {
        Vector3 tempRotation = theTarget.transform.eulerAngles;
        tempRotation.x = xSlider.value;
        theTarget.transform.rotation = Quaternion.Euler(tempRotation);
    }

    public void GetYRotation()
    {

        ySlider.value = theTarget.transform.eulerAngles.y;

    }

    public void SetYRotation()
    {
        Vector3 tempRotation = theTarget.transform.eulerAngles;
        tempRotation.y = ySlider.value;
        theTarget.transform.rotation = Quaternion.Euler(tempRotation);

    }


}