using System;
using EmployeeDetailsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeDetailsAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        TestDBEntities db = new TestDBEntities();

        //[HttpGet]
        //[Route("AllEmployeeDetails")]
        //public IQueryable<EmployeeDetail> GetEmployee()
        //{
        //    try
        //    {
        //        return db.EmployeeDetails;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        [HttpGet]
        [Route("AllEmployeeDetails")]
        public IHttpActionResult GetEmployee()
        {
            List<EmployeeDetail> emp = new List<EmployeeDetail>();
            try
            {
                emp = db.EmployeeDetails.ToList();
                if (emp == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(emp);
        }

        [HttpGet]
        [Route("GetStateList")]
        public IHttpActionResult GetAllStates()
        {
            List<State> st = new List<State>();
            try
            {
                st = db.States.ToList();
                if (st == null)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(st);
        }

        [HttpGet]
        [Route("GetEmployeeDetailsById/{employeeId}")]
        public IHttpActionResult GetEmployeeById(string employeeId)
        {
            EmployeeDetail objEmp = new EmployeeDetail();
            int ID = Convert.ToInt32(employeeId);
            try
            {
                objEmp = db.EmployeeDetails.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertEmployeeDetails")]
        public IHttpActionResult InsertEmployee(EmployeeDetail data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.EmployeeDetails.Add(data);
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(data);
        }
        
        [HttpPut]
        [Route("UpdateEmployeeDetails")]
        public IHttpActionResult UpdateEmployee(EmployeeDetail data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                EmployeeDetail objEmp = new EmployeeDetail();
                objEmp = db.EmployeeDetails.Find(data.EmpId);
                if (objEmp != null)
                {
                    objEmp.EmpName = data.EmpName;
                    objEmp.Gender = data.Gender;
                    objEmp.DateOfBirth = data.DateOfBirth;
                    objEmp.State = data.State;
                    objEmp.Address = data.Address;
                    objEmp.Phone = data.Phone;
                    objEmp.EmailId = data.EmailId;
                    objEmp.Language = data.Language;
                    objEmp.Photo = data.Photo;

                }
                int i = this.db.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(data);
        }

        [HttpDelete]
        [Route("DeleteEmployeeDetails")]
        public IHttpActionResult DeleteEmployee(int id)
        {            
            EmployeeDetail employee = db.EmployeeDetails.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.EmployeeDetails.Remove(employee);
            db.SaveChanges();

            return Ok(employee);
        }

    }

    public class EmployeeModel
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int State { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string Language { get; set; }
        public string Photo { get; set; }

    }

    public class NVP
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }

}