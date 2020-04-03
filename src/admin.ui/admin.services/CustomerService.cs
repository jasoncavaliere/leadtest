using System;
using System.Collections.Generic;
using System.Text;

namespace admin.services
{
    public class Lead
    {
        public Guid LeadId { get; set; }
        public string Name { get; set; }
        public string State{ get; set; }
    }

    public class PagedResult<T>
    {
        public T[] Results { get; set; }

        public int TotalRecords { get; set; }

        public int Pages { get; set; }
        public int PerPage  { get; set; }
    }


    public class CustomerService:ICustomerService
    {
        public PagedResult<Lead> Leads(int page,int perPage)
        {
            return new PagedResult<Lead>{Results = new Lead[]
            {
                new Lead{LeadId = Guid.NewGuid(),Name = "Lead 1",State = "New"},
                new Lead{LeadId = Guid.NewGuid(),Name = "Lead 2",State = "New"},
                new Lead{LeadId = Guid.NewGuid(),Name = "Lead 3",State = "New"},
                new Lead{LeadId = Guid.NewGuid(),Name = "Lead 4",State = "New"},
                new Lead{LeadId = Guid.NewGuid(),Name = "Lead 5",State = "New"},
            },
                Pages = 5,
                TotalRecords = 50,
                PerPage =10
            };
        }

        public Lead LeadDetail(Guid leadId)
        {
            
            return  new Lead
            {
                Name = "Name",
                LeadId = leadId,
                State = "New"
            };
        }
    }

    public interface ICustomerService
    {
        PagedResult<Lead> Leads(int page,int perPage);
        Lead LeadDetail(Guid leadId);
    }
}
