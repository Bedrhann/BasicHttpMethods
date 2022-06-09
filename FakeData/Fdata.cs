using Bedirhan_Hafta_1.Model;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bedirhan_Hafta_1.FakeData
{
    public class Fdata
    {
        //Bogus Eklentisi ile Fake Data üretmemizi sağlayan class
        private static List<Customer> _customer;
        public static List<Customer> GetCustomer()
        {
            if(_customer == null)
            {

                _customer = new Faker<Customer>()
                   .RuleFor(x => x.Id, f => f.IndexFaker + 1)
                   .RuleFor(x => x.Name, f => f.Name.FirstName())
                   .RuleFor(x => x.Surname, f => f.Name.LastName())
                   .RuleFor(x => x.TelNumber, f => f.Phone.PhoneNumberFormat())
                   .RuleFor(x => x.Adress, f => f.Address.FullAddress())
                   .RuleFor(x => x.Gender, f => f.Person.Gender.ToString())
                   .Generate(8);
            }

            return _customer;
        }
    }
}
