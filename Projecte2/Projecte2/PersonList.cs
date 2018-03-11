
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        public List<String> getAllDistinctCountries()
        {
            List<string> countryList = new List<string>();

            foreach (Person person in PersonList)
            {
                countryList.Add(person.country);
            }

            countryList.Sort();

            return countryList.Distinct().ToList();
        }

        public List<String> getAllDistinctGenders()
        {
            List<string> genderList = new List<string>();

            foreach (Person person in PersonList)
            {
                genderList.Add(person.gender);
            }

            genderList.Sort();

            return genderList.Distinct().ToList();
        }

        public List<String> getAllDistinctCompanies()
        {
            List<string> companiesList = new List<string>();

            foreach (Person person in PersonList)
            {
                companiesList.Add(person.company);
            }

            companiesList.Sort();

            return companiesList.Distinct().ToList();
        }


        public List<Person> getPeopleByCountryList(List<string> countryList)
        {
            List<Person> peopleList = new List<Person>();

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

        
            return peopleList;
        }

        public List<Person> getPeopleByGenderListPersonList(List<string> genderList, List<Person> personList)
        {
            List<Person> resultList = new List<Person>();

            foreach (Person person in personList)
            {
                foreach (String gender in genderList)
                {
                    if (person.gender == gender)
                    {
                        resultList.Add(person);
                    }
                }

            }


            return resultList;
        }

        public List<Person> getPeopleByCompanyListPersonList(List<string> companyList, List<Person> personList)
        {
            List<Person> resultList = new List<Person>();

            foreach (Person person in personList)
            {
                foreach (String company in companyList)
                {
                    if (person.company == company)
                    {
                        resultList.Add(person);
                    }
                }

            }


            return resultList;
        }

        public List<Person> getPeopleByCountryListParallelFor(List<string> countryList)
        {
            List<Person> resultList = new List<Person>();
            var exceptions = new ConcurrentQueue<Exception>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            };

            for(int i = 0; i < PersonList.Count; i++)
            {
                Parallel.For(0, countryList.Count, options, j =>
                  {
                      try
                      {
                          if (PersonList[i].country == countryList[j])
                          {
                              resultList.Add(PersonList[i]);
                          }
                      }
                      catch (NullReferenceException)
                      {

                          Console.WriteLine("Error processing parallel search");
                      }

                  });

            }

            return resultList;
        }


        public List<Person> getPeopleByGenderListPersonListParallelFor(List<string> genderList, List<Person> personList)
        {
            List<Person> resultList = new List<Person>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            };

            for(int i = 0; i<personList.Count; i++ )
            {
                Parallel.For(0, genderList.Count, options, j =>
                {
                    try
                    {
                        if (personList[i].gender == genderList[j])
                        {
                            resultList.Add(personList[i]);
                        }
                    }
                    catch (NullReferenceException)
                    {

                        Console.WriteLine("Error processing parallel search");
                    }

                });

            }

            return resultList;
        }

        public List<Person> getPeopleByCompanyListPersonListParallelFor(List<string> companyList, List<Person> personList)
        {
            List<Person> resultList = new List<Person>();

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            };

            for(int i = 0; i < personList.Count; i++ )
             {
                Parallel.For(0, companyList.Count, options, j =>
               {
                   try
                   {
                       if (personList[i].company == companyList[j])
                       {
                           resultList.Add(personList[i]);
                       }
                   }
                   catch (NullReferenceException)
                   {

                       Console.WriteLine("Error processing parallel search");
                   }

               });

            }

            return resultList;
        }




        public List<Person> getPeopleByCountryListParallelForeach(List<string> countryList)
        {
            List<Person> resultList = new List<Person>();


            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            };

            
                foreach(Person person in PersonList)
                {
                    Parallel.ForEach(countryList, country =>
                    {
                        try
                        {
                            if (person.country == country)
                            {
                                resultList.Add(person);
                            }
                        }
                        catch (NullReferenceException)
                        {

                            Console.WriteLine("Error processing parallel search");
                        }
                        
                    });

                }
                      
            
            return resultList;
        }

        public List<Person> getPeopleByGenderListPersonListParallelForeach(List<string> genderList, List<Person> personList)
        {
            List<Person> resultList = new List<Person>();
 

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            };

            
                foreach(Person person in personList)
                {
                    Parallel.ForEach(genderList, gender =>
                    {
                        try
                        {
                            if (person.gender == gender)
                            {
                                resultList.Add(person);
                            }
                        }
                        catch (NullReferenceException)
                        {

                            Console.WriteLine("Error processing parallel search");
                        }
                        
                    });

                }
            
            
            return resultList;
        }

        public List<Person> getPeopleByCompanyListPersonListParallelForeach(List<string> companyList, List<Person> personList)
        {
            List<Person> resultList = new List<Person>();


            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount / 2
            };

            
                foreach(Person person in personList)
                {
                    Parallel.ForEach(companyList, company =>
                    {
                        try
                        {
                            if (person.company == company)
                            {
                                resultList.Add(person);
                            }
                        }
                        catch (NullReferenceException)
                        {

                            Console.WriteLine("Error processing parallel search");
                        }
                        
                    });

                }
            
            


            return resultList;
        }

        private List<ListViewItem> composeResultsToListViewItems(List<ListViewItem> resultList, List<Person> list)
        {
            int index = 1;

            String indexstr;
            String name;
            String surname;
            String email;

            foreach (Person person in list)
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

        
        


        public void writeListToConsole(List<string> list)
        {
            foreach (String elem in list)
            {
                Console.WriteLine(elem);
            }
        }


    }
}
