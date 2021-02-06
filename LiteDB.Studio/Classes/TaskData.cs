using System;
using System.Collections.Generic;
using System.Threading;

namespace LiteDB.Studio.Classes
{
    internal class TaskData
    {
        public const int RESULT_LIMIT = 1000;

        public bool IsGridLoaded = false;
        public bool IsParametersLoaded = false;
        public bool IsTextLoaded = false;
        public ManualResetEventSlim WaitHandle = new ManualResetEventSlim(false);

        public int Id { get; set; }
        public bool Executing { get; set; } = false;

        public string EditorContent { get; set; } = "";
        public string SelectedTab { get; set; } = "";
        public Tuple<int, int> Position { get; set; }

        public string Sql { get; set; } = "";
        public string Collection { get; set; } = "";
        public List<BsonValue> Result { get; set; }
        public BsonDocument Parameters { get; set; } = new BsonDocument();

        public bool LimitExceeded { get; set; }
        public Exception Exception { get; set; } = null;
        public TimeSpan Elapsed { get; set; } = TimeSpan.Zero;

        public Thread Thread { get; set; }
        public bool ThreadRunning { get; set; } = true;

        public void ReadResult(IBsonDataReader reader)
        {
            Result = new List<BsonValue>();
            LimitExceeded = false;
            Collection = reader.Collection;

            var index = 0;

            while (reader.Read())
            {
                if (index++ >= RESULT_LIMIT)
                {
                    LimitExceeded = true;
                    break;
                }

                Result.Add(reader.Current);
            }
        }
    }
}