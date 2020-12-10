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
        public string Period;
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
        private string ScoreAway
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
        private string ScoreHome
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
        private string FoulsHome
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
        private string TimeoutsAway
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
        private string TimeoutsHome
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

        private string scoreAway;
        private string scoreHome;
        private string foulsAway;
        private string foulsHome;
        private string timeoutsAway;
        private string timeoutsHome;
        private string shotClock;

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
