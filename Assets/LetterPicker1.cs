using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WordDirection
{
    Horizontal,
    Vertical
}
public class LetterPicker : MonoBehaviour
{
    private Letter _Letter;
    private Tile _Tile;
    private bool hasPicked = false;
    [SerializeField] private LayerMask LetterLayer;
    private int layerMask;
    private Ray ray;
    private RaycastHit hit;

    private bool isPreviousLetterPlaced = false;
    private Vector3 firstLetterPos;
    private Dictionary<Vector3, float> placedLetters = new Dictionary<Vector3, float>();
    private List<Vector3> positionList = new List<Vector3>();
    private WordDirection wordDirection;

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
                _Letter = hit.collider.GetComponent<Letter>();
                _Letter.isPicked = true;
                _Letter.PickLetter();
                hasPicked = true;
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.CompareTag("Tile"))
            {
                _Tile = hit.collider.GetComponent<Tile>();
                if(_Tile.isOccupied)
                {
                    _Tile.RemoveLetter();
                    placedLetters.Remove(_Letter.transform.position);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && hasPicked == true)
        {
            _Letter.isPicked = false;
            hasPicked = false;
            _Letter.transform.position = _Letter.initialPosition;

        }
        else if (Input.GetMouseButtonDown(0) && hasPicked)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && hit.collider.CompareTag("Tile"))
            {
                _Tile = hit.collider.GetComponent<Tile>();
                if (isPreviousLetterPlaced == true && _Tile.isOccupied == false)
                {
                    CheckLine(_Tile, _Letter);
                }
                else if (isPreviousLetterPlaced == false && _Tile.isOccupied == false)
                {
                    _Letter.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f);
                    _Letter.isPicked = false;
                    _Tile.PlaceLetter(_Letter.letter, _Letter._Score);
                    //placedLetters.Add(_Letter.)
                    hasPicked = false;
                    positionList.Add(_Letter.transform.position);
                    firstLetterPos = _Tile.transform.position;
                    isPreviousLetterPlaced = true;
                    ChoseWordDirection(_Tile, _Letter);
                    Debug.Log("First Tile placed");
                }
            }
        }
    }

    private void CheckLine(Tile pTile, Letter pLetter)
    {
        if (wordDirection == WordDirection.Horizontal && firstLetterPos.x == pTile.transform.position.x)
        {
            if (firstLetterPos.x + 1 == pTile.transform.position.x || firstLetterPos.x - 1 == pTile.transform.position.x)
            {

            }
        }
        else if (wordDirection == WordDirection.Vertical && firstLetterPos.y == pTile.transform.position.y)
        {
            if (firstLetterPos.y + 1 == pTile.transform.position.y || firstLetterPos.y - 1 == pTile.transform.position.y)
            {

            }
        }
        else return;
        _Letter.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z - .1f);
        _Letter.isPicked = false;
        _Tile.PlaceLetter(_Letter.letter, _Letter._Score);
        placedLetters.Add(pLetter.transform.position, pLetter._Score);
        hasPicked = false;
        positionList.Add(_Letter.transform.position);
        Debug.Log("CheckedLine");
    }

    private void ChoseWordDirection(Tile pTile, Letter pLetter)
    {
        if(firstLetterPos.x == pTile.transform.position.x)
        {
            wordDirection = WordDirection.Vertical;
        }
        else if (firstLetterPos.y == pTile.transform.position.y)
        {
            wordDirection = WordDirection.Horizontal;
        }
    }
}
