using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGrid : MonoBehaviour
{
    public int row = 10;
    public int col = 10;
    [SerializeField]
    private int tileSize = 1;

    // Start is called before the first frame update
    void Start()
    {
        CreateGridFunction();
    }

    public void CreateGridFunction()
    {
        GameObject referenceTile = GameObject.Instantiate(Resources.Load<GameObject>("Grid"));
        GameObject referenceCell = GameObject.Instantiate(Resources.Load<GameObject>("Cell"));

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                int rand = Random.Range(0, 2);
                GameObject tile = GameObject.Instantiate(referenceTile, transform);
                GameObject cell = GameObject.Instantiate(referenceCell, transform);
                CellClass cellClass = cell.GetComponent<CellClass>();
                GameManager.Instance.cellGrid.Add(cellClass);
                GameManager.Instance.tileGrid.Add(tile);
                cellClass.InitializeCell(r, c, row, col);

                float posX = r * tileSize;
                float posY = c * -tileSize;

                tile.transform.position = new Vector3(posX, posY, 0);
                cell.transform.position = new Vector3(posX, posY, -1);
            }
        }

        float gridW = col * tileSize;
        float gridH = row * tileSize;

        transform.position = -new Vector2(gridW / 2 + tileSize / 2, -gridH / 2 - tileSize / 2);

        SetCellNeighbours();

        Destroy(referenceCell);
        Destroy(referenceTile);
    }

    void SetCellNeighbours()
    {
        for(int i = 0; i < GameManager.Instance.cellGrid.Count; i++)
        {
            GameManager.Instance.cellGrid[i].AddNeighbours(GameManager.Instance.cellGrid, row, col);
            Debug.Log(i);
        }
    }
}
