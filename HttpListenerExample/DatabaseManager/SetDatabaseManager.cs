using HttpListenerExample.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    public static partial class DatabaseManager
    {
        public static async Task<bool> SetAppointmentsToDatabase(List<Appointment> appointmens)
        {
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {


                connection.Open();

                string insertQuery = "INSERT INTO `appointments` (`patient_id`, `doctor_id`, `department_id`, `appointment_date`) " +
                    "VALUES (@patient_id, @doctor_id, @department_id, @appointment_date)";

                string updateQuery = "UPDATE `appointments` SET `patient_id` = @patient_id, `doctor_id` = @doctor_id, " +
                    "`department_id` = @department_id, `appointment_date` = @appointment_date WHERE `appointment_id` = @appointment_id";

                foreach (Appointment appointmen in appointmens)
                {
                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;

                        if (appointmen.AppointmentId == 0)
                        {
                            command.CommandText = insertQuery;
                        }
                        else
                        {
                            command.CommandText = updateQuery;
                            command.Parameters.AddWithValue("@appointment_id", appointmen.AppointmentId);
                        }

                        command.Parameters.AddWithValue("@patient_id", appointmen.PatientId);
                        command.Parameters.AddWithValue("@doctor_id", appointmen.DoctorId);
                        command.Parameters.AddWithValue("@department_id", appointmen.DepartmentId);
                        command.Parameters.AddWithValue("@appointment_date", appointmen.AppointmentDate);

                        try
                        {
                            Task<int> task = command.ExecuteNonQueryAsync();
                            int rowsAffected = await task;
                            Console.WriteLine($"Количество затронутых строк: {rowsAffected}");
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex.ToString());
                            return false;
                        }

                    }
                    
                }
                return true;
            }
        }
        public static async Task<bool> SetDiagnosisToDatabase(List<Diagnosis> diagnosis)
        {
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {


                connection.Open();

                string insertQuery = "INSERT INTO `diagnoses` (`appointment_id`, `diagnosis_text`) " +
                    "VALUES (@appointment_id, @diagnosis_text)";

                string updateQuery = "UPDATE `diagnoses` SET `appointment_id` = @appointment_id, `diagnosis_text` = @diagnosis_text " +
       "WHERE `diagnosis_id` = @diagnosis_id";


                foreach (Diagnosis diagnosi in diagnosis)
                {
                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;

                        if (diagnosi.DiagnosisId == 0)
                        {
                            command.CommandText = insertQuery;
                        }
                        else
                        {
                            command.CommandText = updateQuery;
                            command.Parameters.AddWithValue("@diagnosis_id", diagnosi.DiagnosisId);
                        }

                        command.Parameters.AddWithValue("@appointment_id", diagnosi.AppointmentId);
                        command.Parameters.AddWithValue("@diagnosis_text", diagnosi.DiagnosisText);

                        try
                        {
                            Task<int> task = command.ExecuteNonQueryAsync();
                            int rowsAffected = await task;
                            Console.WriteLine($"Количество затронутых строк: {rowsAffected}");
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex.ToString());
                            return false;
                        }

                    }

                }
                return true;
            }
        }
        public static async Task<bool> SetPatientsToDatabase(List<Patient> patients)
        {
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string insertQuery = "INSERT INTO `patients` (`first_name`, `last_name` , `date_of_birth` , `gender` ,`phone_number` ,`address` ) " +
                    "VALUES (@first_name, @last_name ,@date_of_birth ,@gender ,@phone_number ,@address )";

                string updateQuery = "UPDATE `patients` SET `first_name` = @first_name, `last_name` = @last_name ,`date_of_birth` = @date_of_birth , `gender` = @gender , `phone_number` = @phone_number , `address` = @address " +
                         "WHERE `patient_id` = @patient_id";


                foreach (Patient patient in patients)
                {



                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;

                        if (patient.PatientId == 0)
                        {
                            command.CommandText = insertQuery;
                        }
                        else
                        {
                            command.CommandText = updateQuery;
                            command.Parameters.AddWithValue("@patient_id", patient.PatientId);
                        }

                        command.Parameters.AddWithValue("@first_name", patient.FirstName);
                        command.Parameters.AddWithValue("@last_name", patient.LastName);
                        command.Parameters.AddWithValue("@date_of_birth", patient.DateOfBirth);
                        command.Parameters.AddWithValue("@gender", patient.Gender);
                        command.Parameters.AddWithValue("@phone_number", patient.PhoneNumber);
                        command.Parameters.AddWithValue("@address", patient.Address);

                        try
                        {
                            Task<int> task = command.ExecuteNonQueryAsync();
                            int rowsAffected = await task;
                            Console.WriteLine($"Количество затронутых строк: {rowsAffected}");
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex.ToString());
                            return false;
                        }

                    }
                }
                return true;
            }
        }
        public static async Task<bool> SetDepartmentsToDatabase(List<Department> departments)
        {
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string insertQuery = "INSERT INTO `departments` (`name`, `location`) " +
                    "VALUES (@name, @location)";

                string updateQuery = "UPDATE `departments` SET `name` = @name, `location` = @location " +
                         "WHERE `department_id` = @department_id";


                foreach (Department department in departments)
                {



                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;

                        if (department.DepartmentId == 0)
                        {
                            command.CommandText = insertQuery;
                        }
                        else
                        {
                            command.CommandText = updateQuery;
                            command.Parameters.AddWithValue("@department_id", department.DepartmentId);
                        }

                        command.Parameters.AddWithValue("@name", department.Name);
                        command.Parameters.AddWithValue("@location", department.Location);

                        try
                        {
                            Task<int> task = command.ExecuteNonQueryAsync();
                            int rowsAffected = await task;
                            Console.WriteLine($"Количество затронутых строк: {rowsAffected}");
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex.ToString());
                            return false;
                        }
                       
                    }
                }
                return true;
            }
        }
        public static async Task<bool>  SetDoctorsToDatabase(List<Doctor> doctors)
        {
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string insertQuery = "INSERT INTO `doctors` (`first_name`, `last_name`, `specialization`, `phone_number` , `mobile_number` , `address`) " +
                    "VALUES (@FirstName, @LastName, @Specialization, @PhoneNumber, @mobile_number, @address)";

                string updateQuery = "UPDATE `doctors` SET `first_name` = @FirstName, `last_name` = @LastName, `mobile_number` = @mobile_number, `address` = @address, " +
                    "`specialization` = @Specialization, `phone_number` = @PhoneNumber WHERE `doctor_id` = @DoctorId";

                foreach (Doctor doctor in doctors)
                {
                 


                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;

                        if (doctor.DoctorId == 0)
                        {
                            // Insert new doctor
                            command.CommandText = insertQuery;
                        }
                        else
                        {
                            // Update existing doctor
                            command.CommandText = updateQuery;
                            command.Parameters.AddWithValue("@DoctorId", doctor.DoctorId);
                        }

                        command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                        command.Parameters.AddWithValue("@LastName", doctor.LastName);
                        command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                        command.Parameters.AddWithValue("@PhoneNumber", doctor.PhoneNumber);
                        command.Parameters.AddWithValue("@mobile_number", doctor.mobile_number);
                        command.Parameters.AddWithValue("@address", doctor.address);

                        try
                        {
                            Task<int> task = command.ExecuteNonQueryAsync();
                            int rowsAffected = await task;
                            Console.WriteLine($"Количество затронутых строк: {rowsAffected}");
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex.ToString());
                            return false;
                        }

                    }
                }
                return true;
            }
        }
        public static async Task<bool> SetPrescriptionsToDatabase(List<Prescription> prescriptions)
        {
            using (MySqlConnection connection = new MySqlConnection(connStr))
            {
                connection.Open();

                string insertQuery = "INSERT INTO `prescriptions` (`appointment_id`, `medication`, `dosage`) " +
                    "VALUES (@appointment_id, @medication, @dosage)";

                string updateQuery = "UPDATE `prescriptions` SET `appointment_id` = @appointment_id, `medication` = @medication, " +
                    "`dosage` = @dosage WHERE `prescription_id` = @prescription_id";

                foreach (Prescription prescription in prescriptions)
                {



                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Connection = connection;

                        if (prescription.PrescriptionId == 0)
                        {
                            // Insert new doctor
                            command.CommandText = insertQuery;
                        }
                        else
                        {
                            // Update existing doctor
                            command.CommandText = updateQuery;
                            command.Parameters.AddWithValue("@prescription_id", prescription.PrescriptionId);
                        }

                        command.Parameters.AddWithValue("@appointment_id", prescription.AppointmentId);
                        command.Parameters.AddWithValue("@medication", prescription.Medication);
                        command.Parameters.AddWithValue("@dosage", prescription.Dosage);

                        try
                        {
                            Task<int> task = command.ExecuteNonQueryAsync();
                            int rowsAffected = await task;
                            Console.WriteLine($"Количество затронутых строк: {rowsAffected}");
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex.ToString());
                            return false;
                        }

                    }
                }
                return true;
            }

        }

    }
}
