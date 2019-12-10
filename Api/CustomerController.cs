using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CustomerManager.Models;
using CustomerManager.Dtos;
using AutoMapper;


namespace CustomerManager.Controllers.Api
{
    [AllowAnonymous]
    public class CustomerController : ApiController
    {
        private ApplicationDbContext db;
        public CustomerController()
        {
            db = new ApplicationDbContext();
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {

            var customers = db.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
            return customers;
        }

        public IHttpActionResult GetCustomer(int Id)
        {
            var customer = db.Customers.ToList().SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return BadRequest("The Customer was not found");
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        //post /api/customer/1
        
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Ensure the required fields are filled");
            //next lines maps properties in customerdto model to our domain object customer
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            //next line is to now add the mapped properties to the db
            var addcustomer = db.Customers.Add(customer);
            db.SaveChanges();
            //to return the database generated Id to the client
            customerDto.Id = customer.Id;
            //return the Uri of the newly created resource to the client
            return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);


        }

        [Authorize(Roles = "CanManageCustomers")]
        [HttpPut]
        public IHttpActionResult UpdateCustomer(CustomerDto customerDto)//, int Id
        {
            //update requires loading, editing b4 updating
            if (!ModelState.IsValid)
                return BadRequest("Ensure the required fields are filled");
            //next line loads the customer with the Id we passed in our parameter
            var customerInDb = db.Customers.SingleOrDefault(c => c.Id == customerDto.Id);
            //next line checks if a customer with the Id we passed exists in the db
            if (customerInDb == null)
                BadRequest("Customer with such Id doesnt exist");
            //next lines does the editing, it maps properties in customerdto model to our domain object customer comparing the values we retrieved so it can make the changes
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            db.SaveChanges();
            return Ok("Records Updated successfully");
        }

        [Authorize(Roles = "CanManageCustomers")]
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = db.Customers.SingleOrDefault(c => c.Id == id);
            //next line checks if a customer with the Id we passed exists in the db
            if (customerInDb == null)
                BadRequest("Customer with such Id doesnt exist");
            //next line removes the customer
            var deleteCustomer = db.Customers.Remove(customerInDb);
            db.SaveChanges();
        }
    }
}
