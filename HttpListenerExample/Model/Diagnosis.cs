using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample.Model
{
    public class Diagnosis
    {
        public int DiagnosisId { get; set; }
        public int AppointmentId { get; set; }
        public string DiagnosisText { get; set; }


    }
}
