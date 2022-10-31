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
    internal class GameProcessorConstructorTests
    {

        private GameSettings? _settings;
        private bool[,]? _field;
        private Minesweeper.Core.GameProcessor? _gameProcessor;
        private PointState[,] _currentField;


        //Beginner
        [SetUp]
        public void SetUp()
        {
            //Setup before every test
            _settings = DifficultyManager.GetGameSettingsByDifficultylevel(DifficultyLevel.Beginner);

            _field = FieldGenerator.GetRandomField(_settings.Height, _settings.Width, _settings.Mines);

            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);

            _currentField = _gameProcessor.GetCurrentField();
        }

        [TearDown]
        public void Postcondition()
        {
            _settings = DifficultyManager.GetGameSettingsByDifficultylevel(DifficultyLevel.Beginner);

            _field = FieldGenerator.GetRandomField(_settings.Height, _settings.Width, _settings.Mines);

            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);

            _currentField = _gameProcessor.GetCurrentField();
        }


        //Falta verificar s etem o numero de minas setado 
        //TEM DE SER NULCEAR OS TESTES
        //Enviar matrix com x numeros de bombas e chamar GetCurrentField() e verificar o numero de bombas?

        //Verify if field generated is the same size has requested
        [Test]
        public void GameProcessor_FieldSize_Equal(
            [Values(5, 10, 15)] int rowLength,
            [Values(15, 10, 5)] int columnLength,
            [Values(5, 10, 25)] int mines)
        {  
            _field = FieldGenerator.GetRandomField(rowLength, columnLength,mines);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);

            _currentField = _gameProcessor.GetCurrentField();

            int currentFieldRowLength = _currentField.GetLength(1);
            int currentFieldColumnLength = _currentField.GetLength(0);

            Assert.AreEqual(currentFieldRowLength, rowLength, $"Row size expected");
            Assert.AreEqual(currentFieldColumnLength, columnLength, $"Column size expected");
        }

        [Test]
        public void GameProcessor_NumberOfMines_EqualHasSeted(
            [Values(15)] int rowLength,
            [Values(14)] int columnLength,
            [Values(50)] int mines)
        {
            _field = FieldGenerator.GetRandomField(columnLength , rowLength, mines);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);

            _currentField = _gameProcessor.GetCurrentField();

            GameState gamestate = new GameState();

            //Run the game until win/lose to expose the mines position
            for (int row = 0; row < rowLength; row++)
            {
                for (int column = 0; column < columnLength ; column++)
                {
                    gamestate = _gameProcessor.Open(row, column);
                    if (gamestate != GameState.Active)
                    {
                        int numberOfMines = getNumberOfMines(_gameProcessor.GetCurrentField(), rowLength, columnLength);
                        Assert.AreEqual(numberOfMines, mines, $"mines size expected");
                        break;
                    }
                }
                if (gamestate != GameState.Active)
                {
                    break;
                }
            }
        }
        
        private int getNumberOfMines(PointState[,] pointStates, int rowLength, int columnLength)
        {
            int minesCounter = 0;

            for (int row = 0; row < rowLength; row++)
            {
                for (int column = 0; column < columnLength; column++)
                {
                    PointState pointState = pointStates[row, column];
                    if (pointState == PointState.Mine)
                    {
                       minesCounter++;
                    }
                }
            }
            return minesCounter;
        }

    }
}
