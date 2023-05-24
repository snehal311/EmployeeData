
using EmployeeDAL.CustomException;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL
{
    public class EmployeeRepository : IEmployeeRepo
    {
        EmployeeDetailDBEntities entities= new EmployeeDetailDBEntities();
        public EmployeeRepository()
        {
            
        }
        public void AddEmployees(Employee employee)
        {
            try
            {
                    entities.Employees.Add(employee);
                    entities.SaveChanges();
            }
            catch(SqlException ex)
            {
                throw new DataAccessException("Failed to add employee to the database.", ex);
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                return entities.Employees.ToList();
            }
            catch(SqlException ex)
            {
                throw new DataAccessException("Failed to get all employees from database", ex);
            }
        }

        public IEnumerable<Region> GetRegions()
        {
            try
            {
                return entities.Regions.ToList();
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Failed to get all regions from database", ex);
            }
        }
        public IEnumerable<Branch> GetBranchById(int zoneId)
        {
            entities.Configuration.ProxyCreationEnabled = false;
            try
            {
                return entities.Branches.Where(branch => branch.ZoneId == zoneId).ToList();
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Failed to get brach by Id from database", ex);
            }
        }

        public IEnumerable<Zone> GetZoneById(int regionId)
        {
            entities.Configuration.ProxyCreationEnabled = false;
            try
            {
                return entities.Zones.Where(zone => zone.RegionId == regionId).ToList<Zone>();
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Failed to get zone by Id from database", ex);
            }
        }

        public void AddEmployeeDoc(EmpDocument document)
        {
            try
            {
                entities.EmpDocuments.Add(document);
                entities.SaveChanges();
            }
            catch(SqlException ex)
            {
                throw new DataAccessException("Failed to add employee to the database", ex);
            }
        }

        public IEnumerable<EmpDocument> GetEmpDocuments()
        {
            try
            {
                return entities.EmpDocuments.ToList();
            }
            catch(SqlException ex)
            {
                throw new DataAccessException("Failed to get documents details from database", ex);
            }
        }

        public EmpDocument GetDocumentById(int docId)
        {
            try
            {
                return entities.EmpDocuments.Where(doc=>doc.DocId== docId).FirstOrDefault();
            }
            catch(SqlException ex) 
            {
                throw new DataAccessException("Failed to get document details by Id from database", ex);
            }
        }
    }
}
