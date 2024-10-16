void LogTimerExpiration()
{
    Console.WriteLine("Timer expired");
}

var timer = new TimerDemo.Timer
{
    Interval = 500,
    //Expired = LogTimerExpiration,
    //Expired = () => Console.WriteLine("Timer expired");
};

timer.Expired += LogTimerExpiration;
timer.Expired += () => Console.WriteLine("Timer expired (Lamba)");

timer.Expired -= LogTimerExpiration;
// Deregistrieren mit Lambda nicht moeglich, da Lambda anonym ist. Workaround: Lambda in Variable speichern und diese deregistrieren

timer.Start();