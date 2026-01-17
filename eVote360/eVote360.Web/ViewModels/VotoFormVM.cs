using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVote360.Web.ViewModels
{
    public class VotoFormVM
    {
        // Necesario para Edit
        public int Id { get; set; }

        // Solo lectura (se muestra en Edit si quieres)
        public DateTime? FechaHora { get; set; }

        [Required(ErrorMessage = "Seleccione una elección.")]
        [Display(Name = "Elección")]
        public int EleccionId { get; set; }

        [Required(ErrorMessage = "Seleccione un puesto electivo.")]
        [Display(Name = "Puesto Electivo")]
        public int PuestoElectivoId { get; set; }

        [Required(ErrorMessage = "Seleccione una candidatura.")]
        [Display(Name = "Candidatura")]
        public int CandidaturaId { get; set; }

        [Display(Name = "Token (opcional)")]
        public string? TokenVoto { get; set; }

        // Listas para combos
        public List<SelectListItem> Elecciones { get; set; } = new();
        public List<SelectListItem> Puestos { get; set; } = new();
        public List<SelectListItem> Candidaturas { get; set; } = new();
    }
}