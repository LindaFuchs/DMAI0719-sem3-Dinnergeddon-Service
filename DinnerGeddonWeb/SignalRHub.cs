﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace DinnerGeddonWeb {
    public class SignalRHub : Hub {
        public void Hello() {
            Clients.All.hello();
        }
    }
}