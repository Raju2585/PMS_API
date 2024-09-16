﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities.DTOs
{
    public class MedicalHistoryDTOs
    {

        public int HistoryId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime RecordedDate { get; set; }
        public string Reason { get; set; }

        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public List<string> Medication { get; set; } = new List<string>();

        // Boolean fields for Personal Medical History
        public bool HasAsthma { get; set; }
        public bool HasBloodPressure { get; set; }
        public bool HasCancer { get; set; }
        public bool HasCholesterol { get; set; }
        public bool HasDiabetes { get; set; }
        public bool HasHeartDisease { get; set; }




    }
}
