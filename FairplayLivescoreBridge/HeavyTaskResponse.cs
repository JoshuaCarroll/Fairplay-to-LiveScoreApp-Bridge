using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace FairplayLivescoreBridge
{
    class HeavyTaskResponse
    {
        private readonly string message;

        public HeavyTaskResponse(string msg)
        {
            this.message = msg;
        }

        public string Message { get { return message; } }
    }
}
