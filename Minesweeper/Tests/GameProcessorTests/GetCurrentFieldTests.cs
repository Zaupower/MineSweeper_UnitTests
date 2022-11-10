using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using Minesweeper.Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.GameProcessor
{
    [TestFixture]
    internal class GetCurrentFieldTests
    {
        private GameSettings? _settings;
        private bool[,]? _field;
        private Minesweeper.Core.GameProcessor? _gameProcessor;
        private PointState[,]? _currentField;

        [Test]
        public void GetCurrentField_GameProcessorObject_Equal([Values(0, 1, 2)] int difficultyLevel)
        {            
            GameState actualGameState;
            int x=0, y = 0;

            _settings = DifficultyManager.GetGameSettingsByDifficultylevel((DifficultyLevel)difficultyLevel);
            _field = FieldGenerator.GetRandomField(_settings.Height, _settings.Width, _settings.Mines);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);
            actualGameState = _gameProcessor.Open(x, y);

            _currentField = _gameProcessor.GetCurrentField();

            var currentFieldCell = _currentField[x, y];

            switch (actualGameState)
            {
                case GameState.Active:
                    Assert.AreNotEqual(currentFieldCell, PointState.Mine, "Correct state of cell(not mine) opened");               
                    break;
                case GameState.Lose:
                    Assert.AreEqual(currentFieldCell, PointState.Mine, "Correct state of cell opened, mine");
                    break;
            }
            
            
        }

        [Test]
        public void GetCurrentField_RectangularField_Equal([Values(0, 1, 2)] int difficultyLevel, [Values(5, 10)] int rowLenght, [Values(10, 5)] int columnLenght)
        {
            GameState actualGameState;
            int x = 0, y = 0;

            _settings = DifficultyManager.GetGameSettingsByDifficultylevel((DifficultyLevel)difficultyLevel);
            _field = FieldGenerator.GetRandomField(rowLenght, columnLenght, _settings.Mines);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);
            actualGameState = _gameProcessor.Open(x, y);

            _currentField = _gameProcessor.GetCurrentField();

            var currentFieldCell = _currentField[x, y];

            switch (actualGameState)
            {
                case GameState.Active:
                    Assert.AreNotEqual(currentFieldCell, PointState.Mine, "Correct state of cell(not mine) opened");
                    break;
                case GameState.Lose:
                    Assert.AreEqual(currentFieldCell, PointState.Mine, "Correct state of cell opened, mine");
                    break;
                case GameState.Win:
                    for (int row = 0; row < rowLenght; row++)
                    {
                        for (int column = 0; column < columnLenght; column++)
                        {
                            Console.WriteLine(_currentField[row, column]);
                        }
                    }
                    break;
            }
        }


        [Test]
        public void GetCurrentField_RectangularFieldNoMines_Equal(
            [Values(0, 1, 2)] int difficultyLevel, 
            [Values(5, 10)] int rowLenght, 
            [Values(10, 5)] int columnLenght)
        {            
            GameState actualGameState;
            int x = 0, y = 0;


            _settings = DifficultyManager.GetGameSettingsByDifficultylevel((DifficultyLevel)difficultyLevel);
            _field = FieldGenerator.GetRandomField(rowLenght, columnLenght, _settings.Mines);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);
            actualGameState = _gameProcessor.Open(x, y);

            _currentField = _gameProcessor.GetCurrentField();

            var currentFieldCell = _currentField[x, y];

            switch (actualGameState)
            {
                case GameState.Active:
                    Assert.AreNotEqual(currentFieldCell, PointState.Mine, "Correct state of cell(not mine) opened");
                    break;
                case GameState.Lose:
                    Assert.AreEqual(currentFieldCell, PointState.Mine, "Correct state of cell opened, mine");
                    break;
                case GameState.Win:
                    for (int row = 0; row < rowLenght; row++)
                    {
                        for (int column = 0; column < columnLenght; column++)
                        {
                            Console.WriteLine(_currentField[row, column]);
                        }
                    }
                    break;
            }
        }
    }
}
