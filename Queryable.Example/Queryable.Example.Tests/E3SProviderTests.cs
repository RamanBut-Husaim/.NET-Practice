﻿using System.Configuration;
using System.Linq;
using Queryable.Example.E3SClient;
using Queryable.Example.E3SClient.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Queryable.Example.Tests
{
    public class E3SProviderTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public E3SProviderTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void WithoutProvider()
        {
            var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
            var res = client.SearchFTS<EmployeeEntity>("workstation:(EPBYMINW3594)", 0, 1);

            foreach (var emp in res)
            {
                _outputHelper.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [Fact]
        public void WithoutProviderNonGeneric()
        {
            var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
            var res = client.SearchFTS(typeof(EmployeeEntity), "workstation:(EPBYMINW3594)", 0, 10);

            foreach (var emp in res.OfType<EmployeeEntity>())
            {
                _outputHelper.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [Fact]
        public void WithProvider()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation == "EPBYMINW3594"))
            {
                _outputHelper.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [Fact]
        public void Where_WhenOperandPrecedenceIsInversed_CompletedSuccessfully()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => "EPBYMINW3594" == e.workstation))
            {
                _outputHelper.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
            }
        }

        [Fact]
        public void Where_WhenStartWithOperationIsUsed_QueryIsExecutedSuccessfully()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.StartsWith("EPBYMINW359")))
            {
                _outputHelper.WriteLine("{0} {1} {2}", emp.nativename, emp.startworkdate, emp.workstation);
            }
        }

        [Fact]
        public void Where_WhenEndsWithOperationIsUsed_QueryIsExecutedSuccessfully()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.EndsWith("94")))
            {
                _outputHelper.WriteLine("{0} {1} {2}", emp.nativename, emp.startworkdate, emp.workstation);
            }
        }

        [Fact]
        public void Where_WhenContainsOperationIsUsed_QueryIsExecutedSuccessfully()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(e => e.workstation.EndsWith("594")))
            {
                _outputHelper.WriteLine("{0} {1} {2}", emp.nativename, emp.startworkdate, emp.workstation);
            }
        }

        [Fact]
        public void Where_WhenAndOperatorIsUsed_QueryIsExecutedSuccessfully()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(
                e => e.workstation.Contains("EPBY") &&
                     e.workstation.EndsWith("594") &&
                     e.workstation.Contains("MINW")))
            {
                _outputHelper.WriteLine("{0} {1} {2}", emp.nativename, emp.startworkdate, emp.workstation);
            }
        }

        [Fact]
        public void Where_WhenEqualsAndAndOperatorAreUsed_QueryIsExecutedSuccessfully()
        {
            var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

            foreach (var emp in employees.Where(
                e => e.workstation.Contains("EPBY") &&
                     e.workstation == "EPBYMINW3594"))
            {
                _outputHelper.WriteLine("{0} {1} {2}", emp.nativename, emp.startworkdate, emp.workstation);
            }
        }
    }
}
