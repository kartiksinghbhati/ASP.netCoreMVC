using Microsoft.AspNetCore.Mvc;
using WebApplication_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private static List<Employee> employees = new List<Employee>();

        // GET: Employee
        public IActionResult Index()
        {
            return View(employees);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee, IFormFile photoBase64)
        {
            try
            {
                // Perform validation
                if (string.IsNullOrEmpty(employee.FirstName) || employee.FirstName.Length > 50)
                    throw new Exception("Invalid first name.");

                if (string.IsNullOrEmpty(employee.LastName) || employee.LastName.Length > 50)
                    throw new Exception("Invalid last name.");

                if (employee.DateOfBirth > DateTime.Now.AddYears(-18))
                    throw new Exception("Invalid date of birth.");

                if (employee.Salary < 0)
                    throw new Exception("Invalid salary.");


                if (photoBase64 != null && photoBase64.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        photoBase64.CopyTo(ms);
                        byte[] photoBytes = ms.ToArray();
                        employee.PhotoBase64 = Convert.ToBase64String(photoBytes);
                    }
                }

                if (string.IsNullOrEmpty(employee.Gender))
                    throw new Exception("Please select Gender");

                if (string.IsNullOrEmpty(employee.IsActive))
                    throw new Exception("Please select Status");

                // Add the employee to the list
                employee.Id = employees.Count + 1;
                employees.Add(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee updatedEmployee, IFormFile photoBase64)
        {
            try
            {
                Employee employee = employees.FirstOrDefault(e => e.Id == id);
                if (employee == null)
                    return NotFound();

                // Perform validation
                if (string.IsNullOrEmpty(updatedEmployee.FirstName) || updatedEmployee.FirstName.Length > 50)
                    throw new Exception("Invalid first name.");

                if (string.IsNullOrEmpty(updatedEmployee.LastName) || updatedEmployee.LastName.Length > 50)
                    throw new Exception("Invalid last name.");

                if (updatedEmployee.DateOfBirth > DateTime.Now.AddYears(-18))
                    throw new Exception("Invalid date of birth.");

                if (updatedEmployee.Salary < 0)
                    throw new Exception("Invalid salary.");

                if (photoBase64 != null && photoBase64.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        photoBase64.CopyTo(ms);
                        byte[] photoBytes = ms.ToArray();
                        employee.PhotoBase64 = Convert.ToBase64String(photoBytes);
                    }
                }

                if (string.IsNullOrEmpty(employee.Gender))
                    throw new Exception("Please select Gender");

                if (string.IsNullOrEmpty(employee.IsActive))
                    throw new Exception("Please select Status");

                // Update the employee details
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                employee.DateOfBirth = updatedEmployee.DateOfBirth;
                employee.Salary = updatedEmployee.Salary;
                employee.PhotoBase64 = updatedEmployee.PhotoBase64;
                employee.Gender = updatedEmployee.Gender;
                employee.IsActive = updatedEmployee.IsActive;
                employee.Hobbies = updatedEmployee.Hobbies;

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            Employee employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                employees.RemoveAll(e => e.Id == id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/GetAll
        public ActionResult GetAll()
        {
            return Json(employees);
        }
    }
}

