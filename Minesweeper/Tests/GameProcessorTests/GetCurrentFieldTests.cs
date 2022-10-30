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


        //Verify if all publicFieldInfo.
        //Missing test all field is equaly returned (_field[x,y] == _currentField[x,y]
        [Test]
        public void GetCurrentField_GameProcessorObject_Equal([Values(0, 1, 2)] int difficultyLevel)
        {
            GameState expectedGameState;
            GameState actualGameState;
            int x=0, y = 0;


            _settings = DifficultyManager.GetGameSettingsByDifficultylevel((DifficultyLevel)difficultyLevel);
            _field = FieldGenerator.GetRandomField(_settings.Height, _settings.Width, _settings.Mines);
            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);
            actualGameState = _gameProcessor.Open(x, y);

            _currentField = _gameProcessor.GetCurrentField();

            switch (actualGameState)
            {
                case GameState.Active:
                    for (int row = 0; row < _settings.Height; row++)
                    {
                        for (int column = 0; column < _settings.Height; column++)
                        {
                            var fieldCell = _field[row, column];
                            var currentFieldCell = _currentField[row, column];
                            if (row == x && column == y)
                            {
                                Assert.AreNotEqual(currentFieldCell, PointState.Mine, "Correct state of cell(not mine) opened");
                            }
                            //else
                            //{
                            //    Assert.AreEqual(currentFieldCell, fieldCell? PointState.Close : PointState.Mine, "Same state has previous");
                            //}
                        }
                    }
                    break;
                case GameState.Lose:
                    for (int row = 0; row < _settings.Height; row++)
                    {
                        for (int column = 0; column < _settings.Height; column++)
                        {
                            var fieldCell = _field[row, column];
                            var currentFieldCell = _currentField[row, column];
                            if (row == x && column == y)
                            {
                                Assert.AreEqual(currentFieldCell, PointState.Mine, "Correct state of cell opened, mine");
                            }
                            //else
                            //{
                            //    Assert.AreEqual(currentFieldCell, fieldCell ? PointState.Close : PointState.Mine, "Same state has previous");
                            //}
                        }
                    }
                    break;
                //case GameState.Win:

                  //  break;
            }
            
            
        }

    }
}
