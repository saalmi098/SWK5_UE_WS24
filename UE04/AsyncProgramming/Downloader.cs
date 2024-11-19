using System.Net;
using static System.Console;

#pragma warning disable SYSLIB0014 // Type or member is obsolete; WebClient used for demonstration purposes. Use HttpClient instead

public class Downloader
{
    public void DownloadSync(string url, string filePath)
    {
        using var client = new WebClient();

        byte[] bytes = client.DownloadData(url);
        WriteLine($"{nameof(DownloadSync)}: Downloaded '{url}'");

        File.WriteAllBytes(filePath, bytes);
        WriteLine($"{nameof(DownloadSync)}: Saved '{filePath}'");
    }

    public void DownloadAsync_Legacy(string url, string filePath)
    {
        using var client = new WebClient();

        client.DownloadDataCompleted += (sender, e) =>
        {
            byte[] bytes = e.Result;
            WriteLine($"{nameof(DownloadAsync_Legacy)}: Downloaded '{url}'");

            var stream = new FileStream(filePath, FileMode.Create); // hier kein using verwenden, da sonst der Stream sofort geschlossen wird, und innerhalb der Callback-Methode nicht mehr verwendet werden kann
            stream.BeginWrite(bytes, 0, bytes.Length, asyncResult =>
            {
                stream.EndWrite(asyncResult);
                stream.Dispose();
                WriteLine($"{nameof(DownloadAsync_Legacy)}: Saved '{filePath}'");
            }, null);
        };

        client.DownloadDataAsync(new Uri(url));
    }

    public Task DownloadAsync_Task(string url, string filePath)
    {
        using var client = new WebClient();

        var task = client.DownloadDataTaskAsync(url)
            .ContinueWith(task =>
            {
                // ContinueWith wird aufgerufen, wenn der Task abgeschlossen ist
                // ContinueWith liefert wieder einen Task zurück, der das Ergebnis des vorherigen Tasks enthält (dieser wird abgeschlossen sein, wenn der
                // Download fertig ist und wir die Ausgabe erledigt ist)
                WriteLine($"{nameof(DownloadAsync_Task)}: Downloaded '{url}'");
                return task.Result;
            })
            .ContinueWith(task => File.WriteAllBytesAsync(filePath, task.Result))
            .ContinueWith(t => WriteLine($"{nameof(DownloadAsync_Task)}: Saved '{filePath}'"));

        return task;
    }

    // hier Return-Type von void auf Task geändert, um beim Aufrufer await verwenden zu können
    public async Task DownloadAsync_Await(string url, string filePath)
    {
        using var client = new WebClient();
        byte[] bytes = await client.DownloadDataTaskAsync(url); // await erfordert async in Methodensignatur
        // Task<T> vs Task: Task<T> gibt das Ergebnis des Tasks zurück, Task gibt void zurück

        WriteLine($"{nameof(DownloadAsync_Await)}: Downloaded '{url}'");

        await File.WriteAllBytesAsync(filePath, bytes);
        WriteLine($"{nameof(DownloadAsync_Await)}: Saved '{filePath}'");
    }

    public async Task DownloadMultipleAsync(string url1, string filePath1, string url2, string filePath2)
    {
        Task t1 = DownloadAsync_Await(url1, filePath1);
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of '{url1}' started");

        Task t2 = DownloadAsync_Await(url2, filePath2);
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of '{url2}' started");

        await Task.WhenAll(t1, t2); // WhenAll gibt einen Task zurück, der abgeschlossen ist, wenn alle Tasks in der Liste abgeschlossen sind
        WriteLine($"{nameof(DownloadMultipleAsync)}: {nameof(DownloadAsync_Await)} of all files completed");
    }
}

#pragma warning restore SYSLIB0014 // Type or member is obsolete