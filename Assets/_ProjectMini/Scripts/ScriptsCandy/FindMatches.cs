using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Dtawan;
public class FindMatches : MonoBehaviour {

    [SerializeField] private Board board;
    [SerializeField] private List<GameObject> currentMatches = new List<GameObject>();

    public List<GameObject> CurrentMatches { get => currentMatches; set => currentMatches = value; }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private List<GameObject> IsRowBomb(Dot dot1, Dot dot2, Dot dot3)
    {

        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.IsRowBomb)
        {
            CurrentMatches.Union(GetRowPieces(dot1.Row));
        }

        if (dot2.IsRowBomb)
        {
            CurrentMatches.Union(GetRowPieces(dot2.Row));
        }

        if (dot3.IsRowBomb)
        {
            CurrentMatches.Union(GetRowPieces(dot3.Row));
        }
        return currentDots;
    }

    private List<GameObject> IsColumnBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.IsColumnBomb)
        {
            CurrentMatches.Union(GetColumnPieces(dot1.Column));
        }

        if (dot2.IsColumnBomb)
        {
            CurrentMatches.Union(GetColumnPieces(dot2.Column));
        }

        if (dot3.IsColumnBomb)
        {
            CurrentMatches.Union(GetColumnPieces(dot3.Column));
        }
        return currentDots;
    }

    private void AddToListAndMatch(GameObject dot)
    {
        if (!CurrentMatches.Contains(dot))
        {
            CurrentMatches.Add(dot);
        }
        dot.GetComponent<Dot>().IsMatched = true;
    }

    private void GetNearbyPieces(GameObject dot1, GameObject dot2, GameObject dot3)
    {
        AddToListAndMatch(dot1);
        AddToListAndMatch(dot2);
        AddToListAndMatch(dot3);
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.Width; i ++)
        {
            for (int j = 0; j < board.Height; j ++)
            {
                GameObject currentDot = board.AllDots[i, j];
                currentDot?.GetComponent<Dot>();
                if(currentDot != null)
                {
                    Dot currentDotDot = currentDot.GetComponent<Dot>();

                    if(i > 0 && i < board.Width - 1)
                    {
                        GameObject leftDot = board.AllDots[i - 1, j];
                        GameObject rightDot = board.AllDots[i + 1, j];

                        if (leftDot != null && rightDot != null)
                        {
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            if (leftDot.GetComponent<Dot>().NameTypeBar == currentDot.GetComponent<Dot>().NameTypeBar 
                            && rightDot.GetComponent<Dot>().NameTypeBar == currentDot.GetComponent<Dot>().NameTypeBar)
                            {
                                
                                CurrentMatches.Union(IsRowBomb(leftDotDot, currentDotDot, rightDotDot));

                                CurrentMatches.Union(IsColumnBomb(leftDotDot, currentDotDot, rightDotDot));

                                GetNearbyPieces(leftDot, currentDot, rightDot);
                            }

                            // if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            // {
                            //     CurrentMatches.Union(IsRowBomb(leftDotDot, currentDotDot, rightDotDot));

                            //     CurrentMatches.Union(IsColumnBomb(leftDotDot, currentDotDot, rightDotDot));

                            //     GetNearbyPieces(leftDot, currentDot, rightDot);
                            // }
                        }
                    }

                    if (j > 0 && j < board.Height - 1)
                    {
                        GameObject upDot = board.AllDots[i, j + 1];

                        GameObject downDot = board.AllDots[i, j - 1];

                        if (upDot != null && downDot != null)
                        {
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            Dot upDotDot = upDot.GetComponent<Dot>();

                            if (upDot.GetComponent<Dot>().NameTypeBar == currentDot.GetComponent<Dot>().NameTypeBar 
                            && downDot.GetComponent<Dot>().NameTypeBar == currentDot.GetComponent<Dot>().NameTypeBar)
                            {
                                CurrentMatches.Union(IsColumnBomb(upDotDot, currentDotDot, downDotDot));

                                CurrentMatches.Union(IsRowBomb(upDotDot, currentDotDot, downDotDot));

                                GetNearbyPieces(upDot, currentDot, downDot);
                            }
                            // if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            // {
                            //     CurrentMatches.Union(IsColumnBomb(upDotDot, currentDotDot, downDotDot));

                            //     CurrentMatches.Union(IsRowBomb(upDotDot, currentDotDot, downDotDot));

                            //     GetNearbyPieces(upDot, currentDot, downDot);
                            // }
                        }
                    }
                }
            }
        }
    }

    public void MatchPiecesOfColor(NameTypeBar color)
    {
        for (int i = 0; i < board.Width; i ++)
        {
            for (int j = 0; j < board.Height; j ++)
            {
                //Check if that piece exists
                if(board.AllDots[i, j] != null)
                {
                    //Check the tag on that dot
                    if(board.AllDots[i, j].GetComponent<Dot>().NameTypeBar == color)
                    {
                        //Set that dot to be matched
                        board.AllDots[i, j].GetComponent<Dot>().IsMatched = true;
                        Debug.Log("True");
                    }
                    // if(board.AllDots[i, j].tag == color)
                    // {
                    //     //Set that dot to be matched
                    //     board.AllDots[i, j].GetComponent<Dot>().IsMatched = true;
                    // }
                }
            }
        }
    }

    private List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.Height; i ++){
            if(board.AllDots[column, i]!= null){
                dots.Add(board.AllDots[column, i]);
                board.AllDots[column, i].GetComponent<Dot>().IsMatched = true;
            }
        }
        return dots;
    }

    private List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.Width; i++)
        {
            if (board.AllDots[i, row] != null)
            {
                dots.Add(board.AllDots[i, row]);
                board.AllDots[i, row].GetComponent<Dot>().IsMatched = true;
            }
        }
        return dots;
    }

    public void CheckBombs()
    {
        //Did the player move something?
        if(board.CurrentDot != null){
            //Is the piece they moved matched?
            if (board.CurrentDot.IsMatched)
            {
                //make it unmatched
                board.CurrentDot.IsMatched = false;
                //Decide what kind of bomb to make
                if((board.CurrentDot.SwipeAngle > -45 && board.CurrentDot.SwipeAngle <= 45) ||(board.CurrentDot.SwipeAngle < -135 || board.CurrentDot.SwipeAngle >= 135))
                {
                    board.CurrentDot.MakeRowBomb();
                }else{
                    board.CurrentDot.MakeColumnBomb();
                }
            }
            //Is the other piece matched?
            else if(board.CurrentDot.OtherDot != null)
            {
                Dot otherDot = board.CurrentDot.OtherDot.GetComponent<Dot>();
                //Is the other Dot matched?
                if(otherDot.IsMatched){
                    //Make it unmatched
                    otherDot.IsMatched = false;
                    if ((board.CurrentDot.SwipeAngle > -45 && board.CurrentDot.SwipeAngle <= 45) || (board.CurrentDot.SwipeAngle < -135 || board.CurrentDot.SwipeAngle >= 135))
                    {
                        otherDot.MakeRowBomb();
                    }
                    else
                    {
                        otherDot.MakeColumnBomb();
                    }
                }
            }
        }
    }
}
