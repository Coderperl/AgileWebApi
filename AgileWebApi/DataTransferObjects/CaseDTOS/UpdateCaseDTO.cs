﻿using AgileWebApi.Data;

namespace AgileWebApi.DataTransferObjects.CaseDTO
{
    public class UpdateCaseDTO
    {
        public int TechnicianId { get; set; }
        public Comment Comment { get; set; }
        public string Status { get; set; }
    }
}
