using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace SnailRace;

public partial class MainPage : ContentPage
{
    public int Timeout = 500;
    bool GameOver = false;
    Stopwatch sw = new Stopwatch();
    Random rnd = new Random();
    Thread SnailMoverThread;
    string name = "";
    SqliteConnection connection;
    public MainPage()
    {
        InitializeComponent();
        SnailMoverThread = new Thread(StartSnails);
        SnailMoverThread.IsBackground = true;
        var SqliteConnectionString = new SqliteConnectionStringBuilder();
        SqliteConnectionString.DataSource = @"D:\\DB1";
        connection = new SqliteConnection(SqliteConnectionString.ConnectionString);
        connection.Open();
    }
    ~MainPage()
    {

    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        RandomPlace();
        MoveSnail(Snail0);
    }
    public void RandomPlace()
    {
        var rndwidth = rnd.Next(0, (int)(this.Width - TargetButton.WidthRequest));
        var rndheight = rnd.Next(0, (int)(this.Height - TargetButton.HeightRequest - 110));
        TargetButton.Margin = new Thickness(rndwidth, rndheight, 0, 0);
    }
    public void MoveSnail(Image snail)
    {

        MainThread.BeginInvokeOnMainThread(() =>
        {
            snail.Margin = new Thickness(snail.Margin.Left + this.Width / 20, 0, 0, 0);
        });
        if (snail.Margin.Left > this.Width - 20)
        {
            GameOver = true;
            if (snail == Snail0)
            {
            }
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                StartButton.IsEnabled = true;
                StartButton.IsVisible = true;
                TargetButton.IsVisible = false;
                Snail0.Margin = new Thickness(0, 0, 0, 0);
                Snail1.Margin = new Thickness(0, 0, 0, 0);
                Snail2.Margin = new Thickness(0, 0, 0, 0);
                Snail3.Margin = new Thickness(0, 0, 0, 0);
                await DisplayAlert("Congratulations!", "You won!", "Ok");
                name = await DisplayPromptAsync("Savescore", "Would you like to save your score?\n" + sw.ElapsedMilliseconds + " ms", placeholder: "Your hame here", accept: "Save");
                if (name != "")
                {
                    SaveToDB(name, sw.ElapsedMilliseconds);
                }
            });
        }

    }

    private void SaveToDB(string name, long elapsedMilliseconds)
    {
        using (var transaction = connection.BeginTransaction())
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Scores VALUES ('"+elapsedMilliseconds+"','"+name+"')";
            command.ExecuteNonQuery();
            transaction.Commit();
        }
    }

    public void StartSnails()
    {
        while (!GameOver)
        {
            switch (rnd.Next(0, 3))
            {
                case 0:
                    MoveSnail(Snail1);
                    break;
                case 1:
                    MoveSnail(Snail2);
                    break;
                case 2:
                    MoveSnail(Snail3);
                    break;
            }
            Thread.Sleep(Timeout);
        }
        GameOver = false;
        sw.Stop();
    }
    public void StartButtonClicked(object sender, EventArgs e)
    {
        TargetButton.IsVisible = true;
        StartButton.IsVisible = false;
        {
            SnailMoverThread = new Thread(StartSnails);
            SnailMoverThread.IsBackground = true;
        }
        sw.Restart();
        SnailMoverThread.Start();
    }
}

