using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlPanelText : MonoBehaviour {

    public bool TextEnabled = false;
    public GameObject NextLevelLaserGrid;
    public GameObject[] VentilationLaserGrid;
    public GameObject ObjectiveArrow1;
    public GameObject ObjectiveArrow2;
    MeshRenderer Text;

    void Start()
    {
        if (NextLevelLaserGrid == null)
            Debug.Log("NextLevelGrid is not assigned!");

        if (VentilationLaserGrid == null ||VentilationLaserGrid.Length == 0)
            Debug.Log("VentilationGrid is not assigned!");

        if (ObjectiveArrow1 == null)
            Debug.Log("ObjectiveArrow1 is not assigned!");

        if (ObjectiveArrow2 == null)
            Debug.Log("ObjectiveArrow2 is not assigned!");

        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        Text = children[1].GetComponent<MeshRenderer>();
        if(Text == null)
            Debug.Log("3D text object is not found!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && TextEnabled)
        {
            //Disable next level laser grid
            NextLevelLaserGrid.SetActive(false);

            //Enable ventilation laser grids
            for (int i = 0; i < VentilationLaserGrid.Length; i++)
                VentilationLaserGrid[i].SetActive(true);


            ObjectiveArrow1.SetActive(false);
            ObjectiveArrow2.SetActive(true);
            GameObject.Find("ObjectiveIndicatorText").GetComponent<Text>().text = "Objective: get back to stairs";
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "";
            this.gameObject.SetActive(false);

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Enable Text
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "press  [e]  to  disable  laser  grid";
            TextEnabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Disable text
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "";
            TextEnabled = false;
        }
    }
}
