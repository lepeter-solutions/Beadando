using Beadando.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Beadando.Pages
{
    public class PersonInteraktivModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _filePath;

        public PersonInteraktivModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Data", "sampledata.csv");
        }

        [BindProperty]
        public Person NewPerson { get; set; }

        public List<Person> People { get; private set; }

        public void OnGet()
        {
            People = ReadCsv();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                People = ReadCsv();
                return Page();
            }

            var people = ReadCsv();
            NewPerson.Id = people.Count > 0 ? people.Max(p => p.Id) + 1 : 1;
            WriteToCsv(NewPerson);

            return RedirectToPage();
        }

        private List<Person> ReadCsv()
        {
            var people = new List<Person>();

            if (!System.IO.File.Exists(_filePath))
            {
                return people;
            }

            var lines = System.IO.File.ReadAllLines(_filePath).Skip(1);

            foreach (var line in lines)
            {
                var data = line.Split(',');

                if (data.Length == 4 && int.TryParse(data[0], out int id) && int.TryParse(data[3], out int age))
                {
                    people.Add(new Person
                    {
                        Id = id,
                        Name = data[1],
                        Email = data[2],
                        Age = age
                    });
                }
            }

            return people;
        }

        private void WriteToCsv(Person person)
        {
            var line = $"{person.Id},{person.Name},{person.Email},{person.Age}";
            System.IO.File.AppendAllText(_filePath, line + "\n");
        }
    }
}