using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.ComponentsLibrary.Map;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeDetail
    {
        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        public IEnumerable<Employee> Employees { get; set; }
        public List<Marker> MapMarkers { get; set; } = new List<Marker>();

        [Inject]
        public IEmployeeDataService _employeeDataService { get; set; }

        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employee = await _employeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            MapMarkers = new List<Marker>
            {
                new Marker{Description = $"{Employee.FirstName} {Employee.LastName}", ShowPopup = false, X = Employee.Longitude, Y = Employee.Latitude}
            };
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await jSRuntime.InvokeVoidAsync(
                "testJSInterop");
        }
    }
}
