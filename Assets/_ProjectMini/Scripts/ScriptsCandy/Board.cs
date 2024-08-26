using System.Collections;
using System.Collections.Generic;
using Boss.Timer;
using Dtawan;
using UnityEngine;
using DG.Tweening;

public enum GameState{
    wait,
    move
}

public enum TileKind
{
	Breakable,
    Blank,
    Normal
}

[System.Serializable]
public class TileType{
	public int x;
	public int y;
	public TileKind tileKind;
}

public class Board : MonoBehaviour
{

	[SerializeField] private BarControl barControl;
    [SerializeField] private GameState currentState = GameState.move;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private int offSet;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject[] dots;
    [SerializeField] private GameObject destroyParticle;
	[SerializeField] private TileType[] boardLayout;
    [SerializeField] private GameObject[,] allDots;
    [SerializeField] private Dot currentDot;
    [SerializeField] private FindMatches findMatches;
    [SerializeField] private Timer _timer;
    [SerializeField] private GameObject keyBehavior;

    private bool[,] isBlankSpaces;
    private int streakValue = 1;
    public GameObject[,] AllDots { get => allDots; set => allDots = value; }
    public int Height { get => height; set => height = value; }
    public int Width { get => width; set => width = value; }
    public GameState CurrentState { get => currentState; set => currentState = value; }
    public Dot CurrentDot { get => currentDot; set => currentDot = value; }

    // private ScoreManagerCandy _scoreManager;

    // Use this for initialization
    public void StartCandy ()
	{
		// _scoreManager = FindObjectOfType<ScoreManagerCandy>();
		isBlankSpaces = new bool[Width, Height];
        AllDots = new GameObject[Width, Height];
        SetUp();
        _timer.StartTime();
	}

	private void GenerateBlankSpaces(){
		for (int i = 0; i < boardLayout.Length; i ++)
		{
			if(boardLayout[i].tileKind == TileKind.Blank)
			{
				isBlankSpaces[boardLayout[i].x, boardLayout[i].y] = true;
			}
		}
	}

    private void SetUp(){
		GenerateBlankSpaces();
        for (int i = 0; i < Width; i ++){
			for (int j = 0; j < Height; j++)
			{
				if (!isBlankSpaces[i, j])
				{
					Vector2 tempPosition = new Vector2(i, j + offSet);
					GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
					backgroundTile.transform.parent = this.transform;
					backgroundTile.name = "( " + i + ", " + j + " )";

					int dotToUse = Random.Range(0, dots.Length);

					int maxIterations = 0;

					while (MatchesAt(i, j, dots[dotToUse]) && maxIterations < 100)
					{
						dotToUse = Random.Range(0, dots.Length);
						maxIterations++;
						// Debug.Log(maxIterations);
					}
					maxIterations = 0;

					GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
					dot.GetComponent<Dot>().Row = j;
					dot.GetComponent<Dot>().Column = i;
                    dot.GetComponent<Dot>().Board = this.gameObject.GetComponent<Board>();
                    dot.GetComponent<Dot>().FindMatches = findMatches;
					dot.GetComponent<CheckDestroy>().BarControl = barControl; 
					dot.transform.parent = this.transform;
					dot.name = "( " + i + ", " + j + " )";
					AllDots[i, j] = dot;
					keyBehavior.SetActive(true);
				}
			}

        }
    }

    private bool MatchesAt(int column, int row, GameObject piece){
        if(column > 1 && row > 1){
			if (AllDots[column - 1, row] != null && AllDots[column - 2, row] != null)
			{
				if (AllDots[column - 1, row].GetComponent<Dot>().NameTypeBar == piece.GetComponent<Dot>().NameTypeBar 
                && AllDots[column - 2, row].GetComponent<Dot>().NameTypeBar == piece.GetComponent<Dot>().NameTypeBar)
				{
					return true;
				}
			}
			if (AllDots[column, row - 1] != null && AllDots[column, row - 2] != null)
			{
				if (AllDots[column, row - 1].GetComponent<Dot>().NameTypeBar == piece.GetComponent<Dot>().NameTypeBar 
                && AllDots[column, row - 2].GetComponent<Dot>().NameTypeBar == piece.GetComponent<Dot>().NameTypeBar)
				{
					return true;
				}
			}

        }else if(column <= 1 || row <= 1){
            if(row > 1){
				if (AllDots[column, row - 1] != null && AllDots[column, row - 2] != null)
				{
					if (AllDots[column, row - 1].GetComponent<Dot>().NameTypeBar == piece.GetComponent<Dot>().NameTypeBar 
                    && AllDots[column, row - 2].GetComponent<Dot>().NameTypeBar == piece.GetComponent<Dot>().NameTypeBar)
					{
						return true;
					}
				}
            }
            if (column > 1)
            {
				if (AllDots[column - 1, row] != null && AllDots[column - 2, row] != null)
				{
					if (AllDots[column - 1, row].GetComponent<Dot>().NameTypeBar == piece.GetComponent<Dot>().NameTypeBar 
                    && AllDots[column - 2, row].GetComponent<Dot>().NameTypeBar == piece.GetComponent<Dot>().NameTypeBar)
					{
						return true;
					}
				}
            }
        }

        return false;
    }

    private bool ColumnOrRow(){
        int numberHorizontal = 0;
        int numberVertical = 0;
        Dot firstPiece = findMatches.CurrentMatches[0].GetComponent<Dot>();
        if (firstPiece != null)
        {
            foreach (GameObject currentPiece in findMatches.CurrentMatches)
            {
                Dot dot = currentPiece.GetComponent<Dot>();
                if(dot.Row == firstPiece.Row){
                    numberHorizontal++;
                }
                if(dot.Column == firstPiece.Column){
                    numberVertical++;
                }
            }
        }
        return (numberVertical == 5 || numberHorizontal == 5);

    }

    private void CheckToMakeBombs(){
        if(findMatches.CurrentMatches.Count == 4 || findMatches.CurrentMatches.Count == 7){
            findMatches.CheckBombs();
        }
        if(findMatches.CurrentMatches.Count == 5 || findMatches.CurrentMatches.Count == 8){
            if(ColumnOrRow()){
                //Make a color bomb
                //is the current dot matched?
                if(CurrentDot != null){
                    if(CurrentDot.IsMatched){
                        if(!CurrentDot.IsColorBomb){
                            CurrentDot.IsMatched = false;
                            CurrentDot.MakeColorBomb();
                        }
                    }else{
                        if(CurrentDot.OtherDot != null){
                            Dot otherDot = CurrentDot.OtherDot.GetComponent<Dot>();
                            if(otherDot.IsMatched){
                                if(!otherDot.IsColorBomb){
                                    otherDot.IsMatched = false;
                                    otherDot.MakeColorBomb();
                                }
                            }
                        }
                    }
                }
            }/*else{
                //Make a adjacent bomb
                //is the current dot matched?
                if (currentDot != null)
                {
                    if (currentDot.isMatched)
                    {
                        if (!currentDot.isAdjacentBomb)
                        {
                            currentDot.isMatched = false;
                            currentDot.MakeAdjacentBomb();
                        }
                    }
                    else
                    {
                        if (currentDot.otherDot != null)
                        {
                            Dot otherDot = currentDot.otherDot.GetComponent<Dot>();
                            if (otherDot.isMatched)
                            {
                                if (!otherDot.isAdjacentBomb)
                                {
                                    otherDot.isMatched = false;
                                    otherDot.MakeAdjacentBomb();
                                }
                            }
                        }
                    }
                }
            }*/
        }
    }

    private void DestroyMatchesAt(int column, int row){
        if(AllDots[column, row].GetComponent<Dot>().IsMatched){
            //How many elements are in the matched pieces list from findmatches?
            if(findMatches.CurrentMatches.Count >= 4){
                CheckToMakeBombs();
            }

            GameObject particle = Instantiate(destroyParticle, 
                                              AllDots[column, row].transform.position, 
                                              Quaternion.identity);
            Destroy(particle, .5f);
            Destroy(AllDots[column, row]);
            // _scoreManager.increaseSocre(basePieceValue * streakValue);
            AllDots[column, row] = null;
        }
    }

    public void DestroyMatches(){
        for (int i = 0; i < Width; i ++){
            for (int j = 0; j < Height; j++){
                if (AllDots[i, j] != null){
                    
                    DestroyMatchesAt(i, j);
                }
            }
        }
        findMatches.CurrentMatches.Clear();
        StartCoroutine(DecreaseRowCo2());
    }

	private IEnumerator DecreaseRowCo2()
	{
		for (int i = 0; i < Width; i ++)
		{
			for (int j = 0; j < Height; j ++)
			{
				//if the current spot isn't blank and is empty. . . 
				if(!isBlankSpaces[i,j] && AllDots[i,j] == null)
				{
					//loop from the space above to the top of the column
					for (int k = j + 1; k < Height; k ++)
					{
						//if a dot is found. . .
						if(AllDots[i, k]!= null)
						{
							//move that dot to this empty space
							AllDots[i, k].GetComponent<Dot>().Row = j;
							//set that spot to be null
							AllDots[i, k] = null;
							//break out of the loop;
							break;
						}
					}
				}
			}
		}
		yield return new WaitForSeconds(.4f);
		StartCoroutine(FillBoardCo());
	}

    /*private IEnumerator DecreaseRowCo(){
        int nullCount = 0;
        for (int i = 0; i < width; i ++){
            for (int j = 0; j < height; j ++){
                if(allDots[i, j] == null){
                    nullCount++;
                }else if(nullCount > 0){
                    allDots[i, j].GetComponent<Dot>().row -= nullCount;
                    allDots[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());
    }*/

    private void RefillBoard(){
        for (int i = 0; i < Width; i ++){
            for (int j = 0; j < Height; j ++){
				if(AllDots[i, j] == null && !isBlankSpaces[i,j]){
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    int dotToUse = Random.Range(0, dots.Length);
                    GameObject piece = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                    AllDots[i, j] = piece;
                    piece.transform.DOScale(.8f, .5f).SetEase(Ease.InOutBack).From();
                    piece.GetComponent<Dot>().Row = j;
                    piece.GetComponent<Dot>().Column = i;
                    piece.GetComponent<CheckDestroy>().BarControl = barControl;
                    piece.GetComponent<Dot>().Board = this.gameObject.GetComponent<Board>();
                    piece.GetComponent<Dot>().FindMatches = findMatches;
				}
            }
        }
    }

    private bool MatchesOnBoard(){
        for (int i = 0; i < Width; i ++){
            for (int j = 0; j < Height; j ++){
                if(AllDots[i, j]!= null){
                    if(AllDots[i, j].GetComponent<Dot>().IsMatched){
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo(){
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while(MatchesOnBoard())
        {
	        streakValue++;
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
        findMatches.CurrentMatches.Clear();
        CurrentDot = null;
        yield return new WaitForSeconds(.5f);
        CurrentState = GameState.move;
        streakValue = 1;

    }



}
