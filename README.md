# MineSweeper_UnitTests   
C# MineSweeper game UnitTests    

**Methodology**     
The tests made for each function where designed with the branching method where each branch represents a possible outcome from the method.     
     
**Methods description**  
     
**Open()**     
Receives two ints, has coordinates in range of the field defined
Returns the GameState that can be Active, Lose or Win.    
    
**Branches:**     
     
- if (GameState != GameState.Active) throw InvalidOperationException. DONE
- if (targetCell.IsOpen) return GameState; DONE
- if (targetCell.IsMine) GameState = GameState.Lose; DONE
- else from all above: run between all neighbor cells and count their neighbor mines
  - if the current cell does not have mines as neighbors recursively open each neighbor. DONE
- if (openCount + mineCount == totalCount) : GameState = GameState.Win;

**GetCurrentField()**     
Receives void
E possible testar cada posicao ao percorrer o field bool criado com o final PointState(x,y) is mine
Returns PointState[,] 2d matrix with the state of every cell 
Branches: 
- if (!targetCell.IsOpen && GameState == GameState.Active) : publicFieldInfo[row, column] = PointState.Close;
- else if (targetCell.IsMine)if (targetCell.IsMine) GameState = GameState.Lose;
- else publicFieldInfo[row, column] = (PointState)targetCell.MineNeighborsCount;


