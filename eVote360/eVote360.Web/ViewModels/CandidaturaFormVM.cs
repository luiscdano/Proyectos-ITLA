using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eVote360.Web.ViewModels
{
    public class CandidaturaFormVM
    {
        // Para Edit / Details referenciados desde Controller
        public int Id { get; set; }

        [Required(ErrorMessage = "Seleccione una elección.")]
        [Display(Name = "Elección")]
        public int EleccionId { get; set; }

        [Required(ErrorMessage = "Seleccione un puesto electivo.")]
        [Display(Name = "Puesto Electivo")]
        public int PuestoElectivoId { get; set; }

        [Required(ErrorMessage = "Seleccione un partido.")]
        [Display(Name = "Partido")]
        public int PartidoPoliticoId { get; set; }

        [Required(ErrorMessage = "Indique el nombre completo.")]
        [StringLength(120, ErrorMessage = "El nombre no puede exceder 120 caracteres.")]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [StringLength(120, ErrorMessage = "El nombre en boleta no puede exceder 120 caracteres.")]
        [Display(Name = "Nombre en Boleta (opcional)")]
        public string? NombreBoleta { get; set; }

        [StringLength(250, ErrorMessage = "El FotoPath no puede exceder 250 caracteres.")]
        [Display(Name = "FotoPath (opcional)")]
        public string? FotoPath { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = true;

        // Combos
        public List<SelectListItem> Elecciones { get; set; } = new();
        public List<SelectListItem> Puestos { get; set; } = new();
        public List<SelectListItem> Partidos { get; set; } = new();
    }
}