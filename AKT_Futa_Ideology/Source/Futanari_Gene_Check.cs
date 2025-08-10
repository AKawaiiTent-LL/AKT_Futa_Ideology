using Verse;
using RimWorld;
using RJW_Genes;
using rjw;


namespace Futanari_Social
{
    public static class GeneUtility
    {
        public static bool IsFutanari(this Pawn pawn)
        {
            if (!ModsConfig.BiotechActive || pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasActiveGene(RJW_Genes.GeneDefOf.rjw_genes_futa);
        }
    }
}
