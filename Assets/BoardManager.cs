using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BoardData
{
    public List<string> board;
}

public class BoardManager : MonoBehaviour
{
    public GameObject classicTilePrefab;  // Assign in Inspector
    public GameObject doubleWordTilePrefab; // Assign in Inspector
    public GameObject doubleLetterTilePrefab; // Assign in Inspector
    public GameObject TripleLetterTilePrefab; // Assign in Inspector
    public GameObject TripleWordTilePrefab; // Assign in Inspector
    private GameObject _Tile;

    public int tileSize = 1;  // Size of each tile (adjust as needed)

    void Start()
    {
        LoadBoard();
    }

   void LoadBoard()
{
    TextAsset jsonFile = Resources.Load<TextAsset>("board");
    if (jsonFile == null)
    {
        Debug.LogError("Board JSON file not found!");
        return;
    }

    BoardData boardData = JsonUtility.FromJson<BoardData>(jsonFile.text);

    if (boardData == null || boardData.board == null)
    {
        Debug.LogError("Invalid board data!");
        return;
    }

    for (int y = 0; y < boardData.board.Count; y++)
    {
        string[] rowTiles = boardData.board[y].Split(','); // Split manually

        if (rowTiles.Length != 15) // Ensure correct row size
        {
            Debug.LogError($"Row {y} has {rowTiles.Length} elements, expected 15!");
            return;
        }

        for (int x = 0; x < rowTiles.Length; x++)
        {
            string tileType = rowTiles[x].Trim();
            Debug.Log($"Tile at ({x},{y}) = {tileType}"); // Debugging

            GameObject tilePrefab = GetTilePrefab(tileType);

            if (tilePrefab != null)
            {
               _Tile = Instantiate(tilePrefab);
               _Tile.transform.position = new Vector3(x * tileSize, -y * tileSize, 0);
            }
        }
    }
}

    GameObject GetTilePrefab(string type)
    {
        type = type.Trim(); // Ensure no extra spaces

        switch (type)
        {
            case "0": return classicTilePrefab;
            case "1": return doubleLetterTilePrefab;
            case "2": return TripleLetterTilePrefab;
            case "3": return doubleWordTilePrefab;
            case "4": return TripleWordTilePrefab;
            default: return null; // Ignore unknown types
        }
    }
}
