using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class DistressResponses {

        public CalmDistressLevelResponse CalmResponse = new CalmDistressLevelResponse();
        public NonCalmDistressLevelResponse NonCalmResponse = new NonCalmDistressLevelResponse();

    }
}
