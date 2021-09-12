﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.DTO
{
    public class ServiceRequestPutDTO
    {
        public Guid Id { get; set; }
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string CurrentStatus { get; set; }
    }
}
