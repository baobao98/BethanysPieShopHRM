using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Pages
{
    public class IndexBase : ComponentBase
    {
        public Employee employee = new Employee() { FirstName = "bao", LastName = "lam" };
    }
}
