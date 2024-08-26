using System.Collections;
using System.Collections.Generic;
using Dtawan;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Dot : MonoBehaviour {

    [Header("Board Variables")]
    private int column;
    private int row;
    [SerializeField] private int previousColumn;
    [SerializeField] private int previousRow;
    [SerializeField] private int targetX;
    [SerializeField] private int targetY;
    private bool isMatched = false;
    [SerializeField] private NameTypeBar _nameTypeBar = NameTypeBar.HomeSolution;
    [SerializeField] private FindMatches findMatches;
    [SerializeField] private Board board;
    private GameObject otherDot;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    [Header("Swipe Stuff")]
    private float swipeAngle = 0;
    [SerializeField] private float swipeResist = 1f;

    [Header("Powerup Stuff")]
    private bool isColorBomb;
    private bool isColumnBomb;
    private bool isRowBomb;
    [SerializeField] private GameObject rowArrow;
    [SerializeField] private GameObject columnArrow;
    [SerializeField] private GameObject colorBomb;

    public Board Board { get => board; set => board = value; }
    public FindMatches FindMatches { get => findMatches; set => findMatches = value; }
    public int Column { get => column; set => column = value; }
    public int Row { get => row; set => row = value; }
    public bool IsMatched { get => isMatched; set => isMatched = value; }
    public GameObject OtherDot { get => otherDot; set => otherDot = value; }
    public float SwipeAngle { get => swipeAngle; set => swipeAngle = value; }
    public bool IsColorBomb { get => isColorBomb; set => isColorBomb = value; }
    public bool IsColumnBomb { get => isColumnBomb; set => isColumnBomb = value; }
    public bool IsRowBomb { get => isRowBomb; set => isRowBomb = value; }
    public NameTypeBar NameTypeBar { get => _nameTypeBar; set => _nameTypeBar = value; }

    // Use this for initialization
    private void Start () 
    {
        IsColumnBomb = false;
        IsRowBomb = false;
        IsColorBomb = false;
    }

    private void Update () 
    {
        targetX = Column;
        targetY = Row;

        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //Move Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);

            if(Board.AllDots[Column, Row] != this.gameObject)
            {
                Board.AllDots[Column, Row] = this.gameObject;
            }

            FindMatches.FindAllMatches();
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;

        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);

            if (Board.AllDots[Column, Row] != this.gameObject)
            {
                Board.AllDots[Column, Row] = this.gameObject;
            }

            FindMatches.FindAllMatches();
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
	}

    private IEnumerator CheckMoveCo()
    {
        if(IsColorBomb)
        {
            //This piece is a color bomb, and the other piece is the color to destroy
            FindMatches.MatchPiecesOfColor(OtherDot.GetComponent<Dot>().NameTypeBar);
            // FindMatches.MatchPiecesOfColor(OtherDot.tag);
            IsMatched = true;
        }
        else if(OtherDot.GetComponent<Dot>().IsColorBomb)
        {
            //The other piece is a color bomb, and this piece has the color to destroy
            FindMatches.MatchPiecesOfColor(OtherDot.GetComponent<Dot>().NameTypeBar);
            // FindMatches.MatchPiecesOfColor(this.gameObject.tag);
            OtherDot.GetComponent<Dot>().IsMatched = true;
        }
        yield return new WaitForSeconds(.5f);
        if(OtherDot != null)
        {
            if(!IsMatched && !OtherDot.GetComponent<Dot>().IsMatched)
            {
                OtherDot.GetComponent<Dot>().Row = Row;
                OtherDot.GetComponent<Dot>().Column = Column;
                Row = previousRow;
                Column = previousColumn;
                yield return new WaitForSeconds(.5f);
                Board.CurrentDot = null;
                Board.CurrentState = GameState.move;
            }
            else
            {
                Board.DestroyMatches();
            }
        }
    }

    private void OnMouseDown()
    {
        if (Board.CurrentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (Board.CurrentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    private void CalculateAngle(){
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            Board.CurrentState = GameState.wait;
            SwipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();

            Board.CurrentDot = this;

        }else{
            Board.CurrentState = GameState.move;

        }
    }

    private void MovePiecesActual(Vector2 direction)
    {
        OtherDot = Board.AllDots[Column + (int)direction.x, Row + (int)direction.y];
        previousRow = Row;
        previousColumn = Column;
		if (OtherDot != null)
		{
			OtherDot.GetComponent<Dot>().Column += -1 * (int)direction.x;
			OtherDot.GetComponent<Dot>().Row += -1 * (int)direction.y;
			Column += (int)direction.x;
			Row += (int)direction.y;
			StartCoroutine(CheckMoveCo());
		}
        else
        {
			Board.CurrentState = GameState.move;
		}
    }

    private void MovePieces()
    {
        if (SwipeAngle > -45 && SwipeAngle <= 45 && Column < Board.Width - 1)
        {
           //Right Swipe
            MovePiecesActual(Vector2.right);
        }
        else if (SwipeAngle > 45 && SwipeAngle <= 135 && Row < Board.Height - 1)
        {
            //Up Swipe
            MovePiecesActual(Vector2.up);
        }
        else if ((SwipeAngle > 135 || SwipeAngle <= -135) && Column > 0)
        {
            //Left Swipe
            MovePiecesActual(Vector2.left);
        }
        else if (SwipeAngle < -45 && SwipeAngle >= -135 && Row > 0)
        {
            //Down Swipe
            MovePiecesActual(Vector2.down);
        }
        else
        {
            Board.CurrentState = GameState.move;
        }
    }

    public void MakeRowBomb()
    {
        IsRowBomb = true;
        GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColumnBomb()
    {
        IsColumnBomb = true;
        GameObject arrow = Instantiate(columnArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColorBomb()
    {
        IsColorBomb = true;
        GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
        color.transform.parent = this.transform;
    }
}