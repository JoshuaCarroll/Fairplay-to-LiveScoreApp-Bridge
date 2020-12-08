﻿using Fairplay;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairplayLivescoreBridge
{
    class ScoreboardOCRData
    {
        string ScoreAway;
        string ScoreHome;
        string Period;
        string GameClock;
        string ShotClock;
        string FoulsAway;
        string FoulsHome;
        string TimeoutsAway;
        string TimeoutsHome;

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
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
    }
}