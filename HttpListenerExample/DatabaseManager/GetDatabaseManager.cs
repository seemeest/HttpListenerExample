using HttpListenerExample.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    public static partial class DatabaseManager
    {
        private static string connStr = "server=127.0.0.1;user=root;database=clinic;";
        public static List<Appointment> GetAppointmentsFromDatabase()
        {
            List<Appointment> appointments = new List<Appointment>();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string query = "SELECT * FROM `Appointments`";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment();
                            appointment.AppointmentId = reader.GetInt32(0);
                            appointment.PatientId = reader.GetInt32(1);
                            appointment.DoctorId = reader.GetInt32(2);
                            appointment.DepartmentId = reader.GetInt32(3);
                            appointment.AppointmentDate = reader.GetDateTime(4);

                            appointments.Add(appointment);
                        }
                    }
                }
            }

            return appointments;
        }

        public static List<Diagnosis> GetDiagnosisFromDatabase()
        {
            List<Diagnosis> diagnosis = new List<Diagnosis>();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string query = "SELECT * FROM `diagnoses`";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Diagnosis diag = new Diagnosis();
                            diag.DiagnosisId = reader.GetInt32(0);
                            diag.AppointmentId = reader.GetInt32(1);
                            diag.DiagnosisText = reader.GetString(2);

                            diagnosis.Add(diag);
                        }
                    }
                }
            }

            return diagnosis;
        }

        public static List<Patient> GetPatientsFromDatabase()
        {
            List<Patient> patients = new List<Patient>();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string query = "SELECT * FROM `patients`";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient();
                            patient.PatientId = reader.GetInt32(0);
                            patient.FirstName = reader.GetString(1);
                            patient.LastName = reader.GetString(2);
                            patient.DateOfBirth = reader.GetDateTime(3);
                            patient.Gender = reader.GetString(4);
                            patient.PhoneNumber = reader.GetString(5);
                            patient.Address = reader.GetString(6);

                            patients.Add(patient);
                         
                        }
                    }
                }
            }

            return patients;
        }

        public static List<Department> GetDepartmentsFromDatabase()
        {
            List<Department> departments = new List<Department>();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string query = "SELECT * FROM `departments`";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Department department = new Department();
                            department.DepartmentId = reader.GetInt32(0);
                            department.Name = reader.GetString(1);
                            department.Location = reader.GetString(2);

                            departments.Add(department);
                        }
                    }
                }
            }

            return departments;
        }

        public static List<Doctor> GetDoctorsFromDatabase()
        {
            List<Doctor> doctors = new List<Doctor>();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string query = "SELECT * FROM doctors";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Doctor doctor = new Doctor();
                            doctor.DoctorId = reader.GetInt32("doctor_id");
                            doctor.FirstName = reader.GetString("first_name");
                            doctor.LastName = reader.GetString("last_name");
                            doctor.Specialization = reader.GetString("specialization");
                            doctor.PhoneNumber = reader.GetString("phone_number");
                            doctor.mobile_number = reader.IsDBNull(reader.GetOrdinal("mobile_number")) ? null : reader.GetString("mobile_number");
                            doctor.address = reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString("address");

                            doctors.Add(doctor);
                        }
                    }
                }
            }

            return doctors;
        }


        public static List<Prescription> GetPrescriptionsFromDatabase()
        {
            List<Prescription> prescriptions = new List<Prescription>();

            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string query = "SELECT * FROM `prescriptions`";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Prescription prescription = new Prescription();
                            prescription.PrescriptionId = reader.GetInt32(0);
                            prescription.AppointmentId = reader.GetInt32(1);
                            prescription.Medication = reader.GetString(2);
                            prescription.Dosage = reader.GetString(3);

                            prescriptions.Add(prescription);
                        }
                    }
                }
            }

            return prescriptions;
        }

        ////////////////////////////////////////////////////////////////
        public static bool IsClientAuthorized(HttpListenerRequest request)
        {
            string authorizationHeader = request.Headers["Authorization"];
            if (authorizationHeader != null && authorizationHeader.StartsWith("Basic "))
            {
                string credentials = authorizationHeader.Substring("Basic ".Length).Trim();
                string decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));

                string[] parts = decodedCredentials.Split(':');
                if (parts.Length == 2)
                {
                    string username = parts[0];
                    Console.WriteLine(username);
                    string password = parts[1];

                    using (MySqlConnection connection = new MySqlConnection(connStr))
                    {
                        connection.Open();

                        string query = "SELECT COUNT(*) FROM clients WHERE username = @username AND password = @password";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@username", username);
                            command.Parameters.AddWithValue("@password", password);
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            return count > 0;
                        }
                    }
                }
            }

            return false;
        }
    }
}
