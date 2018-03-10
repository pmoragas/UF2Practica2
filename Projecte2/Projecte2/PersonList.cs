
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecte2
{
    class People
    {
        private List<Person> PersonList { get; set; }
        private String jsonPath = "people.json";

        public void readPeopleFromJson()
        {
            using (StreamReader reader = new StreamReader(jsonPath))
            {
                var json = reader.ReadToEnd();
                this.PersonList = JsonConvert.DeserializeObject<List<Person>>(json);   
            }
        }

        public void getPeopleByCountry(string country)
        {
            Parallel.ForEach(array, i =>
            {
                if (IsPrime(i))
                {
                    primes[j] = i;
                    j++;
                }
            }
        }

        public List<String> getCountries()
        {
            List<string> countryList = new List<string>();
            foreach (Person person in PersonList)
            {
                countryList.Add(person.country);
            }

            countryList.Sort();


            return countryList;
        }




    }
}
