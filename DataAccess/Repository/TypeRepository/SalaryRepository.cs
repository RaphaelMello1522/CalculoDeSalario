using DataAccess.Data;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.TypeRepository
{
    public class SalaryRepository : GenericRepository<Salary>, ISalaryRepository
    {
        public SalaryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Salary> GetSalaries()
        {
            var salaryList = new List<Salary>();
            var salary = new Salary();
            var salaryContext = context.Salaries.Include("People").Include("People.Cargo");

            foreach (var item in salaryContext)
            {
                salary.Id = item.Id;
                salary.TimeWorkStart = item.TimeWorkStart;
                salary.TimeWorkEnd = item.TimeWorkEnd;
                salary.TotalTimeWorked = item.TimeWorkEnd - item.TimeWorkStart;
                salary.Total = salary.TotalTimeWorked.TotalHours * Convert.ToDouble(item.People.Cargo.ValueHour);

                salaryList.Add(item);
            }

            return salaryList;
        }
    }
}
