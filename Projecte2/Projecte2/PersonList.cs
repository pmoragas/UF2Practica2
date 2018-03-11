
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public List<Person> getPeopleByCountry(string country)
        {
            List<Person> resultList = new List<Person>();

            foreach (Person person in PersonList)
            {
                if (person.country == country)
                {
                    resultList.Add(person);
                }
            }

            return resultList;
        }

        public List<ListViewItem> getPeopleByCountryList(List<string> countryList)
        {
            List<Person> peopleList = new List<Person>();
            List<ListViewItem> resultList = new List<ListViewItem>();

            foreach (Person person in PersonList)
            {
                foreach (String country in countryList)
                {
                    if (person.country == country)
                    {
                        peopleList.Add(person);
                    }
                }
                
            }


            int index = 1;

            String indexstr;
            String name;
            String surname;
            String email;

            foreach (Person person in peopleList)
            {
                indexstr = index.ToString();
                name = person.Name;
                surname = person.Surname;
                email = person.email;

                resultList.Add(new ListViewItem(new string[] { indexstr, name, surname, email }));

                index++;
            }

            return resultList;
        }

        public List<ListViewItem> getPeopleByCountryListParallelFor(List<string> countryList)
        {
            List<Person> peopleList = new List<Person>();
            List<ListViewItem> resultList = new List<ListViewItem>();


            Parallel.For(0, PersonList.Count, i =>
            {
                Parallel.For(0, countryList.Count, j =>
                 {
                     if (PersonList[i].country == countryList[j])
                     {
                         peopleList.Add(PersonList[i]);
                     }
                 });

            });
            

            int index = 1;

            String indexstr;
            String name;
            String surname;
            String email;

            foreach (Person person in peopleList)
            {
                indexstr = index.ToString();
                name = person.Name;
                surname = person.Surname;
                email = person.email;

                resultList.Add(new ListViewItem(new string[] { indexstr, name, surname, email }));

                index++;
            }


            return resultList;
        }

        public List<ListViewItem> getPeopleByCountryListParallelForeach(List<string> countryList)
        {
            List<Person> peopleList = new List<Person>();
            List<ListViewItem> resultList = new List<ListViewItem>();


            Parallel.ForEach(PersonList, person =>
            {
                Parallel.ForEach(countryList, country =>
                {
                    if (person.country == country)
                    {
                        peopleList.Add(person);
                    }
                });

            });


            int index = 1;

            String indexstr;
            String name;
            String surname;
            String email;

            foreach (Person person in peopleList)
            {
                indexstr = index.ToString();
                name = person.Name;
                surname = person.Surname;
                email = person.email;

                resultList.Add(new ListViewItem(new string[] { indexstr, name, surname, email }));

                index++;
            }


            return resultList;
        }

        public async Task<List<Person>> getPeopleByCountryListAsync(List<string> countryList)
        {
            List<Person> resultList = new List<Person>();

            foreach (Person person in PersonList)
            {
                foreach (String country in countryList)
                {
                    if (person.country == country)
                    {
                        resultList.Add(person);
                    }
                }

            }
            System.Threading.Thread.Sleep(5000);

            return await Task.Run(() => resultList);
        }

        public List<String> getAllDistinctCountries()
        {
            List<string> countryList = new List<string>();
            List<string> countryListDistinct = new List<string>();

            foreach (Person person in PersonList)
            {
                countryList.Add(person.country);
            }

            countryList.Sort();

            return countryList.Distinct().ToList();
        }



        public void writeListToConsole(List<string> list)
        {
            foreach (String elem in list)
            {
                Console.WriteLine(elem);
            }
        }


    }
}
