using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Components
{
    public partial class AddEmployeeDialog : ComponentBase
    {
        public Employee Employee { get; set; } = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };


        [Inject]
        public IEmployeeDataService _employeeDataService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }


        public bool ShowDialog { get; set; }

        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        public async Task HandleValidSubmitAsync()
        {
            var addedEmployee = await _employeeDataService.AddEmployee(Employee);
            ShowDialog = false;

            if (addedEmployee is object)
            {
                await CloseEventCallback.InvokeAsync(true);
            }
            else
            {
                await CloseEventCallback.InvokeAsync(false);
            }

            StateHasChanged();
        }

        private void ResetDialog()
        {
            Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
        }
    };
}
