﻿namespace PeliApi.Models
{
    public class HighScore
    {
        public int ID { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
