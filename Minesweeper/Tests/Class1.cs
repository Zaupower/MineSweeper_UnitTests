using Minesweeper.Core;
using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class Class1
    {
        [SetUp]
        public void SetUp()
        {
            //Setup before every test
            GameSettings settings = DifficultyManager.GetGameSettingsByDifficultylevel(DifficultyLevel.Beginner);

            var field = FieldGenerator.GetRandomField(settings.Width, settings.Height, settings.Mines);

            var gameProcessor = new GameProcessor(field);

            var currentField = gameProcessor.GetCurrentField();
        }

        [TearDown]  
        public void Postcondition()
        {

        }

        //Test method open
        //To test
        //if (GameState != GameState.Active) throw exception
        //if (targetCell.IsOpen) return GameState obj type;
        //if (targetCell.IsMine) GameState = GameState.Lose;
        //targetCell.MineNeighborsCount need to have the same value as the number of mines in the neighborhood, deixar para ultimo
        //if (openCount + mineCount == totalCount) GameState = GameState.Win;
        [Test]

    }
}