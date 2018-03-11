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
            populateListBox(lstBxCountry1, countryList);
            populateListBox(lstBxCountry2, countryList);
            populateListBox(lstBxCountry3, countryList);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {

            
            lstVwResult1.Items.Clear();
            
            Stopwatch clock = new Stopwatch();
            clock.Restart();

            List<string> countryList = new List<string>();

            foreach (String item in lstBxCountry1.SelectedItems)
            {
                String countryName = item;
                countryList.Add(countryName);
            }

            //var resultList = await ppl.getPeopleByCountryListAsync(countryList);


            List< ListViewItem> resultList = new List<ListViewItem>();

            await Task.Run(() =>
            {
                resultList = ppl.getPeopleByCountryList(countryList);
            });

            foreach (ListViewItem result in resultList)
            {
                lstVwResult1.Items.Add(result);
            }
            
            clock.Stop();
            lblTime1.Text = clock.Elapsed.TotalSeconds.ToString() + "s";

            

        }

        


        private void populateListBox(ListBox lstBx, List<string> list)
        {
            foreach (String elem in list)
            {
                lstBx.Items.Add(elem);
            }
        }

       

        private void writeResultsToListView(ListView lstVw, List<Person> list)
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

                lstVw.Items.Add(new ListViewItem(new string[] { indexstr, name, surname, email }));
                
                index++;
            }

        }

        private async void btnSearch2_Click(object sender, EventArgs e)
        {
            lstVwResult2.Items.Clear();

            Stopwatch clock = new Stopwatch();
            clock.Restart();

            List<string> countryList = new List<string>();

            foreach (String item in lstBxCountry2.SelectedItems)
            {
                String countryName = item;
                countryList.Add(countryName);
            }

            //var resultList = await ppl.getPeopleByCountryListAsync(countryList);


            List<ListViewItem> resultList = new List<ListViewItem>();

            await Task.Run(() =>
            {
                resultList = ppl.getPeopleByCountryListParallelFor(countryList);
            });

            foreach (ListViewItem result in resultList)
            {
                lstVwResult2.Items.Add(result);
            }

            clock.Stop();
            lblTime2.Text = clock.Elapsed.TotalSeconds.ToString() + "s";
        }

        private async void btnSearch3_Click(object sender, EventArgs e)
        {
            lstVwResult3.Items.Clear();

            Stopwatch clock = new Stopwatch();
            clock.Restart();

            List<string> countryList = new List<string>();

            foreach (String item in lstBxCountry3.SelectedItems)
            {
                String countryName = item;
                countryList.Add(countryName);
            }

            //var resultList = await ppl.getPeopleByCountryListAsync(countryList);


            List<ListViewItem> resultList = new List<ListViewItem>();

            await Task.Run(() =>
            {
                resultList = ppl.getPeopleByCountryListParallelForeach(countryList);
            });

            foreach (ListViewItem result in resultList)
            {
                lstVwResult3.Items.Add(result);
            }

            clock.Stop();
            lblTime3.Text = clock.Elapsed.TotalSeconds.ToString() + "s";
        }
    }
}
