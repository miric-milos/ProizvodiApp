using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPProizvodiApp.Models.ViewModels
{
    public class ProizvodViewModel
    {
        [Required(ErrorMessage = "Naziv je obavezan!")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Opis je obavezan!")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Kategorija je obavezna!")]
        public string Kategorija { get; set; }

        [Required(ErrorMessage = "Proizvodjac je obavezan!")]
        public string Proizvodjac { get; set; }

        [Required(ErrorMessage = "Dobavljac je obavezan!")]
        public string Dobavljac { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Cena je obavezna!")]
        [Range(1, int.MaxValue, ErrorMessage = "Cena ne sme biti negativna!")]
        public decimal Cena { get; set; }
    }
}