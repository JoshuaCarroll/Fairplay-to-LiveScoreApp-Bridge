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
        public string Down;
        public string ToGo;
        public string BallOn;
        public string Posesssion;

        private string dataQueue;
        private int processDataQueueCursor;

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
                STX++;
                int ETX = dataQueue.IndexOf("\x0003", STX);

                if (ETX > STX)
                {
                    ProcessDataQueue();
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
                processDataQueueCursor = 0;

                string messageType = getValue(payload, 1);

                switch (messageType)
                {
                    case "B":
                        TeamStats team = new TeamStats();

                        string homeOrAway = getValue(payload, 1);

                        team.TeamName = getValue(payload, 10).Trim();
                        int.TryParse(getValue(payload, 3).Replace(" ", ""), out team.Score);
                        int.TryParse(getValue(payload, 2), out team.TeamFouls);
                        int.TryParse(getValue(payload, 1), out team.TimeOutsLeft);
                        team.Possession = getValue(payload, 1);
                        team.Bonus = getValue(payload, 1);
                        team.DoubleBonus = getValue(payload, 1);
                        team.Timeout = getValue(payload, 1);
                        //int.TryParse(getValue(payload, 2), out team.LastPlayerNumber);
                        //int.TryParse(getValue(payload, 1), out team.LastPlayerFouls);
                        //int.TryParse(getValue(payload, 2), out team.LastPlayerPoints);

                        if (homeOrAway == "H")
                        {
                            Home = team;
                        }
                        else
                        {
                            Away = team;
                        }
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
                    case "F":
                        Home = new TeamStats();
                        Away = new TeamStats();
                        Home.TeamName = getValue(payload, 10).Trim();
                        Home.Score = int.Parse(getValue(payload, 2));
                        Home.TimeOutsLeft = int.Parse(getValue(payload, 1));
                        Away.TeamName = getValue(payload, 10).Trim();
                        Away.Score = int.Parse(getValue(payload, 2));
                        Away.TimeOutsLeft = int.Parse(getValue(payload, 1));
                        Down = getValue(payload, 1);
                        ToGo = getValue(payload, 2);
                        BallOn = getValue(payload, 2);
                        Posesssion = getValue(payload, 1);
                        break;
                    default:
                        break;
                }
                dataQueue = dataQueue.Substring(ETX);
            }
        }

        private string getValue(string payload, int length)
        {
            string ret = payload.Substring(processDataQueueCursor, length);
            processDataQueueCursor += length;
            return ret;
        }
        
    }
}
