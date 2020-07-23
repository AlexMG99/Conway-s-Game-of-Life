using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellClass : MonoBehaviour
{
    public Sprite OffCellSprite;
    public Sprite OnCellSprite;

    enum Location
    {
        NONE = -1,
        TOP_LEFT_CORNER,
        TOP_RIGHT_CORNER,
        BOTTOM_LEFT_CORNER,
        BOTTOM_RIGHT_CORNER,
        LEFT_EDGE,
        RIGHT_EDGE,
        TOP_EDGE,
        DOWN_EDGE,
        MIDDLE
    }

    Vector2 position;
    List<CellClass> neighbours;
    SpriteRenderer spriteRenderer;
    Location location = Location.NONE;
    public bool turnOn = false;
    bool toTurnOn { get; set; }

    private void Awake()
    {
        neighbours = new List<CellClass>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCell(int x, int y, int cols, int rows)
    {
        // Set Grid Position
        position = new Vector2(x, y);

        // Set Location
        if (x == 0 && y == 0)
            location = Location.TOP_LEFT_CORNER;
        else if (x == 0 && y == rows - 1)
            location = Location.TOP_RIGHT_CORNER;
        else if (x == cols - 1 && y == 0)
            location = Location.BOTTOM_LEFT_CORNER;
        else if (x == cols - 1 && y == rows - 1)
            location = Location.BOTTOM_RIGHT_CORNER;
        else if (y == 0)
            location = Location.LEFT_EDGE;
        else if (y == rows - 1)
            location = Location.RIGHT_EDGE;
        else if (x == 0)
            location = Location.TOP_EDGE;
        else if (x == cols - 1)
            location = Location.DOWN_EDGE;
        else
            location = Location.MIDDLE;

        // Set on/off
        int rand = Random.Range(0, 2);
        if (rand == 0)
            TurnOn();
        else
            TurnOff();
            
    }

    public void CheckNeighbours()
    {
        int aliveNeighbours = 0;
        for(int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].turnOn)
                aliveNeighbours++;
        }

        if(turnOn && (aliveNeighbours < 2 || aliveNeighbours > 3))
            toTurnOn = false;    
        else if(!turnOn && aliveNeighbours == 3)
            toTurnOn = true;
            
    }

    public void TurnOnNeighbours()
    {
        if (toTurnOn)
            TurnOn();
        else
            TurnOff();
    }

    public void AddNeighbours(List<CellClass> cellList, int maxCol, int maxRow)
    {
        int currentTile = maxCol * (int)position.x + (int)position.y;

        int top = currentTile - maxCol;
        int bot = currentTile + maxCol;
        int left = currentTile - 1;
        int right = currentTile + 1;
        int topLeft = top - 1;
        int topRight = top + 1;
        int botLeft = bot - 1;
        int botRight = bot + 1;

        switch (location)
        {
            case Location.NONE:
                Debug.Log("No location enum, InitializeCell error!");
                break;
            case Location.TOP_LEFT_CORNER:
                neighbours.Add(cellList[right]);
                neighbours.Add(cellList[botRight]);
                neighbours.Add(cellList[bot]);
                break;
            case Location.TOP_RIGHT_CORNER:
                neighbours.Add(cellList[bot]);
                neighbours.Add(cellList[botLeft]);
                neighbours.Add(cellList[left]);
                break;
            case Location.BOTTOM_LEFT_CORNER:
                neighbours.Add(cellList[right]);
                neighbours.Add(cellList[top]);
                neighbours.Add(cellList[topRight]);
                break;
            case Location.BOTTOM_RIGHT_CORNER:
                neighbours.Add(cellList[left]);
                neighbours.Add(cellList[topLeft]);
                neighbours.Add(cellList[top]);
                break;
            case Location.LEFT_EDGE:
                neighbours.Add(cellList[right]);
                neighbours.Add(cellList[botRight]);
                neighbours.Add(cellList[bot]);
                neighbours.Add(cellList[top]);
                neighbours.Add(cellList[topRight]);
                break;
            case Location.RIGHT_EDGE:
                neighbours.Add(cellList[bot]);
                neighbours.Add(cellList[botLeft]);
                neighbours.Add(cellList[left]);
                neighbours.Add(cellList[topLeft]);
                neighbours.Add(cellList[top]);
                break;
            case Location.TOP_EDGE:
                neighbours.Add(cellList[right]);
                neighbours.Add(cellList[botRight]);
                neighbours.Add(cellList[bot]);
                neighbours.Add(cellList[botLeft]);
                neighbours.Add(cellList[left]);
                break;
            case Location.DOWN_EDGE:
                neighbours.Add(cellList[right]);
                neighbours.Add(cellList[left]);
                neighbours.Add(cellList[topLeft]);
                neighbours.Add(cellList[top]);
                neighbours.Add(cellList[topRight]);
                break;
            case Location.MIDDLE:
                neighbours.Add(cellList[right]);
                neighbours.Add(cellList[botRight]);
                neighbours.Add(cellList[bot]);
                neighbours.Add(cellList[botLeft]);
                neighbours.Add(cellList[left]);
                neighbours.Add(cellList[topLeft]);
                neighbours.Add(cellList[top]);
                neighbours.Add(cellList[topRight]);
                break;
            default:
                break;
        }
    }

    void TurnOn()
    {
        turnOn = true;
        toTurnOn = true;
        spriteRenderer.sprite = OnCellSprite;
    }

    public void TurnOff()
    {
        turnOn = false;
        toTurnOn = false;
        spriteRenderer.sprite = OffCellSprite;
    }

    public void ChangeState()
    {
        turnOn = !turnOn;
        toTurnOn = !toTurnOn;
        if (turnOn)
            TurnOn();
        else
            TurnOff();
    }

    public void RebootCell()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            TurnOn();
        else
            TurnOff();
    }
}

