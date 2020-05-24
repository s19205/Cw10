using Cw10.DTOs.Requests;
using Cw10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw10.Services
{
    public class MsSqlDbService : IDbService
    {
        private readonly CodeFirstContext dbContext;

        public MsSqlDbService(CodeFirstContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddDoctor(AddDoctorRequest request)
        {
            try
            {
                dbContext.Add(new Doctor
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email
                });
                dbContext.SaveChanges();
                return true;

            } catch(Exception ex)
            {
                return false;
            }
        }

        public bool DeleteDoctor(int IdDoctor)
        {
            try
            {
                var doctor = dbContext.Doctors.Where(e => e.IdDoctor == IdDoctor).FirstOrDefault();
                if (doctor == null)
                {
                    return false;
                }
                dbContext.Doctors.Remove(doctor);

                if (!dbContext.Prescriptions.Any())
                {
                    return true;
                }

                var prescription = dbContext.Prescriptions.Where(p => p.IdDoctor == doctor.IdDoctor).ToList();

                foreach(var el in prescription)
                {
                    dbContext.Prescriptions.Remove(el);

                    if (dbContext.Prescription_Medicaments.Any())
                    {
                        var pm = dbContext.Prescription_Medicaments.Where(e => e.IdPrescription == el.IdPrescription);
                        if (pm != null && pm.Count() > 0)
                        {
                            foreach(var value in pm)
                            {
                                dbContext.Remove(value);
                            }
                        }
                    }
                }
                dbContext.SaveChanges();
                return true;


            } catch(Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            try
            {
                if (!dbContext.Doctors.Any())
                {
                    return null;
                }
                var res = dbContext.Doctors.ToList();
                return res;
            } catch(Exception ex)
            {
                return null;
            }
        }

        public bool UpdateDoctor(UpdateDoctorRequest request)
        {
            try
            {
                var el = dbContext.Doctors.Where(e => e.IdDoctor == request.IdDoctor).FirstOrDefault();
                if(el == null)
                {
                    return false;
                }

                el.FirstName = request.FirstName;
                el.LastName = request.LastName;
                el.Email = request.Email;
                dbContext.SaveChanges();
                return true;

            } catch(Exception ex)
            {
                return false;
            }
        }
    }
}
