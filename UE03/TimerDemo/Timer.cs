namespace TimerDemo
{
    // Action ... immer ohne Rueckgabewert (aber optional mit generischen Parametern)
    // Func ... immer mit Rueckgabewert (letzter generischer Parameter = Rueckgabetyp)

    public delegate void ExpiredEventHandler(); // koennte auch durch Action ersetzt werden

    public class Timer
    {
        private const int DEFAULT_INTERVAL = 1000;
        private readonly Thread thread;

        public Timer()
        {
            thread = new Thread(OnTick);
        }

        public int Interval { get; set; } = DEFAULT_INTERVAL;

        //public ExpiredEventHandler? Expired { get; set; }
        public event ExpiredEventHandler? Expired;
        // Vorteil von event: man kann Delegate nicht mehr mit = zuweisen, nur noch mit += und -=
        // (damit man nicht andere Registrierungen ueberschreiben kann)

        public void Start() => thread.Start();

        private void OnTick()
        {
            while (true)
            {
                Thread.Sleep(Interval);

                Expired?.Invoke(); // immer Invoke verwenden, um NullReferenceException zu vermeiden
                // auf Null-Pruefung immer achten (auch bei Kurztests)
            }
        }
    }
}
