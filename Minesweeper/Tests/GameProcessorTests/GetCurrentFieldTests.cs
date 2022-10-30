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
        [Test]
        public void GetCurrentField_GameProcessorObject_Equal([Values(0, 1, 2)] int difficultyLevel)
        {
            //Setup before every test
            _settings = DifficultyManager.GetGameSettingsByDifficultylevel((DifficultyLevel)difficultyLevel);

            _field = FieldGenerator.GetRandomField(_settings.Height, _settings.Width, _settings.Mines);

            _gameProcessor = new Minesweeper.Core.GameProcessor(_field);
            Assert.IsInstanceOf<Minesweeper.Core.GameProcessor>(_gameProcessor);
        }

    }
}
