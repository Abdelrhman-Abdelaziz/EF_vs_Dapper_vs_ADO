using BenchmarkDotNet.Attributes;
using EF_vs_Dapper_vs_ADO.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_vs_Dapper_vs_ADO.BenchMark
{
    public class DataAcessBenchMark
    {
        [Benchmark]
        public  void GetAllEmployeesUsingEF()
        {
            IUnitOfWork unitOfWork = new EF.Repositories.UnitOfWork(new EF.AppDBContext());
            var _ = unitOfWork.Employees.GetAllAsync().Result;
            Console.WriteLine($"Number of Employees is {_?.Count()}");
        }
        [Benchmark]
        public void GetAllEmployeesUsingDapper()
        {
            IUnitOfWork unitOfWork = new Dapper.UnitOfWork();
            var _ = unitOfWork.Employees.GetAllAsync().Result;
            Console.WriteLine($"Number of Employees is {_?.Count()}");
        }
        [Benchmark]
        public void GetAllEmployeesUsingADO()
        {
            IUnitOfWork unitOfWork = new ADO.UnitOfWork();
            var _ = unitOfWork.Employees.GetAllAsync().Result;
            Console.WriteLine($"Number of Employees is {_?.Count()}");

        }

    }
}
