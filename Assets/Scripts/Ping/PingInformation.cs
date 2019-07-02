namespace Network.Ping
{
    [System.Serializable]
    public class PingInformation
    {
        public string Address;
        public int CurrentPing = 0;
        public int AveragePing = 0;

        private int _iterations = 0;

        public void AddPing(int ping)
        {
            CurrentPing = ping;
            _iterations++;
            AveragePing = (AveragePing + ping) / _iterations;
        }
    }

    public class PlayerPingInformation : PingInformation
    {
        public int PlayerID = -1;
    }
}
