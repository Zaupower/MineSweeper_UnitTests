# MineSweeper_UnitTests   
C# MineSweeper game UnitTests    

**Methodology**     
The tests made for each function where designed with the branching method where each branch represents a possible outcome from the method.     
     
**Methods description**  
     
**Open()**     
Receives: two ints, has coordinates in range of the field defined     
Returns: the GameState that can be Active, Lose or Win.    
    
**Branches:**     
     
- if (GameState != GameState.Active) throw InvalidOperationException.
- if (targetCell.IsOpen) return GameState; 
- if (targetCell.IsMine) GameState = GameState.Lose;
- else from all above: run between all neighbor cells and count their neighbor mines
  - if the current cell does not have mines as neighbors recursively open each neighbor.
- if (openCount + mineCount == totalCount) : GameState = GameState.Win;

**GetCurrentField()**     
Receives: void       
Returns: PointState[,] 2d matrix with the state of every cell    
Branches:        
- if (!targetCell.IsOpen && GameState == GameState.Active) : publicFieldInfo[row, column] = PointState.Close;
- else if (targetCell.IsMine)if (targetCell.IsMine) GameState = GameState.Lose;
- else publicFieldInfo[row, column] = (PointState)targetCell.MineNeighborsCount;

**GameProcessor Constructor**    
Receives: bool[,] boolField    
Returns: void     
Branches:     
- Populate Ponit[,] _field;
- define total count as row*column
- define mineCount = mineCount + (isMine ? 1: 0);

**Setup VS for unit tests with Nunit**     		
Intall nugget packages:     		
- NUnit     		
- NUnit3TestAdapter		
- Microsoft.NET.Test.Sdk		


