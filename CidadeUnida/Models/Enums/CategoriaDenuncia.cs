using System.ComponentModel.DataAnnotations;

namespace CidadeUnida.Models.Enums
{
    public enum CategoriaDenuncia : int
    {
        [Display(Name = "Lixo na Rua")]
        LixoNaRua = 0,

        [Display(Name = "Árvore Caída")]
        ArvoreCaida = 1,

        [Display(Name = "Vazamento de Água")]
        VazamentoDeAgua = 2,

        [Display(Name = "Obra Irregular")]
        ObraIrregular = 3,

        [Display(Name = "Outros")]
        Outros = 4
    }
}
