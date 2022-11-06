using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TicTacToe
{
    public class GameEndTextScript : MonoBehaviour
    {
        [SerializeField] private GameObject text;
        [SerializeField] private TBlocks tblocks;
        [SerializeField] private TextMeshPro endText;
        private int winner;
        // Start is called before the first frame update
        void Start()
        {
            text = GameObject.Find("GameEndText");
            tblocks = GameObject.Find("Blocks").GetComponent<TBlocks>();
            endText=text.GetComponent<TextMeshPro>();
            text.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            winner = tblocks.getWinner();
            if (winner == 1)
            {
                endText.text = "Player 1 Wins!";  
                text.SetActive(true);

            }else if (winner == 2)
            {
                endText.text = "Player 2 Wins!";
                text.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                text.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                text.SetActive(false);
            }
        }
    }

}
