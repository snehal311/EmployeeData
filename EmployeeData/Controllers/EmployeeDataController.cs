using EmployeeBAL;
using EmployeeDAL;
using EmployeeData.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace EmployeeData.Controllers
{
    public class EmployeeDataController : Controller
    {
        IEmployeeService service = new EmployeeService();
       
        public ActionResult ListEmployee()
        {
            try
            {
                return View(service.GetAllEmployees().ToList());
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please contact support.");
                return View("error");
            }
            
        }
        public ActionResult Create()
        {
            try
            {
                EmployeesDetails employeesDetails = new EmployeesDetails();
                var regions = service.GetRegions();
                ViewBag.Regions = new SelectList(regions, "RegionId", "RegionName");

                return View(employeesDetails);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please contact support.");
                return View("error");
            }
            
        }

        [HttpPost]
        public ActionResult Create(EmployeesDetails employees)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Employee emp = new Employee()
                    {
                        EmpName = employees.EmpName,
                        Designation = string.Join("", employees.Designation),
                        PhoneNo = employees.PhoneNo,
                        Email = employees.Email,
                        RegionId = employees.RegionId,
                        ZoneId = employees.ZoneId,
                        BranchId = employees.BranchId,
                        Gender = employees.Gender,
                        DateOfBirth = employees.DateOfBirth,
                        Address = employees.Address,
                        Technology = string.Join(",", employees.Technology),
                        CreatedOn = DateTime.Now
                    };
                    service.AddEmployees(emp);

                    foreach (var doc in employees.Documents)
                    {
                        var document = new EmpDocument()
                        {
                            DocName = doc.FileName,
                            DocPath = SaveToPhysicalLocation(doc),
                            EmpId = emp.EmpId,
                            CreatedOn = DateTime.Now
                        };
                        service.AddEmployeeDoc(document);
                    }
                    ViewData["Message"] = "Record saved successfully";
                    return RedirectToAction("ListEmployee");
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred. Please contact support.");
                    TempData["Message"] = "Please fill all form field";
                    TempData.Keep();
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please contact support.");
                return View("error");
            }

        }

        public ActionResult GetZones(int regionId)
        {
            try
            {
                var zones = service.GetZoneById(regionId);
                return Json(zones, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please contact support.");
                return View("error");
            }
            
        }

        public ActionResult GetBranches(int zoneId)
        {
            try
            {
                var branches = service.GetBranchById(zoneId);
                return Json(branches, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please contact support.");
                return View("error");
            }
        }

        public ActionResult DownloadDoc(int docId)
        {
            try
            {
                var document = service.GetDocumentById(docId);
                if (document != null)
                {
                    byte[] fileBytes;

                    using (var fileStream = new FileStream(document.DocPath, FileMode.Open, FileAccess.Read))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            fileStream.CopyTo(memoryStream);
                            fileBytes = memoryStream.ToArray();
                        }
                    }
                    string fileName = document.DocName;
                    return File(fileBytes, "application/octet-stream", fileName);
                }
                else
                {
                    return HttpNotFound();
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please contact support.");
                return View("error");
            }
            
        }

        public ActionResult ExportToExcel()
        {
            try
            {
                var employees = service.GetAllEmployees();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employees");

                    worksheet.Cells[1, 1].Value = "ID";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "Designation";
                    worksheet.Cells[1, 4].Value = "Phone no";
                    worksheet.Cells[1, 5].Value = "Email";
                    worksheet.Cells[1, 6].Value = "Region";
                    worksheet.Cells[1, 7].Value = "Zone";
                    worksheet.Cells[1, 8].Value = "Branch";
                    worksheet.Cells[1, 9].Value = "Gender";
                    worksheet.Cells[1, 10].Value = "DateOfBirth";
                    worksheet.Cells[1, 11].Value = "Address";
                    worksheet.Cells[1, 12].Value = "Technology";


                    int row = 2;
                    foreach (var employee in employees)
                    {
                        worksheet.Cells[row, 1].Value = employee.EmpId;
                        worksheet.Cells[row, 2].Value = employee.EmpName;
                        worksheet.Cells[row, 3].Value = employee.Designation;
                        worksheet.Cells[row, 4].Value = employee.PhoneNo;
                        worksheet.Cells[row, 5].Value = employee.Email;
                        worksheet.Cells[row, 6].Value = employee.Region.RegionName;
                        worksheet.Cells[row, 7].Value = employee.Zone.ZoneName;
                        worksheet.Cells[row, 8].Value = employee.Branch.BranchName;
                        worksheet.Cells[row, 9].Value = employee.Gender;
                        worksheet.Cells[row, 10].Value = employee.DateOfBirth;
                        worksheet.Cells[row, 11].Value = employee.Address;
                        worksheet.Cells[row, 12].Value = employee.Technology;
                        row++;
                    }

                    // Auto-fit columns for better readability
                    worksheet.Cells.AutoFitColumns();

                    // Convert the Excel package to a byte array
                    byte[] fileBytes = package.GetAsByteArray();

                    // Return the Excel file for download
                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please contact support.");
                return View("error");
            }
            
        }

        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                    file.SaveAs(path);
                    return path;
                }

                return string.Empty;
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please contact support.");
                return "Exception occurred";
            }
        }

        public ActionResult Error()
        {
            var err = TempData["Message"];
            ViewBag.Error = err;
            return View("error");
        }
    }
}