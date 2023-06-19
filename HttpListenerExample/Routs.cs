using HttpListenerExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpListenerExample
{
    internal static  class Routs
    {
        public static async Task ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            Console.WriteLine();
            Console.WriteLine("Request: {0} {1}", request.HttpMethod, request.Url);
            Console.WriteLine("Client Login: {0}", request.Headers["Authorization"]);
            Console.WriteLine("Client IP Address: {0}", request.RemoteEndPoint.Address);


            if (!DatabaseManager.IsClientAuthorized(request))
            {
                ResponseManager.WriteUnauthorizedResponse(response);
                response.Close();
                return;
            }

            if (request.HttpMethod == "POST")
            {
                if (request.Url.AbsolutePath == "/getAppointments")
                {
                    List<Appointment> appointments = DatabaseManager.GetAppointmentsFromDatabase();
                    await ResponseManager.SendResponseData(context, appointments);
                }
                else if (request.Url.AbsolutePath == "/getDiagnosis")
                {
                    List<Diagnosis> diagnosis = DatabaseManager.GetDiagnosisFromDatabase();
                    await ResponseManager.SendResponseData(context, diagnosis);
                }
                else if (request.Url.AbsolutePath == "/getPatient")
                {
                    List<Patient> patients = DatabaseManager.GetPatientsFromDatabase();
                    await ResponseManager.SendResponseData(context, patients);
                }
                else if (request.Url.AbsolutePath == "/getDepartment")
                {
                    List<Department> departments = DatabaseManager.GetDepartmentsFromDatabase();
                    await ResponseManager.SendResponseData(context, departments);
                }
                else if (request.Url.AbsolutePath == "/getDoctors")
                {
                    List<Doctor> doctor = DatabaseManager.GetDoctorsFromDatabase();
                    await ResponseManager.SendResponseData(context, doctor);
                }
                else if (request.Url.AbsolutePath == "/getPrescription")
                {
                    List<Prescription> prescription = DatabaseManager.GetPrescriptionsFromDatabase();
                    await ResponseManager.SendResponseData(context, prescription);
                }
                

                /////////////////////////////////////

                else if (request.Url.AbsolutePath == "/setAppointments")
                {
                    List<Appointment> appointments = await RequestManager.GetRequestData<List<Appointment>>(context.Request);
                    if (await DatabaseManager.SetAppointmentsToDatabase(appointments))
                    {
                        List<Appointment> appointment = DatabaseManager.GetAppointmentsFromDatabase();
                        await ResponseManager.SendResponseData(context, appointment);
                        await Console.Out.WriteLineAsync(" Stat 200");
                    }

                }
                else if (request.Url.AbsolutePath == "/setDiagnosis")
                {
                    List<Diagnosis> diagnosis = await RequestManager.GetRequestData<List<Diagnosis>>(context.Request);

                    if (await DatabaseManager.SetDiagnosisToDatabase(diagnosis))
                    {
                        List<Diagnosis> diagnosi = DatabaseManager.GetDiagnosisFromDatabase();
                        await ResponseManager.SendResponseData(context, diagnosi);
                        await Console.Out.WriteLineAsync(" Stat 200");
                    }

                }
                else if (request.Url.AbsolutePath == "/setPatients")
                {
                    List<Patient> patients = await RequestManager.GetRequestData<List<Patient>>(context.Request);

                    if (await DatabaseManager.SetPatientsToDatabase(patients))
                    {
                        List<Patient> patient = DatabaseManager.GetPatientsFromDatabase();
                        await ResponseManager.SendResponseData(context, patient);
                        await Console.Out.WriteLineAsync(" Stat 200");
                    }

                }
                else if (request.Url.AbsolutePath == "/setDepartments")
                {
                    List<Department> departments = await RequestManager.GetRequestData<List<Department>>(context.Request);

                    if (await  DatabaseManager.SetDepartmentsToDatabase(departments))
                    {
                        List<Department> department = DatabaseManager.GetDepartmentsFromDatabase();
                        await ResponseManager.SendResponseData(context, department);
                        await Console.Out.WriteLineAsync(" Stat 200");
                    }
                }
                else if (request.Url.AbsolutePath == "/setDoctors")
                {
                    List<Doctor> doctors = await RequestManager.GetRequestData<List<Doctor>>(context.Request);

                    if ( await DatabaseManager.SetDoctorsToDatabase(doctors))
                    {
                        List<Doctor> doctor = DatabaseManager.GetDoctorsFromDatabase();
                        await ResponseManager.SendResponseData(context, doctor);
                        await Console.Out.WriteLineAsync(" Stat 200");
                    }

                }
                else if (request.Url.AbsolutePath == "/setPrescriptions")
                {
                    List<Prescription> prescriptions = await RequestManager.GetRequestData<List<Prescription>>(context.Request);

                    if (await DatabaseManager.SetPrescriptionsToDatabase(prescriptions))
                    {
                        List<Prescription> prescription = DatabaseManager.GetPrescriptionsFromDatabase();
                        await ResponseManager.SendResponseData(context, prescription);
                        await Console.Out.WriteLineAsync(" Stat 200");
                    }
                }

            }

            response.Close();
        }
    }
}
