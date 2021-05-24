using BethanysPieShopHRM.App.Components;
using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeOverview
    {

        public IEnumerable<Employee> Employees { get; set; }

        [Inject]
        public IEmployeeDataService _employeeDataService { get; set; }

        protected AddEmployeeDialog AddEmployeeDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetAllEmployees();
        }

        protected async Task AddEmployeeDialog_OnDialogClose()
        {
            await GetAllEmployees();
            StateHasChanged();
        }

        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        private async Task GetAllEmployees()
        {
            Employees = (await _employeeDataService.GetAllEmployees()).ToList();
        }
    }
}
