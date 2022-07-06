using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TopoChallenge.Helpers;
using TopoChallenge.Interfaces;
using TopoChallenge.Models;

namespace TopoChallenge.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IDBHelper dbHelper;

        public UserController(IConfiguration _configuration, IDBHelper dbHelper)
        {
            Configuration = _configuration;
            this.dbHelper = dbHelper;
        }

        [HttpPost]
        [Route("savecontact")]
        public async Task<ActionResult<bool>> SaveContactDetails([FromBody] ContactDetails contactDetails)
        {
            if (contactDetails == null)
                return BadRequest();

            try
            {
                contactDetails.Id = new Guid();
                contactDetails = await dbHelper.SaveContactDetails(contactDetails);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return null;
        }

        [HttpGet]
        [Route("contacts")]
        public async Task<ActionResult<List<ContactDetails>>> GetContactDetails()
        {
            List<ContactDetails> contacts;
            try
            {
                contacts = await dbHelper.GetContactDetails();

                if(contacts == null)
                {
                    return BadRequest("Something went wrong!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return contacts;
        }

        [HttpGet]
        [Route("searchcontact/{searchString}")]
        public async Task<ActionResult<List<ContactDetails>>> SearchContactDetails(string searchString)
        {
            List<ContactDetails> contacts;
            try
            {
                contacts = await dbHelper.SearchContactDetails(searchString);

                if (contacts == null)
                {
                    return BadRequest("Something went wrong!!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return contacts;
        }
    }
}
