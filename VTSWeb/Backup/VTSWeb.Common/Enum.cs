using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VTSWeb.Enum
{
    public enum DateForm : byte
    {
        SQL,
        English,
        Indonesian
    }

    public enum YesNo : byte
    {
        Yes = 1,
        No = 0
    }

    public enum AssignmentStatus : byte
    {
        OnHold,
        OnProgress,
        Done,
        Approved,
        Posting
    }

    public enum AssignmentPriority : byte
    {
        Low,
        Medium,
        High
    }

    public enum OrganizationUnitActiveStatus : byte
    {
        Active,
        NotActive
    }
    public enum CustomerStatus : byte
    {
        Active = 0,
        NotActive = 1
    }

    public enum ClearanceStatus : byte
    {

        NoComplete,
        Complete
    }
    
}

