using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fairplay
{
    /// <summary>
    /// Class for receiving and processing data from a Fair Play MP-70 or MP-80 via an SDI-DB9 (RS-232)
    /// </summary>
    class Mp70Rs232Data
    {
        public string GameClock;
        public string GamePeriod;
        public string PlayTimer;
        public string ShotClock
        {
            get
            {
                return PlayTimer;
            }
            set
            {
                PlayTimer = value;
            }
        }
        public TeamStats Home;
        public TeamStats Away;
        private string dataQueue;

        public Mp70Rs232Data()
        {
            Home = new TeamStats();
            Away = new TeamStats();
        }

        public void Receive(string input)
        {
            dataQueue += input;

            int STX = dataQueue.IndexOf("\x0002");

            if (STX >= 0)
            {
                try
                {
                    STX++;
                    int ETX = dataQueue.IndexOf("\x0003", STX);

                    if (ETX > STX)
                    {
                        string payload = dataQueue.Substring(STX, ETX - STX);
                        ProcessDataQueue();
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // Ignore
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void ProcessDataQueue()
        {
            int ETX = dataQueue.LastIndexOf("\x0003");
            int STX = dataQueue.LastIndexOf("\x0002", ETX);

            if ((-1 < STX) && (STX < ETX))
            {
                STX++;
                string payload = dataQueue.Substring(STX, ETX - STX);

                switch (payload.Substring(0, 1))
                {
                    case "B":
                        TeamStats team;

                        if (payload.Substring(1, 1) == "H")
                        {
                            team = Home;
                        }
                        else
                        {
                            team = Away;
                        }

                        team.TeamName = payload.Substring(2, 10).Trim();
                        int.TryParse(payload.Substring(12, 4).Replace(" ", ""), out team.Score);
                        int.TryParse(payload.Substring(15, 2), out team.TeamFouls);
                        int.TryParse(payload.Substring(17, 1), out team.TimeOutsLeft);
                        team.Possession = payload.Substring(18, 1);
                        team.Bonus = payload.Substring(19, 1);
                        team.DoubleBonus = payload.Substring(20, 1);
                        team.Timeout = payload.Substring(21, 1);
                        int.TryParse(payload.Substring(22, 2), out team.LastPlayerNumber);
                        int.TryParse(payload.Substring(24, 1), out team.LastPlayerFouls);
                        int.TryParse(payload.Substring(25, 2), out team.LastPlayerPoints);
                        break;
                    case "C":
                        GameClock = payload.Substring(1, 2);
                        if (GameClock.Trim() != "") { GameClock += ":"; }
                        GameClock += payload.Substring(3, 2);
                        if (payload.Substring(5,1) != " ") { GameClock += "." + payload.Substring(5, 1); }
                        GameClock = GameClock.Trim();
                        GamePeriod = payload.Substring(6, 1);
                        PlayTimer = payload.Substring(7, 2);
                        break;
                    default:
                        break;
                }
                dataQueue = dataQueue.Substring(ETX);
            }
        }
    }
}
