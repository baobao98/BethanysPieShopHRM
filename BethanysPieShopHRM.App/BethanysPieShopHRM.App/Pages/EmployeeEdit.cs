using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeEdit
    {
        [Parameter]
        public string EmployeeId { get; set; }

        [Inject]
        public IEmployeeDataService _employeeDataService { get; set; }
        [Inject]
        public ICountryDataService _countryDataService { get; set; }
        [Inject]
        public IJobCategoryDataService _jobCategoryDataService { get; set; }  
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        protected string CountryId = string.Empty;
        protected string JobCategoryId = string.Empty;

        // used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Countries = (await _countryDataService.GetAllCountries()).ToList();
            JobCategories = (await _jobCategoryDataService.GetAllJobCategories()).ToList();

            int.TryParse(EmployeeId, out var employeeId);

            if (employeeId == 0)
            {
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
            }
            else
            {
                Employee = await _employeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            }

            CountryId = Employee.CountryId.ToString(); // InputSelect does not support the type System Int32. Fixed in .Net 5.0
            JobCategoryId = Employee.JobCategoryId.ToString(); // InputSelect does not support the type System Int32. Fixed in .Net 5.0
        }

        protected async Task HandleValidSubmit()
        {
            Saved = false;
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            if (Employee.EmployeeId == 0)
            {
                var addedEmployee = await _employeeDataService.AddEmployee(Employee);
                if(addedEmployee is object)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                var updatedEmployee = await _employeeDataService.UpdateEmployee(Employee);
                if (updatedEmployee is object)
                {
                    StatusClass = "alert-success";
                    Message = "Employee updated successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong updating the employee. Please try again.";
                    Saved = false;
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected async Task DeleteEmployee()
        {
            await _employeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverView()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }
    }
}
