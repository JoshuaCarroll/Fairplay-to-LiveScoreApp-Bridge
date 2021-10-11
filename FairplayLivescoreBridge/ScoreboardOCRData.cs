using Fairplay;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairplayLivescoreBridge
{
    class ScoreboardOCRData
    {
        public string GameClock;
        public string Period
        {
            get
            {
                return period;
            }
            set
            {
                period = checkNumericValue(value);
            }
        }
        public string Quarter
        {
            get
            {
                return period;
            }
            set
            {
                period = checkNumericValue(value);
            }
        }
        public string ShotClock
        {
            get
            {
                return shotClock;
            }
            set
            {
                if (value == null)
                {
                    shotClock = "";
                }
                else
                {
                    shotClock = value.Trim();
                }
            }
        }
        public string PlayClock
        {
            get
            {
                return shotClock;
            }
            set
            {
                if (value == null)
                {
                    shotClock = "";
                }
                else
                {
                    shotClock = value.Trim();
                }
            }
        }
        public string ScoreAway
        {
            get
            {
                return scoreAway;
            }
            set
            {
                scoreAway = checkNumericValue(value);
            }
        }
        public string ScoreHome
        {
            get
            {
                return scoreHome;
            }
            set
            {
                scoreHome = checkNumericValue(value);
            }
        }
        public string FoulsAway
        {
            get
            {
                return foulsAway;
            }
            set
            {
                foulsAway = checkNumericValue(value);
            }
        }
        public string FoulsHome
        {
            get
            {
                return foulsHome;
            }
            set
            {
                foulsHome = checkNumericValue(value);
            }
        }
        public string TimeoutsAway
        {
            get
            {
                return timeoutsAway;
            }
            set
            {
                timeoutsAway = checkNumericValue(value);
            }
        }
        public string TimeoutsHome
        {
            get
            {
                return timeoutsHome;
            }
            set
            {
                timeoutsHome = checkNumericValue(value);
            }
        }
        public string Downs
        {
            get
            {
                return down;
            }
            set
            {
                down = checkNumericValue(value);
            }
        }
        public string Yards
        {
            get
            {
                return toGo;
            }
            set
            {
                toGo = checkNumericValue(value);
            }
        }
        public string LOS
        {
            get
            {
                return ballOn;
            }
            set
            {
                ballOn = checkNumericValue(value);
            }
        }
        public string Possession
        {
            get
            {
                return possession;
            }
            set
            {
                possession = value;
            }
        }
        public string PossessionAway
        {
            get
            {
                if (Possession == "V")
                {
                    return "1";
                }
                else
                {
                    return "";
                }
            }
        }
        public string PossessionHome
        {
            get
            {
                if (Possession == "H")
                {
                    return "1";
                }
                else
                {
                    return "";
                }
            }
        }

        private string period;
        private string scoreAway;
        private string scoreHome;
        private string foulsAway;
        private string foulsHome;
        private string timeoutsAway;
        private string timeoutsHome;
        private string shotClock;
        private string down;
        private string toGo;
        private string ballOn;
        private string possession;

        public ScoreboardOCRData Parse (Mp70Rs232Data mp70Rs232Data)
        {
            ScoreAway = mp70Rs232Data.Away.Score.ToString();
            ScoreHome = mp70Rs232Data.Home.Score.ToString();
            Period = mp70Rs232Data.GamePeriod;
            GameClock = mp70Rs232Data.GameClock;
            ShotClock = mp70Rs232Data.ShotClock;
            FoulsAway = mp70Rs232Data.Away.TeamFouls.ToString();
            FoulsHome = mp70Rs232Data.Home.TeamFouls.ToString();
            TimeoutsAway = mp70Rs232Data.Away.TimeOutsLeft.ToString();
            TimeoutsHome = mp70Rs232Data.Home.TimeOutsLeft.ToString();
            Downs = mp70Rs232Data.Down;
            Yards = mp70Rs232Data.ToGo;
            LOS = mp70Rs232Data.BallOn;
            Possession = mp70Rs232Data.Posesssion;

            return this;
        }

        public string ToJson()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.None);
            json = "{\"type\":\"ocr\",\"values\":" + json + "}\n";
            Debug.WriteLine(json);
            return json;
        }

        private string checkNumericValue(string val)
        {
            int i;

            if ((val == null) || (val.Trim() == ""))
            {
                val = "0";
            }
            else if (!int.TryParse(val, out i))
            {
                val = "0";
            }

            return val;
        }
    }
}
