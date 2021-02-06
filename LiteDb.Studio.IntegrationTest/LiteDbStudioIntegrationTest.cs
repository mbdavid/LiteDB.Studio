using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiteDB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteDb.Studio.IntegrationTest
{
    [TestCategory("Integration")]
    [TestClass]
    public class LiteDbStudioIntegrationTest
    {
        private enum TestCode
        {
            One = 1,
            Two = 2,
        }

        private class TestData
        {
            public TestCode TestCode { get; set; }
            public string TestValue { get; set; }
        }

        private TestData GetTestData()
        {
            return new TestData() {TestCode = (TestCode) new Random().Next(0, 3), TestValue = GetTestValue()};
        }

        private string GetTestValue()
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < random.Next(10, 1000); i++)
            {
                double myFloat = random.NextDouble();
                var myChar = Convert.ToChar(Convert.ToInt32(Math.Floor(25 * myFloat) + 65));
                sb.Append(myChar);
            }

            return sb.ToString();
        }

        private class TestCommand
        {
            public string CommandName { get; set; }
            public TestData TestData { get; set; }
            public DateTime TimeStamp { get; set; }
            [BsonId] public int DatabaseId { get; set; }
            public int ForeignKeyIndex { get; set; }

            public TestCommand()
            {

            }

            public TestCommand(string commandName, TestData testData)
            {
                CommandName = commandName;
                TestData = testData;
                TimeStamp = DateTime.Now;
            }
        }

        private class NameStorage
        {
            public string Data { get; set; }
            public int ForeignKeyIndex { get; set; }
            [BsonId] public int DatabaseId { get; set; }

            public NameStorage(string data, int foreignKeyIndex)
            {
                Data = data;
                ForeignKeyIndex = foreignKeyIndex;
            }

            public NameStorage()
            {

            }
        }




        [TestMethod]
        public void CreateTestDatabases()
        {
            if (MessageBox.Show("Do you really want to recreate test data?", "Confirm", MessageBoxButtons.YesNo) !=
                DialogResult.Yes)
                return;

            foreach (var f in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.db"))
                File.Delete(f);
            var testCommands = new List<TestCommand>
            {
                new TestCommand("Command 00", GetTestData()),
                new TestCommand("Command 01", null),
                new TestCommand("Command 02", GetTestData()),
                new TestCommand("Command 03", GetTestData()),
                new TestCommand("Command 04", null),
                new TestCommand("Command 05", GetTestData()),
                new TestCommand("Command 06", GetTestData()),
                new TestCommand("Command 07", null),
                new TestCommand("Command 08", GetTestData()),
                new TestCommand("Command 09", GetTestData()),
                new TestCommand("Command 0A", GetTestData()),
                new TestCommand("Command 0B", GetTestData())
            };

            using (var db = new LiteDatabase($"Filename=test.db;Password=123"))
            {


                var numberOfEntries = 5;
                var numberOfLevels3 = 10;
                var numberOfLevels2 = 4;
                var numberOfLevels1 = 4;

                var random = new Random((int) DateTime.Now.Ticks);


                foreach (var level1 in Enumerable.Range(1, numberOfLevels1))
                {
                    var level1Name = $"Level(1) {level1}";
                    var level1Entry = new NameStorage(level1Name,-1);
                    db.GetCollection<NameStorage>(nameof(level1Name)).EnsureIndex(x => x.ForeignKeyIndex);
                    db.GetCollection<NameStorage>(nameof(level1Name)).EnsureIndex(x => x.Data);
                    db.GetCollection<NameStorage>(nameof(level1Name)).Insert(level1Entry);
                    var level1Id = level1Entry.DatabaseId;
                    foreach (var level2 in Enumerable.Range(1, numberOfLevels2))
                    {
                        var level2Name = $"Level(2) {level2}";
                        var level2Entry = new NameStorage(level2Name, level1Id);
                        db.GetCollection<NameStorage>(nameof(level2Name)).EnsureIndex(x => x.ForeignKeyIndex);
                        db.GetCollection<NameStorage>(nameof(level2Name)).EnsureIndex(x => x.Data);
                        db.GetCollection<NameStorage>(nameof(level2Name)).Insert(level2Entry);
                        var level2Id = level2Entry.DatabaseId;
                        foreach (var level3 in Enumerable.Range(1, numberOfLevels3))
                        {
                            var level3Name = $"Level(3) {level3}";
                            var level3Entry = new NameStorage(level3Name, level2Id);
                            db.GetCollection<NameStorage>(nameof(level3Name)).EnsureIndex(x => x.ForeignKeyIndex);
                            db.GetCollection<NameStorage>(nameof(level3Name)).EnsureIndex(x => x.Data);
                            db.GetCollection<NameStorage>(nameof(level3Name)).Insert(level3Entry);
                            var level3Id = level3Entry.DatabaseId;
                            foreach (var execution in Enumerable.Range(1, numberOfEntries))
                            {
                                var entryName = $"Entry {execution}";
                                var entryEntry = new NameStorage(entryName, level3Id);
                                db.GetCollection<NameStorage>(nameof(entryName)).EnsureIndex(x => x.ForeignKeyIndex);
                                db.GetCollection<NameStorage>(nameof(entryName)).EnsureIndex(x => x.Data);
                                db.GetCollection<NameStorage>(nameof(entryName)).Insert(entryEntry);
                                var entryId = entryEntry.DatabaseId;
                                foreach (var number in Enumerable.Range(1, random.Next(5, 15)))
                                {
                                    var index = random.Next(0, testCommands.Count);
                                    var cmd = new TestCommand(testCommands[index].CommandName,
                                        testCommands[index].TestData) {ForeignKeyIndex = entryId};
                                    db.GetCollection<TestCommand>(nameof(TestCommand)).EnsureIndex(x => x.ForeignKeyIndex);
                                    db.GetCollection<TestCommand>(nameof(TestCommand)).EnsureIndex(x => x.CommandName);
                                    db.GetCollection<TestCommand>(nameof(TestCommand)).EnsureIndex(x => x.TimeStamp);
                                    db.GetCollection<TestCommand>(nameof(TestCommand)).Insert(cmd);
                                }

                            }
                        }
                    }
                }

            }

        }

    }
}