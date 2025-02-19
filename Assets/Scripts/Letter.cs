using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    [SerializeField] private float _Score;
    private string _Letter;
    [SerializeField] private TMP_Text TMPText;
    public bool isPicked = false;
    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        _Letter = TMPText.text;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPicked)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z; // Use object's current depth
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

    }

    private void PickLetter()
    {
        

    }
}
