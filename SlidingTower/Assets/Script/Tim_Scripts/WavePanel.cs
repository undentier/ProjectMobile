using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePanel : MonoBehaviour
{
    public GameObject panel;

    public GameObject[] upgrades;
    public Transform[] slots;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayPanel();
        }
        if (panel.activeSelf == true)
        {

        }
    }
    void DisplayPanel()
    {
        panel.SetActive(true);
        for (int i = 0; i < slots.Length; i++)
        {
          Instantiate(upgrades[Random.Range(0,upgrades.Length)], slots[i].position, Quaternion.identity);
        }
    }
}
