﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Northwind.Entities.Models;

namespace Northwind.DataAcess
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NorthwindContext Db;

        public EmployeeRepository()
        {
            Db = new NorthwindContext();
        }

        public void AddEmployee(Employee employee)
        {
            Db.Employees.Add(employee);
            Db.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            var teritories = Db.EmployeeTerritories.Where(t => t.EmployeeId == employee.EmployeeId);
            var orders = Db.Orders.Where(o => o.EmployeeId == employee.EmployeeId);
            var employments = Db.Employments.Where(e => e.EmployeeId == employee.EmployeeId);


            foreach (var item in orders)
            {
                var orderdetails = Db.OrderDetails.Where(o => o.OrderId == item.OrderId);
                Db.OrderDetails.RemoveRange(orderdetails);
            }
            
            
            Db.Orders.RemoveRange(orders);
            Db.EmployeeTerritories.RemoveRange(teritories);
            Db.Employments.RemoveRange(employments);
            Db.Employees.Remove(employee);
            Db.SaveChanges();
        }

        public Employee GetEmployeeById(int id)
        {
            return Db.Employees
                .Include(r => r.ReportsToNavigation)
                .Include(em => em.Employments)
                .SingleOrDefault(e => e.EmployeeId == id);
        }

        public Employee GetEmployeeByInitials(string initials)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeByName(string name)
        {
            return Db.Employees.SingleOrDefault(e => e.FirstName == name);
        }

        public IList<Employee> GetEmployees()
        {
            return Db.Employees
                .Include(Employee => Employee.Employments)
                .ToList();
        }

        public IList<Employee> GetEmployeesFiltered(string country, string title, string region, string firstName, string lastName)
        {
            return Db.Employees.Where(e => e.Country.IndexOf(country) != -1 && e.Title.IndexOf(title) != -1 && e.Region.IndexOf(region) != -1 && e.FirstName.IndexOf(firstName) != -1 && e.LastName.IndexOf(lastName) != -1).ToList();
        }


        public void UpdateEmployee(Employee employee)
        {
            Db.Employees.Update(employee);
            Db.SaveChanges();
        }
    }
}
