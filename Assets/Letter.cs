using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    [SerializeField] public float _Score;
    public string letter;
    [SerializeField] private TMP_Text TMPText;
    public bool isPicked = false;
    private Vector3 mousePosition;
    public Vector3 initialPosition;
    public bool isLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        letter = TMPText.text;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPicked)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z; // Use object's current depth
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

    }

    public void PickLetter()
    {
        initialPosition = transform.position;
    }
}