using System.Collections.Generic;
using System.Threading.Tasks;
using TopoChallenge.Models;

namespace TopoChallenge.Interfaces
{
    public interface IDBHelper
    {
        Task<List<ContactDetails>> GetContactDetails();
        Task<ContactDetails> SaveContactDetails(ContactDetails contactDetails);
        Task<List<ContactDetails>> SearchContactDetails(string searchString);
    }
}