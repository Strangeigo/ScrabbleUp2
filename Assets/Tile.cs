using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tileType
{
    Simple,
    MultiplyWord,
    MultiplyLetter
}
public class Tile : MonoBehaviour
{
    public bool isOccupied = false;
    public tileType tileType;
    [SerializeField] public float value = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
