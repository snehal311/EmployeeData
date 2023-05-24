using EmployeeBAL.CustomException;
using EmployeeDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBAL
{
    public class EmployeeService : IEmployeeService
    {
        EmployeeRepository repository= new EmployeeRepository();

        public void AddEmployees(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
                }

                if (string.IsNullOrEmpty(employee.EmpName))
                {
                    throw new ArgumentException("Employee name cannot be empty.");
                }
                repository.AddEmployees(employee);
            }
            catch(Exception ex)
            {
                throw new BusinessException("Failed to add employee", ex);
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                return repository.GetAllEmployees();
            }
            catch(Exception ex)
            {
                throw new BusinessException("Failed to load employees details", ex);
            }
        }

        public IEnumerable<Branch> GetBranchById(int zoneId)
        {
            try
            {
                return repository.GetBranchById(zoneId);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed to load branch detail", ex);
            }
        }

        public IEnumerable<Region> GetRegions()
        {
            try
            {
                return repository.GetRegions();
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed to load regions", ex);
            }
            
        }

        public IEnumerable<EmployeeDAL.Zone> GetZoneById(int regionId)
        {
            try
            {
                return repository.GetZoneById(regionId);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed to load zone details", ex);
            }
        }

        public void AddEmployeeDoc(EmpDocument document)
        {
            try
            {
                if (document == null)
                {
                    throw new ArgumentNullException(nameof(document), "Document cannot be null.");
                }

                if (string.IsNullOrEmpty(document.DocName))
                {
                    throw new ArgumentException("Document name cannot be empty.");
                }
                repository.AddEmployeeDoc(document);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed to add documents", ex);
            }
        }

        public IEnumerable<EmpDocument> GetEmpDocuments()
        {
            try
            {
                return repository.GetEmpDocuments();
            }
            catch(Exception ex)
            {
                throw new BusinessException("Failed to load documents", ex);
            }
        }

        public EmpDocument GetDocumentById(int docId)
        {
            try
            {
                return repository.GetDocumentById(docId);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Failed to load document by Id", ex);
            }
        }
    }
}
