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
        private PointState[,]? _currentField;
        
        [Test]
        public void GameProcessor_GameProcessorObject_Equal([Values(0, 1, 2)] int difficultyLevel)
        {
            //Setup before every test
            _settings = DifficultyManager.GetGameSettingsByDifficultylevel((DifficultyLevel)difficultyLevel);

            _field = FieldGenerator.GetRandomField(_settings.Height, _settings.Width, _settings.Mines);

            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);
            Assert.IsInstanceOf<Minesweeper.Core.GameProcessor>(_gameProcessor);
        }

        [Test]
        public void GameProcessor_RectangularFieldGameProcessorObject_Equal([Values(0, 1, 2)] int difficultyLevel, [Values(5, 10)] int rowLenght, [Values(10,5)] int columnLenght)
        {
            //Setup before every test
            _settings = DifficultyManager.GetGameSettingsByDifficultylevel((DifficultyLevel)difficultyLevel);

            _field = FieldGenerator.GetRandomField(rowLenght, columnLenght, _settings.Mines);

            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);
            Assert.IsInstanceOf<Minesweeper.Core.GameProcessor>(_gameProcessor);
        }
    }
}
