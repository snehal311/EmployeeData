using EmployeeDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBAL
{
    public interface IEmployeeService
    {
        void AddEmployees(Employee employee);

        IEnumerable<Region> GetRegions();
        IEnumerable<Zone> GetZoneById(int regionId);

        IEnumerable<Branch> GetBranchById(int zoneId);

        IEnumerable<Employee> GetAllEmployees();
        void AddEmployeeDoc(EmpDocument document);
        IEnumerable<EmpDocument> GetEmpDocuments();

        EmpDocument GetDocumentById(int docId);
    }
}
