﻿using Northwind.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.DataAcess
{
    public interface IEmployeeRepository
    {
        IList<Employee> GetEmployees();

        Employee GetEmployeeById(int id);

        Employee GetEmployeeByName(string name);

        Employee GetEmployeeByInitials(string initials);

        void AddEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        IList<Employee> GetEmployeesFiltered(string country, string title, string region, string firstName, string lastName);

    }
}
