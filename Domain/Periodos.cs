using System.ComponentModel.DataAnnotations;

namespace ELLPScore
{
    public enum Periodos
    {
        [Display(Name = "Primeiro Bimestre")]
        PrimeiroBimestre,

        [Display(Name = "Segundo Bimestre")]
        SegundoBimestre,

        [Display(Name = "Terceiro Bimestre")]
        TerceiroBimestre,

        [Display(Name = "Quarto Bimestre")]
        QuartoBimestre,

        [Display(Name = "Primeiro Trimestre")]
        PrimeiroTrimestre,

        [Display(Name = "Segundo Trimestre")]
        SegundoTrimestre,

        [Display(Name = "Terceiro Trimestre")]
        TerceiroTrimestre,

        [Display(Name = "Quarto Trimestre")]
        QuartoTrimestre,

        [Display(Name = "Primeiro Semestre")]
        PrimeiroSemestre,

        [Display(Name = "Segundo Semestre")]
        SegundoSemestre,

        [Display(Name = "Anual")]
        Anual
    }
}
