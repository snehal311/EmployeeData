using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace EmployeeDAL
{
    public interface IEmployeeRepo
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
