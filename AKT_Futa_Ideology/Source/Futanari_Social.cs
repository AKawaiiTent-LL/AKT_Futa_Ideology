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



    public class ThoughtWorker_Precept_IsFutanari : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (!ModsConfig.BiotechActive || !ModsConfig.IdeologyActive)
            {
                return ThoughtState.Inactive;
            }
            return p.IsFutanari();
        }
    }
    public class ThoughtWorker_Precept_FutanariPresent : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (!ModsConfig.BiotechActive || !ModsConfig.IdeologyActive || p.IsFutanari())
            {
                return ThoughtState.Inactive;
            }
            foreach (Pawn item in p.MapHeld.mapPawns.AllPawnsSpawned)
            {
                if (item.IsFutanari() && (item.IsPrisonerOfColony || item.IsSlaveOfColony || item.IsColonist))
                {
                    return ThoughtState.ActiveDefault;
                }
            }
            return ThoughtState.Inactive;
        }
    }

    public class ThoughtWorker_Precept_FutanariColonist : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (!ModsConfig.BiotechActive || !ModsConfig.IdeologyActive || p.IsFutanari() || p.Faction == null)
            {
                return ThoughtState.Inactive;
            }
            _ = p.Ideo;
            bool flag = false;
            foreach (Pawn item in p.MapHeld.mapPawns.SpawnedPawnsInFaction(p.Faction))
            {
                if (item.IsFutanari())
                {
                    flag = true;
                    Precept_Role precept_Role = item.Ideo?.GetRole(item);
                    if (precept_Role != null && precept_Role.ideo == p.Ideo && precept_Role.def == RimWorld.PreceptDefOf.IdeoRole_Leader)
                    {
                        return ThoughtState.ActiveAtStage(2);
                    }
                }
            }
            if (flag)
            {
                return ThoughtState.ActiveAtStage(1);
            }
            return ThoughtState.ActiveAtStage(0);
        }
    }

    public class ThoughtWorker_Precept_Futanari_Social : ThoughtWorker_Precept_Social
    {
        protected override ThoughtState ShouldHaveThought(Pawn p, Pawn otherPawn)
        {
            if (!ModsConfig.BiotechActive || !ModsConfig.IdeologyActive)
            {
                return ThoughtState.Inactive;
            }
            return otherPawn.IsFutanari();
        }
    }
}

