using System.Collections.Generic;
using Cassowary;

namespace Nanoray.Umbral.Constraints.Cassowary
{
    internal static class CassowaryExt
    {
        internal static bool RemoveConstraintIfExists(this ClSimplexSolver solver, ClConstraint constraint)
        {
            try
            {
                solver.RemoveConstraint(constraint);
                return true;
            }
            catch (CassowaryConstraintNotFoundException)
            {
                return false;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
    }
}
