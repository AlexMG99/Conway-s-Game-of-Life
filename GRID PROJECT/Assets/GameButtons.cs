using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour
{
    public GameObject optionsMenu;

    public Text columnsText;
    public Text rowText;

    public Image playButton;

    public Sprite playSprite;
    public Sprite pauseSprite;

    private void Start()
    {
        columnsText.text = GameManager.Instance.createGrid.col.ToString();
        rowText.text = GameManager.Instance.createGrid.row.ToString();
    }

    void StartGame()
    {
        GameManager.Instance.play = true;
        playButton.sprite = pauseSprite;
    }

    void PauseGame()
    {
        GameManager.Instance.play = false;
        playButton.sprite = playSprite;
    }

    public void ClickPlayButton()
    {
        if (GameManager.Instance.play)
            PauseGame();
        else
            StartGame();
    }

    public void ClickDeleteButton()
    {
        GameManager.Instance.ClearGrid();
    }

    public void RecalculateGrid()
    {
        GameManager.Instance.RecalculateGrid();
    }

    public void ResetGrid()
    {
        GameManager.Instance.ResetGrid();
    }

    public void OpenSettingsButton()
    {
        optionsMenu.SetActive(true);
    }

    public void CloseSettingsButton()
    {
        optionsMenu.SetActive(false);
    }

    public void AddRows()
    {
        GameManager.Instance.createGrid.row++;
        rowText.text = GameManager.Instance.createGrid.row.ToString();
    }

    public void RemoveRows()
    {
        GameManager.Instance.createGrid.row--;
        rowText.text = GameManager.Instance.createGrid.row.ToString();
    }

    public void AddColumns()
    {
        GameManager.Instance.createGrid.col++;
        columnsText.text = GameManager.Instance.createGrid.col.ToString();
    }

    public void RemoveColumns()
    {
        GameManager.Instance.createGrid.col--;
        columnsText.text = GameManager.Instance.createGrid.col.ToString();
    }
}
