using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Nanoray.Umbral
{
    [Generator]
    public class Node_WithComponents_Generator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            int maxComponentCount = 8;
            List<string> methodSources = new();

            IEnumerable<IList<bool>> VariantCombinations(int componentCount)
            {
                if (componentCount <= 0)
                {
                    yield break;

                }
                else if (componentCount == 1)
                {
                    yield return new bool[] { false };
                    yield return new bool[] { true };
                }
                else
                {
                    foreach (IList<bool> variant in VariantCombinations(componentCount - 1))
                    {
                        yield return new List<bool>(variant) { false };
                        yield return new List<bool>(variant) { true };
                    }
                }
            }

            IEnumerable<IList<bool>> AllVariantCombinations()
            {
                for (int i = 1; i <= maxComponentCount; i++)
                    foreach (var variants in VariantCombinations(i))
                        yield return variants;
            }

            string GetMethodSource(IList<bool> variants)
            {
                StringBuilder genericTypes = new();
                StringBuilder whereClauses = new();
                StringBuilder arguments = new();

                genericTypes.Append("TNode");
                whereClauses.Append("where TNode : INode");

                for (int i = 0; i < variants.Count; i++)
                {
                    if (genericTypes.Length != 0)
                        genericTypes.Append(", ");
                    genericTypes.Append($"TComponent{i + 1}");

                    if (variants[i])
                    {
                        if (genericTypes.Length != 0)
                            genericTypes.Append(", ");
                        genericTypes.Append($"TVariant{i + 1}");

                        if (whereClauses.Length != 0)
                            whereClauses.Append(' ');
                        whereClauses.Append($"where TVariant{i + 1} : IEquatable<TVariant{i + 1}>");

                        if (arguments.Length != 0)
                            arguments.Append(", ");
                        arguments.Append($"TVariant{i + 1} variant{i + 1}");
                    }
                }

                return $"public TNode WithComponents<{genericTypes}>({arguments}) {whereClauses} {{\nthrow new NotImplementedException();\n}}";
            }

            string source = $@"
                using System;

                namespace Nanoray.Umbral
                {{
                    public partial class Node
                    {{
                        {string.Join("\n\n", AllVariantCombinations().Select(variants => GetMethodSource(variants)))}
                    }}
                }}
            ";

            Console.WriteLine(source);
            context.AddSource("Node.WithComponents.g.cs", source);
        }
    }
}
