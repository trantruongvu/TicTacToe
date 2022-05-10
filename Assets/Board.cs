using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    TicTacToe ttt;
    public Block prefabBlock;
    public List<Block> blocks;

    int lastIndexBlockCheck;
    int consecutiveBlockCount;

    private void Start()
    {
        ttt = TicTacToe.Instance;
    }

    // Create table 
    public void BuildTable()
    {
        // if already has table
        if (transform.childCount > 0)
        {
            // Clean old table
            foreach (Block block in blocks)
                block.Reset();

        }
        else
        {
            // Get size per block
            int blockSize = 1080 / ttt.blocksToBuild;
            for (int y = 0; y < ttt.blocksToBuild; y++)
            {
                for (int x = 0; x < ttt.blocksToBuild; x++)
                {
                    Block block = Instantiate(prefabBlock, transform);
                    block.image.rectTransform.sizeDelta = new Vector2(blockSize, blockSize);
                    block.image.rectTransform.anchoredPosition = new Vector2(blockSize * x, -blockSize * y);
                    blocks.Add(block);
                }
            }
        }

        ttt.turnCount = 0;
        ttt.isPlayer1Turn = true;
        ttt.currentPlayerCharacter = "x";
        ttt.txtResult.transform.parent.gameObject.SetActive(false);
    }

    // Check and return game result
    // [0] [1] [2]
    // [3] [4] [5]
    // [6] [7] [8]

    public bool CheckForWinner()
    {
        // Horizontal
        for (int i = 0; i <= 6; i += 3)
        {
            if (!CompareBlocks(i, i + 1)) // [0] [1]
                continue;
            if (!CompareBlocks(i, i + 2)) // [0] [2]
                continue;

            return true;
        }

        // Vertical
        for (int i = 0; i < 3; i++)
        {
            if (!CompareBlocks(i, i + 3)) // [0] [3]
                continue;
            if (!CompareBlocks(i, i + 6)) // [0] [6]
                continue;

            return true;
        }

        // Left Diagonal [0] [4] n [0] [8]
        if (CompareBlocks(0, 4) && CompareBlocks(0, 8))
            return true;

        // Right Diagonal [2] [4] && [2] [6]
        if (CompareBlocks(2, 4) && CompareBlocks(2, 6))
            return true;

        return false;

        #region Test
        /*
        #region Check Horizontal 

        // Get last index of block to check for Horizontal
        lastIndexBlockCheck = ttt.blocksToBuild * ttt.blocksToBuild - ttt.blocksToBuild;
        // 3 * 3 - 3 = 6 -> [0] [ ] [ ]
        //                  [3] [ ] [ ]
        //                  [6] [ ] [ ]

        //int row = 0;
        //for (int i = 0; i <= lastIndexBlockCheck; i += ttt.blocksToBuild)
        //{
        //    row++;
        //    consecutiveBlockCount = 0;
        //    for (int j = 0; j < ttt.blocksToBuild; j++)
        //    {
        //        // if block index goes out of blocks row amount
        //        if ((i + j) == (ttt.blocksToBuild * row - 1))
        //        {
        //            if (CompareBlocks(i + j, i + j - 1))
        //                consecutiveBlockCount++; // Count up if correct
        //        }
        //        else
        //        {
        //            if (CompareBlocks(i + j, i + j + 1))
        //                consecutiveBlockCount++; // Count up if correct
        //        }

        //        // Enough amounts of block from current Player to win
        //        if (consecutiveBlockCount >= ttt.blocksToWin)
        //            return true;
        //    }
        //}
        #endregion

        #region Vertical
        // Check Vertical

        // Get last index of block to check for Vertical
        lastIndexBlockCheck = ttt.blocksToBuild;
        // 3 -> [0] [1] [2] 
        //      [ ] [ ] [ ]
        //      [ ] [ ] [ ]

        //int column = 0;
        //for (int i = 0; i < lastIndexBlockCheck; i++)
        //{
        //    column++;
        //    consecutiveBlockCount = 0;
        //    for (int j = 0; j < ttt.blocksToBuild; j++)
        //    {
        //        int index1 = ttt.blocksToBuild * j + i;
        //        int index2 = ttt.blocksToBuild * j + ttt.blocksToBuild * column;
        //        //Debug.Log(index1);
        //        //Debug.Log(index2);
        //        //Debug.Log(ttt.blocksToBuild * ttt.blocksToBuild - ttt.blocksToBuild + i);

        //        if (index1 == ttt.blocksToBuild * ttt.blocksToBuild - ttt.blocksToBuild + i)
        //        {
        //            Debug.Log(index1);
        //            Debug.Log(index1 - ttt.blocksToBuild);
        //            //if (CompareBlocks(index1 - ttt.blocksToBuild, index1))
        //            //    consecutiveBlockCount++; // Count up if correct
        //        }
        //        else
        //        {
        //            Debug.Log(index1);
        //            Debug.Log(index2);
        //            //if (CompareBlocks(index1, index2))
        //            //    consecutiveBlockCount++; // Count up if correct
        //        }

        //        //Debug.Log(ttt.blocksToBuild * ttt.blocksToBuild - ttt.blocksToBuild * i);

        //        //if ((ttt.blocksToBuild * j + ttt.blocksToBuild * i) == (ttt.blocksToBuild * ttt.blocksToBuild - ttt.blocksToBuild * i))
        //        //{
        //        //    if (CompareBlocks(ttt.blocksToBuild * j + ttt.blocksToBuild * i, ttt.blocksToBuild * j + ttt.blocksToBuild * column - 2))
        //        //        consecutiveBlockCount++; // Count up if correct
        //        //}
        //        //else
        //        //{
        //        //    if (CompareBlocks(ttt.blocksToBuild * j + ttt.blocksToBuild * i, ttt.blocksToBuild * j + ttt.blocksToBuild * column))
        //        //        consecutiveBlockCount++; // Count up if correct
        //        //}

        //        // Enough amounts of block from current Player to win
        //        if (consecutiveBlockCount >= ttt.blocksToWin - 1)
        //            return true;
        //    }
        //    Debug.Log("----------");
        //}
        #endregion Vertical
        */
        #endregion Test
    }

    // Compare two values of blocks
    private bool CompareBlocks(int indexBlock1, int indexBlock2)
    {
        //Debug.Log("b " + indexBlock1 + " : " + blocks[indexBlock1].text.text);
        //Debug.Log("b " + indexBlock2 + " : " + blocks[indexBlock2].text.text);

        // Check empty
        if (string.IsNullOrEmpty(blocks[indexBlock1].text.text) || string.IsNullOrEmpty(blocks[indexBlock2].text.text))
            return false;

        // Check values
        if (blocks[indexBlock1].text.text.Equals(blocks[indexBlock2].text.text))
            return true;
        else
            return false;
    }
}
