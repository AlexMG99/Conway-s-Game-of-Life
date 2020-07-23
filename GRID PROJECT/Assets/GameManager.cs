using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CreateGrid createGrid;

    public List<CellClass> cellGrid;
    public List<GameObject> tileGrid;

    public bool play = false;

    public Text multiplierText;

    private float timeLeft = 1.0f;
    public float timeIteration = 1.0f;
    public float timeMultiplier = 1.0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        cellGrid = new List<CellClass>();
        createGrid = GameObject.Find("GridManager").GetComponent<CreateGrid>();
    }

    // Start is called before the first frame update
    void Start()
    {
        multiplierText.text = "x" + timeMultiplier.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(play)
            ActualizeCellUpdate();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ActualizeCells();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (timeMultiplier != 8)
                timeMultiplier *= 2;
            else
                timeMultiplier = 0.25f;

            multiplierText.text = "x" + timeMultiplier.ToString();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if(hit.collider != null && hit.collider.tag == "Cell")
            {
                hit.collider.gameObject.GetComponent<CellClass>().ChangeState();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

    }

    void ActualizeCellUpdate()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            ActualizeCells();
            timeLeft = timeIteration * (1 / timeMultiplier);
        }
    }

    void ActualizeCells()
    {
        for (int i = 0; i < cellGrid.Count; i++)
        {
            cellGrid[i].CheckNeighbours();
        }

        for (int i = 0; i < cellGrid.Count; i++)
        {
            cellGrid[i].TurnOnNeighbours();
        }
    }

    public void ClearGrid()
    {
        for (int i = 0; i < cellGrid.Count; i++)
        {
            cellGrid[i].TurnOff();
        }
    }

    public void RecalculateGrid()
    {
        for (int i = 0; i < cellGrid.Count; i++)
        {
            cellGrid[i].RebootCell();
        }
    }

    public void ResetGrid()
    {
        for (int i = 0; i < cellGrid.Count; i++)
        {
            Destroy(cellGrid[i].gameObject);
        }
        cellGrid.Clear();

        for (int i = 0; i < tileGrid.Count; i++)
        {
            Destroy(tileGrid[i]);
        }
        tileGrid.Clear();

        createGrid.CreateGridFunction();
    }
}
