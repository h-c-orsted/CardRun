using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public int[,] cardsArray = new int[4, 14];

    public int[] cardsLeftArray = new int[52];
    int cardsLeft = 52;

    /* 
     * 0 = not in game 
     * 1 = on path
     * 2 = collected
    */

    public void UpdateCardState(int color, int value, int state)
    {
        cardsArray[color, value] = state;
        Debug.Log(cardsArray[3, 1]);
    }


    private void Start()
    {
        // Fill cardsLeftArray with numbers representing cards
        for (int i=0; i<52; i++)
        {
            cardsLeftArray[i] = i + 1;
        }
    }
}
