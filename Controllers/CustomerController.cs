using Bedirhan_Hafta_1.FakeData;
using Bedirhan_Hafta_1.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bedirhan_Hafta_1.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private List<Customer> _customer = Fdata.GetCustomer();//Bogus eklentesi yardımı ile Customer türünden nesne listesi oluşturuluyor.
    
        [HttpGet]//Standart gelen get isteklerini karşılayıp customer listesini dönen
                 //veya varsa sorgulara göre filtreleme ve sıralama yapan metod.
        public IActionResult Get([FromQuery] int? id, [FromQuery] string? name,[FromQuery] string? sort="asc")
        {
            if(name==null && id==null && sort == "desc") 
                return Ok(_customer.OrderByDescending(x => x.Id));
            if (name == null && id == null && sort == "asc")
                return Ok(_customer);
            var query = _customer.Where(x => x.Name == name || x.Id == id);
            if (!query.Any()) 
                return NotFound();
            return Ok(query);
        }

      

        [HttpPost]//Post isteğini karşılayıp gönderilen değeri body'de yakalayıp listeye ekleyen metod
        public IActionResult Post([FromBody] Customer customer)
        {
            _customer.Add(customer);
            return Created("Get", customer);
        }

        [HttpDelete("{id}")]//Silme işlemi
        public IActionResult Delete(int id)
        {
            var DeletedCustomer = _customer.FirstOrDefault(x => x.Id == id);
            _customer.Remove(DeletedCustomer);
            return Ok(DeletedCustomer);
        }

        [HttpPut]//Put isteğini karşılayıp id değerine göre güncellenen değeri belirleyip,
                 //gelen değerleri eski değerlerle değiştiren metod.
        public IActionResult Put([FromBody] Customer customer)
        {
            var UpdatedCustomer = _customer.FirstOrDefault(x => x.Id == customer.Id);
            if (UpdatedCustomer == null) return NotFound();
            UpdatedCustomer.Name = customer.Name;
            UpdatedCustomer.Surname = customer.Surname;
            UpdatedCustomer.TelNumber = customer.TelNumber;
            UpdatedCustomer.Adress = customer.Adress;
            UpdatedCustomer.Gender = customer.Gender;
            return Ok(UpdatedCustomer);
        }

        [HttpPatch("{id}")]//Eğer sadece "surname" değeri değiştirlemek istenirse
                           //diğer değerleri gönderme maliyetine girmeden "surname" değiştiren metod.
        public IActionResult Patch(int id,[FromBody] JsonPatchDocument customer)
        {
            var UpdatedCustomer = _customer.FirstOrDefault(x => x.Id == id);
            if (UpdatedCustomer == null) return NotFound();
            customer.ApplyTo(UpdatedCustomer);
            return Ok();
        }



    }
}
