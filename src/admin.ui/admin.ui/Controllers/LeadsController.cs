using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using admin.services;
using Microsoft.AspNetCore.Mvc;

namespace admin.ui.Controllers
{
    public class LeadsController : Controller
    {
        private readonly ICustomerService _customers;

        public LeadsController(ICustomerService customers)
        {
            _customers = customers;
        }



        public IActionResult Index(int page=1,int perPage =10)
        {
            return View(_customers.Leads(page,perPage));
        }

        public IActionResult Detail(Guid leadId)
        {
            return View(_customers.LeadDetail(leadId));
        }
    }
}