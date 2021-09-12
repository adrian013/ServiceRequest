using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.DTO
{
    public static class Enums
    {
        public enum CurrentStatus
        {
            NotApplicable,
            Created,
            InProgress,
            Complete,
            Canceled
        }
    }
}
