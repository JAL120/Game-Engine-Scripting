using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Battleship
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int[,] grid = new int[,]
        {
            { 1,1,0,0,1 },
            { 0,0,0,0,0 },
            { 0,0,1,0,1 },
            { 1,0,1,0,0 },
            { 1,0,1,0,1 },
        };

        private bool[,] hits;

        private int nRows;
        private int nCols;

        private int row;
        private int col;

        private int score;

        private int time;
        

        [SerializeField] Transform gridRoot;
        [SerializeField] GameObject cellPrefab;
        [SerializeField] GameObject winLabel;
        [SerializeField] TextMeshProUGUI timeLabel;
        [SerializeField] TextMeshProUGUI scoreLabel;

        private void Awake()
        {
            nRows = grid.GetLength(0);
            nCols = grid.GetLength(1);
            hits = new bool[nRows, nCols];

            InstantiateCells();

            SelectCurrentCell();
            InvokeRepeating("IncrementTime", 1f, 1f);

        }

        public void InstantiateCells()
        {
            for (int i = 0; i < nRows * nCols; i++)
            {
                GameObject tempcell = Instantiate(cellPrefab, gridRoot);
                //cells.Add(tempcell);
            }
        }

        Transform GetCurrentCell()
        {
            int index = (row * nCols) + col;
            return gridRoot.GetChild(index);
        }

        void SelectCurrentCell()
        {
            Transform cell = GetCurrentCell();
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(true);
        }

        void UnselectCurrentCell()
        {
            Transform cell = GetCurrentCell();
            Transform cursor = cell.Find("Cursor");
            cursor.gameObject.SetActive(false);
        }

        public void MoveHorizontal(int amt)
        {
           UnselectCurrentCell();
            col += amt; 
            col = Mathf.Clamp(col, 0, nCols -1);
            SelectCurrentCell();
        }

        public void MoveVertical(int amt)
        {
            UnselectCurrentCell();
            row += amt;
            row = Mathf.Clamp(row, 0, nRows - 1);
            SelectCurrentCell();
        }

        void ShowHit()
        {
            Transform cell = GetCurrentCell();
            Transform hit = cell.Find("Hit");
            hit.gameObject.SetActive(true);
        }

        void ShowMiss()
        {
            Transform cell = GetCurrentCell();
            Transform miss = cell.Find("Miss");
            miss.gameObject.SetActive(true);
        }

        void IncrementScore()
        {
            score++;
            scoreLabel.text = string.Format("Score: {0}", score);
        }

        public void Fire()
        {
            if (hits[row, col]) return;
            hits[row, col] = true;
            if (grid[row, col] == 1)
            {
                ShowHit();
                IncrementScore();
            }
            else
            {
                ShowMiss();
            }

            void TryEndGame()
            {
                for (int row = 0; row < nRows; row++)
                {
                    if (grid[row, col] == 0) continue;
                    if (hits[row, col] == false) return;
                }
            }
            winLabel.SetActive(true);
            CancelInvoke("IncrementTime");
        }

        void IncrementTime()
        {
            time++;
            timeLabel.text = string.Format("{0}:{1}", time/60, (time%60).ToString("00"));
        }

        public List<GameObject> cells;
        public int timerduration = 60;
        public void Restart()
        {
            time = timerduration;
            score = 0;
            ResetHitData();
            UnselectCurrentCell();
            row = 0;
            col = 0;
            ResetCellObjects();
            RandomizeGrid();
            SelectInitialCell();
        }

        private void ResetHitData()
        {

            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    hits[i, j] = false;
                }
            }
        }


        public void ResetCellObjects()
        {
            GameObject[] HitObjects = GameObject.FindGameObjectsWithTag("Hit");
            GameObject[] MissObjects = GameObject.FindGameObjectsWithTag("Miss");
            foreach (GameObject Hit in HitObjects)
            {
                Hit.SetActive(false);
            }
            foreach (GameObject Miss in MissObjects)
            {
                Miss.SetActive(false);
            }
        }

        private void RandomizeGrid()
        {
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    // Randomly assign 0 or 1 to each cell
                    grid[i, j] = Random.Range(0, 11) > 5 ? 1 : 0;
                }
            }
        }

        private void SelectInitialCell()
        {
            row = 0;
            col = 0;
            SelectCurrentCell();
        }
    }

}