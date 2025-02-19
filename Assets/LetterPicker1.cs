using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPicker : MonoBehaviour
{
    private Letter _Letter;
    private Tile _Tile;
    private bool hasPicked = false;
    [SerializeField] private LayerMask LetterLayer;
    private int layerMask;
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        layerMask = ~LayerMask.GetMask("Letter"); // Ignores "Letter" layer
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasPicked)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Letter"))
            {
                Debug.Log("Objet touche : " + hit.collider.name);
                _Letter = hit.collider.GetComponent<Letter>();
                _Letter.isPicked = true;
                hasPicked = true;
            }
        }
        else if (Input.GetMouseButtonDown(0) && hasPicked)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.CompareTag("Tile"))
            {
                Debug.Log("Objet touche : " + hit.collider.name);
                _Tile = hit.collider.GetComponent<Tile>();
                _Letter.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f);
                _Letter.isPicked = false;
                hasPicked = false;
            }
        }
    }
}
