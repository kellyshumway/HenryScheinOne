using Microsoft.AspNetCore.Mvc;

namespace HenryScheinOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private static List<Patient> _patients = new();

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }
        /*
        [HttpPost(Name = "PostPatientInfo")]
        public void PostPatient([FromBody]Patient patient)
        {
            _patients.Add(patient);
        }

        [HttpPost(Name = "PostPatientInfo")]
        public void PostPatientList([FromBody]List<Patient> patients)
        {
            foreach(var patient in patients)
                _patients.Add(patient);
        }
        */
        [HttpPost(Name = "PostPatientInfo")]
        public void PostPatientString([FromBody]string inputString)
        {
            string[] patientData;
            if(string.IsNullOrEmpty(inputString))
                return;
            else
                patientData = inputString.Split('\n');

            foreach (string patient in patientData)
            {
                string cleanPatient = string.Empty;
                foreach (char ch in patient)
                    if (ch != '\"' && ch != '\"')
                        cleanPatient += ch;

                string[] data = cleanPatient.Split(',');

                Patient pat = new()
                {
                    Name = data[0] + "," + data[1],
                    SSN = data[2],
                    Age = int.Parse(data[3]),
                    Phone = data[4],
                    Status = data[5] + "," + data[6]
                };

                _patients.Add(pat);
            }
        }

        [HttpGet(Name = "GetPatientInfo")]
        public IEnumerable<Patient> Get()
        {
            return _patients;
        }
    }
}