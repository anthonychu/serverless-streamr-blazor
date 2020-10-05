using System;
using ServerlessStreamR.Shared;

namespace ServerlessStreamR.Client.Models
{
    public class UserVideo
    {
        private DateTime lastUpdated = DateTime.Now;
        public DateTime LastUpdated
        { 
            get => lastUpdated;
        }

        private Frame frame;
        public Frame Frame
        {
            get { return frame; }
            set
            {
                frame = value;
                lastUpdated = DateTime.Now;
            }
        }
        
    }
}