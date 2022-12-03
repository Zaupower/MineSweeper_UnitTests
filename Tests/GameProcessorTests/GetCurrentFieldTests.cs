using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using Minesweeper.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.GameProcessorTests.Helper;

namespace Tests.GameProcessor
{
    [TestFixture]
    public class GetCurrentFieldTests
    {
        private GameSettings? _settings;
        private bool[,]? _field;
        private List<Tuple<int, int>> minesIndexs;
        private List<Tuple<int, int, PointState>> cellsToOpen;
        private List<Tuple<int, int>> cellsToWin;
        private List<Tuple<int, int>> cellsToLose;
        private Minesweeper.Core.GameProcessor? _gameProcessor;
        private PointState[,]? _currentField;
        private GenerateField genCostumField = new GenerateField();


        [SetUp]
        public void SetUp()
        {
            //Filed create to have cellsToOpen with all possible neighbors
            minesIndexs = new List<Tuple<int, int>> {
                Tuple.Create(0,1),
                Tuple.Create(0,2),
                Tuple.Create(0,3),
                Tuple.Create(0,4),
                Tuple.Create(0,5),

                Tuple.Create(1,1),
                Tuple.Create(1,3),

                Tuple.Create(2,0),
                Tuple.Create(2,1),
                Tuple.Create(2,2),
                Tuple.Create(2,3),
                Tuple.Create(2,4),
                Tuple.Create(2,5),

                Tuple.Create(3,1),
                Tuple.Create(3,3),
                Tuple.Create(3,5),

                Tuple.Create(4,1),
            };

            cellsToOpen = new List<Tuple<int, int, PointState>>
            {
                Tuple.Create(1,2, PointState.Neighbors8),
                Tuple.Create(1,4, PointState.Neighbors7),
                Tuple.Create(3,2, PointState.Neighbors6),
                Tuple.Create(3,4, PointState.Neighbors5),
                Tuple.Create(1,0, PointState.Neighbors4),
                Tuple.Create(4,2, PointState.Neighbors3),
                Tuple.Create(4,0, PointState.Neighbors2),
                Tuple.Create(5,0, PointState.Neighbors1),
                Tuple.Create(5,4, PointState.Neighbors0),
            };
            cellsToWin = new List<Tuple<int, int>>
            {
                Tuple.Create(1,2),
                Tuple.Create(1,4),
                Tuple.Create(3,2),
                Tuple.Create(3,4),
                Tuple.Create(1,0),
                Tuple.Create(4,2),
                Tuple.Create(4,0),
                Tuple.Create(5,0),
                Tuple.Create(5,4),

                Tuple.Create(0,0),
                Tuple.Create(1,5),
                Tuple.Create(3,0),
                Tuple.Create(4,3),
                Tuple.Create(4,4),
                Tuple.Create(4,5),
                Tuple.Create(5,1)
            };

            cellsToLose = new List<Tuple<int, int>>
            {
                Tuple.Create(0,1)
            };
        }
        [TearDown]
        public void Postcondition()
        {
        }


        [Test]//Verify if all cells are closed
        [TestCase(6, 6)]
        [TestCase(6, 7)]
        public void GetCurrentField_AllCellsAreClosed_BeforeFirstOpen(int rowLenght, int columnLenght)
        {
            _field = genCostumField.generateField(rowLenght, columnLenght, minesIndexs);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);

            _currentField = _gameProcessor.GetCurrentField();

            for (int i = 0; i < rowLenght; i++)
            {
                for (int j = 0; j < columnLenght; j++)
                {
                    var cellState = _currentField[i, j];
                    Assert.AreEqual(PointState.Close, cellState, "Correct state of cell close");
                }
            }
        }

        [Test]
        [TestCase(6, 6)]
        [TestCase(6, 7)]
        public void GetCurrentField_PointStateNeighbors_AllNeighborsAreCorrect(int rowLenght, int columnLenght)
        {
            //Precondition
            _field = genCostumField.generateField(rowLenght, columnLenght, minesIndexs);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);

            PointState currentPointState = PointState.Close;

                //Open Cells
            foreach (Tuple<int, int, PointState> cell in cellsToOpen)
            {
                _gameProcessor.Open(cell.Item2, cell.Item1);
            }
            _currentField = _gameProcessor.GetCurrentField();

            //Action
            foreach (Tuple<int, int, PointState> cell in cellsToOpen)
            {
                var cellState = _currentField[cell.Item1, cell.Item2];
                currentPointState = currentPointState.Previous();
                //Assert
                Assert.AreEqual(currentPointState, cellState, $"Correct state of cell {cell.Item2}, {cell.Item1}");
            }
        }

        [Test]//Verify if all neighbors mine count
        [TestCase(6, 6, false)]
        [TestCase(7, 6, true)]
        [TestCase(6, 6, true)]
        [TestCase(7, 6, false)]
        public void GetCurrentField_IndexOfMines_AfterWinAndLose(int rowLenght, int columnLenght, bool lose)
        {
            //Precondition
            _field = genCostumField.generateField(rowLenght, columnLenght, minesIndexs);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);
            List<Tuple<int, int>> cellsToOpen = lose ? cellsToLose : cellsToWin;
            GameState expectedGameState = lose ? GameState.Lose : GameState.Win;
            GameState resultGameState;
            foreach (Tuple<int, int> cell in cellsToOpen)
            {
                try
                {
                    _gameProcessor.Open(cell.Item2, cell.Item1);

                }catch(InvalidOperationException)//Prevent fail test by open cells affter win/lose
                {
                    Console.WriteLine(cell.Item2 + ", " + cell.Item1);
                }
            }

            resultGameState = _gameProcessor.GameState;
            _currentField = _gameProcessor.GetCurrentField();

            //Action
            foreach (Tuple<int, int> cell in minesIndexs)
            {
                var resultCellState = _currentField[cell.Item1, cell.Item2];

                //Assert
                Assert.AreEqual(PointState.Mine, resultCellState, $"Correct state of cell {cell.Item2}, {cell.Item1}");
            }

            Assert.AreEqual(expectedGameState, resultGameState);
        }
    }
}

