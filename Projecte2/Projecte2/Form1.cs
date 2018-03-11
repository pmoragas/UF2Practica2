using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projecte2
{
    public partial class Form1 : Form

    {
        People ppl;

        
       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ppl = new People();

            ppl.readPeopleFromJson();

            List<string> countryList = ppl.getAllDistinctCountries();
            List<string> genderList = ppl.getAllDistinctGenders();
            List<string> companiesList = ppl.getAllDistinctCompanies();

            populateListBox(lstBxCountry1, countryList);
            populateListBox(lstBxCountry2, countryList);
            populateListBox(lstBxCountry3, countryList);

            populateListBox(lstBxGender1, genderList);
            populateListBox(lstBxGender2, genderList);
            populateListBox(lstBxGender3, genderList);

            populateListBox(lstBxCompany1, companiesList);
            populateListBox(lstBxCompany2, companiesList);
            populateListBox(lstBxCompany3, companiesList);
   
        }

        private void populateListBox(ListBox lstBx, List<string> list)
        {
            foreach (String elem in list)
            {
                lstBx.Items.Add(elem);
            }
        }


        private async void btnSearch_Click(object sender, EventArgs e)
        {

            
            Stopwatch clock = new Stopwatch();
            startSearch(btnSearch1, lstVwResult1, clock);

            List<string> selectedCountryList = new List<string>();
            List<string> selectedGenderList = new List<string>();
            List<string> selectedCompanyList = new List<string>();

            selectedCountryList = getSelectedItems(lstBxCountry1);
            selectedGenderList = getSelectedItems(lstBxGender1);
            selectedCompanyList = getSelectedItems(lstBxCompany1);

            List<Person> resultList = new List<Person>();
            List<ListViewItem> resultListListViewItem = new List<ListViewItem>();

            await Task.Run(() =>
            {
                resultList = ppl.getPeopleByCountryList(selectedCountryList);
                resultList = ppl.getPeopleByGenderListPersonList(selectedGenderList, resultList);
                resultList = ppl.getPeopleByCompanyListPersonList(selectedCompanyList, resultList);

                resultListListViewItem = composeResultsToListViewItems(resultList);
            });


            writeResults(resultListListViewItem, lstVwResult1);

            endSearch(btnSearch1, clock, lblTime1);

        }

        
        private async void btnSearch2_Click(object sender, EventArgs e)
        {
            

            Stopwatch clock = new Stopwatch();
            startSearch(btnSearch2, lstVwResult2, clock);

            List<string> selectedCountryList = new List<string>();
            List<string> selectedGenderList = new List<string>();
            List<string> selectedCompanyList = new List<string>();

            selectedCountryList = getSelectedItems(lstBxCountry2);
            selectedGenderList = getSelectedItems(lstBxGender2);
            selectedCompanyList = getSelectedItems(lstBxCompany2);

            List<Person> resultList = new List<Person>();
            List<ListViewItem> resultListListViewItem = new List<ListViewItem>();



            await Task.Run(() =>
            {
                resultList = ppl.getPeopleByCountryListParallelFor(selectedCountryList);
                resultList = ppl.getPeopleByGenderListPersonListParallelFor(selectedGenderList, resultList);
                resultList = ppl.getPeopleByCompanyListPersonListParallelFor(selectedCompanyList, resultList);

                resultListListViewItem = composeResultsToListViewItems(resultList);
            });



            writeResults(resultListListViewItem, lstVwResult2);

            endSearch(btnSearch2, clock, lblTime2);
        }

        private async void btnSearch3_Click(object sender, EventArgs e)
        {
            Stopwatch clock = new Stopwatch();
            startSearch(btnSearch3, lstVwResult3, clock);

            List<string> selectedCountryList = new List<string>();
            List<string> selectedGenderList = new List<string>();
            List<string> selectedCompanyList = new List<string>();

            selectedCountryList = getSelectedItems(lstBxCountry3);
            selectedGenderList = getSelectedItems(lstBxGender3);
            selectedCompanyList = getSelectedItems(lstBxCompany3);

            List<Person> resultList = new List<Person>();
            List<ListViewItem> resultListListViewItem = new List<ListViewItem>();



            await Task.Run(() =>
            {
                
                resultList = ppl.getPeopleByCountryListParallelForeach(selectedCountryList);
                resultList = ppl.getPeopleByGenderListPersonListParallelForeach(selectedGenderList, resultList);
                resultList = ppl.getPeopleByCompanyListPersonListParallelForeach(selectedCompanyList, resultList);
                resultListListViewItem = composeResultsToListViewItems(resultList);
            });

            

            writeResults(resultListListViewItem, lstVwResult3);



            endSearch(btnSearch3, clock, lblTime3);
        }



        private void startSearch(Button btn, ListView lstVw, Stopwatch clock)
        {
            btn.Enabled = false;
            lstVw.Items.Clear();
            clock.Restart();
        }

        private List<string> getSelectedItems(ListBox lstBx)
        {
            List<string> itemsList = new List<string>();

            if (lstBx.SelectedIndex == -1)
            {
                foreach (String item in lstBx.Items)
                {
                    itemsList.Add(item);
                }
            } else
            {
                foreach (String item in lstBx.SelectedItems)
                {
                    itemsList.Add(item);
                }
            }

            return itemsList;
        }

        private void writeResults(List<ListViewItem> resultList, ListView lstVw)
        {
            foreach (ListViewItem result in resultList)
            {
                lstVw.Items.Add(result);
            }
        }

        private void endSearch(Button btn, Stopwatch clock, Label lbl)
        {
            clock.Stop();
            lbl.Text = clock.Elapsed.TotalSeconds.ToString() + "s";
            btn.Enabled = true;
        }

        private List<ListViewItem> composeResultsToListViewItems(List<Person> list)
        {
            int index = 1;

            String indexstr;
            String name;
            String surname;
            String email;

            List<ListViewItem> resultList = new List<ListViewItem>();

            foreach (Person person in list)
            {
                try
                {
                    indexstr = index.ToString();
                    name = person.Name;
                    surname = person.Surname;
                    email = person.email;

                    resultList.Add(new ListViewItem(new string[] { indexstr, name, surname, email }));

                    index++;
                }
                catch (Exception)
                {

                    Console.WriteLine("Error");
                }
                
            }

            return resultList;

        }
    }
}
