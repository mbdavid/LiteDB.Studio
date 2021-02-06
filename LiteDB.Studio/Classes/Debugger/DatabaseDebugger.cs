using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LiteDB.Studio.Classes.Debugger
{
    public class DatabaseDebugger : IDisposable
    {
        private readonly LiteDatabase _db;
        private HttpListener _listener;

        public DatabaseDebugger(LiteDatabase db, int port)
        {
            _db = db;
            Port = port;
        }

        public int Port { get; }

        public void Dispose()
        {
            _listener?.Stop();
        }

        public Task Start()
        {
            return Task.Run(() =>
            {
                _listener = new HttpListener();

                _listener.Prefixes.Add($"http://localhost:{Port}/");

                _listener.Start();

                Console.WriteLine("Start debbugger listen: " + Port);

                while (true)
                    try
                    {
                        var context = _listener.GetContext();

                        Task.Run(() =>
                        {
                            var body = "";
                            var status = 200;

                            if (Regex.IsMatch(context.Request.RawUrl, @"^\/(\d+)?$"))
                            {
                                var pageID = context.Request.RawUrl == "/"
                                    ? 0
                                    : int.Parse(context.Request.RawUrl.Substring(1));

                                var page = context.Request.HttpMethod == "GET"
                                    ? _db.GetCollection($"$dump({pageID})").Query().FirstOrDefault()
                                    : GetPost(context.Request.InputStream);

                                if (page == null)
                                {
                                    body = $"Page {pageID} not found in database";
                                }
                                else
                                {
                                    var dump = new HtmlPageDump(page);

                                    body = dump.Render();
                                }
                            }
                            else if (Regex.IsMatch(context.Request.RawUrl, @"^\/list/(\d+)$"))
                            {
                                var pageID = int.Parse(context.Request.RawUrl.Substring(6));
                                var exp = new HtmlPageList(_db.GetCollection($"$page_list({pageID})").Query()
                                    .Limit(1000).ToEnumerable());

                                body = exp.Render();
                            }
                            else
                            {
                                body = "Url not found";
                                status = 404;
                            }

                            var message = Encoding.UTF8.GetBytes(body);

                            context.Response.StatusCode = status;
                            context.Response.ContentType = "text/html";
                            context.Response.ContentLength64 = message.Length;
                            context.Response.OutputStream.Write(message, 0, message.Length);
                        });
                    }
                    catch (Exception)
                    {
                    }
            });
        }

        private BsonDocument GetPost(Stream input)
        {
            var text = WebUtility.UrlDecode(new StreamReader(input).ReadToEnd().Substring(2));
            var bytes = text.Trim().Split(' ');
            var buffer = new byte[bytes.Length];

            for (var i = 0; i < bytes.Length; i++) buffer[i] = Convert.ToByte(bytes[i].Trim(), 16);

            return new BsonDocument {["buffer"] = buffer};
        }
    }
}